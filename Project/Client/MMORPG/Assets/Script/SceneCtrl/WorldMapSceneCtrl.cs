//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-06 07:43:41
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class WorldMapSceneCtrl : GameSceneCtrlBase
{
    private WorldMapEntity CurrWorldMapEntity;
    private Dictionary<int, WorldMapTransCtrl> m_TransDic;

    public static WorldMapSceneCtrl Instance;

    private float m_NextUpdatePosTime = 0;

    private Dictionary<int, RoleCtrl> m_AllRoleDic;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_TransDic = new Dictionary<int, WorldMapTransCtrl>();
        m_AllRoleDic = new Dictionary<int, RoleCtrl>();
        Instance = this;
        AddListener();
    }

    private void OnPlayerFailEventCallback(object[] p)
    {
        UIViewMgr.Instance.OpenWindow(WindowUIType.WorldMapFail);
    }

    #region 角色进入场景同步
    private void AddListener()
    {
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_InitRole, OnWorldMap_InitRole);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_OtherRoleEnter, OnWorldMap_OtherRoleEnter);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_OtherRoleLeave, OnWorldMap_OtherRoleLeave);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_OtherRoleMove, OnWorldMap_OtherRoleMove);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_OtherRoleUseSkill, OnWorldMap_OtherRoleUseSkill);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_OtherRoleDie, OnWorldMap_OtherRoleDie);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_OtherRoleResurgence, OnWorldMap_OtherRoleResurgence);

        EventDispatcher.Instance.AddEventHandler(ConstDefine.PlayerFailEvent, OnPlayerFailEventCallback);
        EventDispatcher.Instance.AddEventHandler(ConstDefine.PlayerResurgenceEvent, OnPlayerResurgenceEventCallback);

    }

    private void OnPlayerResurgenceEventCallback(object[] p)
    {
        WorldMap_CurrRoleResurgenceProto proto = new WorldMap_CurrRoleResurgenceProto();
        proto.Type = 0;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    private void OnWorldMap_InitRole(byte[] p)
    {
        WorldMap_InitRoleProto proto = WorldMap_InitRoleProto.GetProto(p);
        if (proto.ItemList != null)
        {
            List<WorldMap_InitRoleProto.RoleItem> roleList = proto.ItemList;
            WorldMap_InitRoleProto.RoleItem roleItem;
            Vector3 pos;
            for (int i = 0; i < roleList.Count; i++)
            {
                roleItem = roleList[i];
                pos = new Vector3(roleItem.RolePosX, roleItem.RolePosY, roleItem.RolePosZ);
                CreateOtherPlayer(roleItem.RoleId, roleItem.RoleNickName, roleItem.RoleLevel, roleItem.RoleJobId, pos, roleItem.RoleYAngle, roleItem.RoleCurrHP, roleItem.RoleMaxHP, roleItem.RoleCurrMP, roleItem.RoleMaxMP);
                //AppDebug.LogError("初始化场景内其他玩家:" + roleItem.RoleNickName);
            }
        }
    }

    private void OnWorldMap_OtherRoleEnter(byte[] p)
    {
        WorldMap_OtherRoleEnterProto proto = WorldMap_OtherRoleEnterProto.GetProto(p);
        Vector3 pos = new Vector3(proto.RolePosX, proto.RolePosY, proto.RolePosZ);
        CreateOtherPlayer(proto.RoleId, proto.RoleNickName, proto.RoleLevel, proto.RoleJobId, pos, proto.RoleYAngle, proto.RoleCurrHP, proto.RoleCurrHP, proto.RoleCurrMP, proto.RoleMaxMP);
        //AppDebug.LogError(proto.RoleNickName + "进入场景");
    }

    private void CreateOtherPlayer(int roleId, string nickName, int roleLevel, byte jobId, Vector3 pos, float yAngle, int currHP, int maxHP, int currMP, int maxMP)
    {
        JobEntity entity = JobDBModel.Instance.Get(jobId);
        if(entity != null)
        {
            Transform otherRoleTrans = RecyclePoolMgr.Instance.Spawn(PoolType.Player, ResourLoadType.AssetBundle, string.Format("Role/{0}", entity.PrefabName));
            otherRoleTrans.eulerAngles = new Vector3(0, yAngle, 0);
            RoleCtrl ctrl = otherRoleTrans.GetComponent<RoleCtrl>();
            RoleInfoMainPlayer roleInfoPlayer = new RoleInfoMainPlayer();
            roleInfoPlayer.RoleId = roleId;
            roleInfoPlayer.RoleNickName = nickName;
            roleInfoPlayer.Level = roleLevel;
            roleInfoPlayer.JobId = jobId;
            roleInfoPlayer.CurrHP = currHP;
            roleInfoPlayer.MaxHP = maxHP;
            roleInfoPlayer.CurrMP = currMP;
            roleInfoPlayer.MaxMP = maxMP;
            ctrl.Init(RoleType.OtherPlayer, roleInfoPlayer, new OtherRoleAI(ctrl));
            ctrl.Born(pos);
            if (ctrl != null)
            {
                m_AllRoleDic.Add(roleId, ctrl);
            }
        }
    }

    private void RemoveListener()
    {
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_InitRole, OnWorldMap_InitRole);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_OtherRoleEnter, OnWorldMap_OtherRoleEnter);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_OtherRoleLeave, OnWorldMap_OtherRoleLeave);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_OtherRoleMove, OnWorldMap_OtherRoleMove);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_OtherRoleUseSkill, OnWorldMap_OtherRoleUseSkill);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_OtherRoleDie, OnWorldMap_OtherRoleDie);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_OtherRoleResurgence, OnWorldMap_OtherRoleResurgence);

        EventDispatcher.Instance.RemoveEventHandler(ConstDefine.PlayerFailEvent, OnPlayerFailEventCallback);
        EventDispatcher.Instance.RemoveEventHandler(ConstDefine.PlayerResurgenceEvent, OnPlayerResurgenceEventCallback);
    }

    private void OnWorldMap_OtherRoleResurgence(byte[] p)
    {
        WorldMap_OtherRoleResurgenceProto proto = WorldMap_OtherRoleResurgenceProto.GetProto(p);
        AppDebug.LogError("OnWorldMap_OtherRoleResurgence");
        if (m_AllRoleDic.ContainsKey(proto.RoleId))
        {
            AppDebug.LogError("ToResurgence : " + m_AllRoleDic[proto.RoleId].CurrRoleInfo.RoleNickName);
            m_AllRoleDic[proto.RoleId].ToResurgence(RoleIdleState.IdleNormal);
        }
    }

    private void OnWorldMap_OtherRoleDie(byte[] p)
    {
        WorldMap_OtherRoleDieProto proto = WorldMap_OtherRoleDieProto.GetProto(p);
        if(proto.RoleIdList != null)
        {
            for (int i = 0; i < proto.RoleIdList.Count; i++)
            {
                int dieRoleId = proto.RoleIdList[i];
                if (m_AllRoleDic.ContainsKey(dieRoleId))
                {
                    //AppDebug.LogError(string.Format("OnWorldMap_OtherRoleDie : {0},  {1}", m_AllRoleDic[dieRoleId].CurrRoleInfo.RoleNickName, Time.realtimeSinceStartup));
                    m_AllRoleDic[dieRoleId].ToDie();
                }

                WorldMapCtrl.Instance.AttackNickName = m_AllRoleDic[proto.AttackRoleId].CurrRoleInfo.RoleNickName;
            }
        }
    }

    private void OnWorldMap_OtherRoleUseSkill(byte[] buffer)
    {
        WorldMap_OtherRoleUseSkillProto proto = WorldMap_OtherRoleUseSkillProto.GetProto(buffer);
        //AppDebug.LogError(string.Format("OnWorldMap_OtherRoleUseSkill : {0}, skillId : {1}", Time.realtimeSinceStartup, proto.SkillId));
        if (m_AllRoleDic.ContainsKey(proto.AttackRoleId))
        {
            SkillEntity skillEntity = SkillDBModel.Instance.Get(proto.SkillId);
            if(skillEntity != null)
            {
                m_AllRoleDic[proto.AttackRoleId].m_Attack.PlayAttack(skillEntity.IsPhyAttack == 1? RoleAttackType.PhyAttack : RoleAttackType.SkillAttack, proto.SkillId);
            }
        }

        if (proto.ItemList != null)
        {
            for (int i = 0; i < proto.ItemList.Count; i++)
            {
                WorldMap_OtherRoleUseSkillProto.BeAttackItem beAttackItem = proto.ItemList[i];
                if (m_AllRoleDic.ContainsKey(beAttackItem.BeAttackRoleId))
                {
                    RoleTransferAttackInfo roleTransferAttackInfo = new RoleTransferAttackInfo();
                    roleTransferAttackInfo.AttackRoleId = proto.AttackRoleId;
                    roleTransferAttackInfo.BeAttackRoleId = beAttackItem.BeAttackRoleId;
                    roleTransferAttackInfo.HurtValue = beAttackItem.ReduceHp;
                    roleTransferAttackInfo.IsCri = beAttackItem.IsCri == 1;
                    roleTransferAttackInfo.SkillId = proto.SkillId;
                    roleTransferAttackInfo.SkillLevel = proto.SkillLevel;
                    m_AllRoleDic[beAttackItem.BeAttackRoleId].ToHurt(roleTransferAttackInfo);
                }
            }
        }
    }

    private void OnWorldMap_OtherRoleMove(byte[] p)
    {
        WorldMap_OtherRoleMoveProto proto = WorldMap_OtherRoleMoveProto.GetProto(p);
        if (m_AllRoleDic.ContainsKey(proto.RoleId))
        {
            RoleCtrl ctrl = m_AllRoleDic[proto.RoleId];
            Vector3 targetPos = new Vector3(proto.TargetPosX, proto.TargetPosY, proto.TargetPosZ);
            OtherRoleAI otherRoleAI = ctrl.CurrRoleAI as OtherRoleAI;
            if(otherRoleAI != null)
            {
                otherRoleAI.MoveTo(targetPos, proto.ServerTime, proto.NeedTime);
            }
        }
    }

    private void OnWorldMap_OtherRoleLeave(byte[] p)
    {
        WorldMap_OtherRoleLeaveProto proto = WorldMap_OtherRoleLeaveProto.GetProto(p);
        if (m_AllRoleDic.ContainsKey(proto.RoleId))
        {
            m_AllRoleDic[proto.RoleId].DespawnHeadBar();
            RecyclePoolMgr.Instance.Despawn(PoolType.Player, m_AllRoleDic[proto.RoleId].transform);
            //AppDebug.LogError(string.Format("{0}离开了场景", m_AllRoleDic[proto.RoleId].CurrRoleInfo.RoleNickName));
            m_AllRoleDic.Remove(proto.RoleId);
        }
    }

    private void SendRoleAlreadyEnterMsg()
    {
        WorldMap_RoleAlreadyEnterProto proto = new WorldMap_RoleAlreadyEnterProto();
        proto.TargetWorldMapSceneId = SceneMgr.Instance.CurrWorldMapId;
        proto.RolePosX = GlobalInit.Instance.CurrPlayer.transform.position.x;
        proto.RolePosY = GlobalInit.Instance.CurrPlayer.transform.position.y;
        proto.RolePosZ = GlobalInit.Instance.CurrPlayer.transform.position.z;
        proto.RoleYAngle = GlobalInit.Instance.CurrPlayer.transform.eulerAngles.y;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    #endregion

    protected override void OnLoadUIMainCityView()
    {
        RoleMgr.Instance.InitMainPlayer();

        if (GlobalInit.Instance != null && GlobalInit.Instance.CurrPlayer != null)
        {
            m_AllRoleDic.Add(GlobalInit.Instance.MainPlayerInfo.RoleId, GlobalInit.Instance.CurrPlayer);
            CurrWorldMapEntity = WorldMapDBModel.Instance.Get(SceneMgr.Instance.CurrWorldMapId);
            if (CurrWorldMapEntity != null)
            {
                InitTransPos();
                if (SceneMgr.Instance.TargetWorldMapTransPosId > 0)
                {
                    if (m_TransDic.ContainsKey(SceneMgr.Instance.TargetWorldMapTransPosId))
                    {
                        WorldMapTransCtrl ctrl = m_TransDic[SceneMgr.Instance.TargetWorldMapTransPosId];
                        Vector3 newPos = ctrl.transform.forward.normalized * 3 + ctrl.transform.position;
                        GlobalInit.Instance.CurrPlayer.Born(newPos);
                        GlobalInit.Instance.CurrPlayer.transform.eulerAngles = new Vector3(0, ctrl.transform.eulerAngles.y, 0);
                    }
                    
                    SceneMgr.Instance.TargetWorldMapTransPosId = 0;
                }
                else
                {
                    if(GlobalInit.Instance.MainPlayerInfo.LastInWorldMapRotateY > 0)
                    {
                        GlobalInit.Instance.CurrPlayer.Born(GlobalInit.Instance.MainPlayerInfo.LastInWorldMapPos);
                        GlobalInit.Instance.CurrPlayer.transform.eulerAngles = new Vector3(0, GlobalInit.Instance.MainPlayerInfo.LastInWorldMapRotateY, 0);
                    }
                    else
                    {
                        GlobalInit.Instance.CurrPlayer.Born(CurrWorldMapEntity.RoleBirthPosition);
                        GlobalInit.Instance.CurrPlayer.transform.eulerAngles = new Vector3(0, CurrWorldMapEntity.RoleBirthEulerAnglesY, 0);
                    }
                }
            }
            else
            {
                GlobalInit.Instance.CurrPlayer.Born(PlayerBornPos);
            }
            SendRoleAlreadyEnterMsg();
            PlayerCtrl.Instance.SetMainCityData();
        }

        //加载完毕
        if (DelegateDefine.Instance.OnSceneLoadOK != null)
        {
            DelegateDefine.Instance.OnSceneLoadOK();
        }

        StartCoroutine(InitNPC());
        AutoMove();
    }

    private IEnumerator InitNPC()
    {
        yield return null;

        if (CurrWorldMapEntity == null) yield break;

        for (int i = 0; i < CurrWorldMapEntity.NPCWorldMapDataList.Count; i++)
        {
            NPCWorldMapData data = CurrWorldMapEntity.NPCWorldMapDataList[i];
            GameObject obj = RoleMgr.Instance.LoadNPC(data.npcEntity.PrefabName);
            obj.transform.position = data.NPCPosition;
            obj.transform.eulerAngles = new Vector3(0, data.EulerAnglesY, 0);
            obj.transform.localScale = Vector3.one;
            NPCCtrl ctrl = obj.GetComponent<NPCCtrl>();
            ctrl.Init(data);
        }
    }

    private void InitTransPos()
    {
        if (CurrWorldMapEntity == null) return;

        string[] arr1 = CurrWorldMapEntity.TransPos.Split('|');
        if(arr1 != null)
        {
            for (int i = 0; i < arr1.Length; i++)
            {
                string[] arr2 = arr1[i].Split('_');
                if(arr2 != null && arr2.Length >= 7)
                {
                    float x = arr2[0].ToFloat();
                    float y = arr2[1].ToFloat();
                    float z = arr2[2].ToFloat();
                    float eulerY = arr2[3].ToFloat();
                    Transform effectTrans = RecyclePoolMgr.Instance.Spawn(PoolType.Effect, ResourLoadType.AssetBundle, "Effect/Effect_Trans");
                    effectTrans.parent = null;
                    effectTrans.localScale = Vector3.one;
                    effectTrans.eulerAngles = new Vector3(0, eulerY, 0);
                    effectTrans.position = new Vector3(x, y, z);
                    WorldMapTransCtrl worldMapTransCtrl = effectTrans.GetComponent<WorldMapTransCtrl>();
                    if(worldMapTransCtrl != null)
                    {
                        worldMapTransCtrl.SetParam(arr2[4].ToInt(), arr2[5].ToInt(), arr2[6].ToInt());
                        m_TransDic.Add(i + 1, worldMapTransCtrl);
                    }
                }
            }
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyUp(KeyCode.A))
        {
            Transform trans = GlobalInit.Instance.CurrPlayer.transform;
            AppDebug.LogError(string.Format("{0}_{1}_{2}_{3}", trans.position.x, trans.position.y, trans.position.z, trans.eulerAngles.y));
        }

        if(Time.time > m_NextUpdatePosTime)
        {
            m_NextUpdatePosTime = Time.time + 1;
            SendServerPos();
        }
    }

    private void SendServerPos()
    {
        if(GlobalInit.Instance != null && GlobalInit.Instance.MainPlayerInfo != null && GlobalInit.Instance.CurrPlayer != null)
        {
            WorldMap_PosProto proto = new WorldMap_PosProto();
            GlobalInit.Instance.MainPlayerInfo.LastInWorldMapPos = GlobalInit.Instance.CurrPlayer.transform.position;
            proto.x = GlobalInit.Instance.MainPlayerInfo.LastInWorldMapPos.x;
            proto.y = GlobalInit.Instance.MainPlayerInfo.LastInWorldMapPos.y;
            proto.z = GlobalInit.Instance.MainPlayerInfo.LastInWorldMapPos.z;
            proto.yAngle = GlobalInit.Instance.CurrPlayer.transform.eulerAngles.y;
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
            GlobalInit.Instance.MainPlayerInfo.LastInWorldMapRotateY = proto.yAngle;
        }
    }

    public void AutoMove()
    {
        if (!WorldMapCtrl.Instance.AutoMove) return;

        if(WorldMapCtrl.Instance.ToMoveSceneId == SceneMgr.Instance.CurrWorldMapId)
        {
            AppDebug.LogError("移动到了目标场景");
            if (GlobalInit.Instance.CurrPlayer != null)
            {
               // GlobalInit.Instance.CurrPlayer.MoveTo(WorldMapCtrl.Instance.TargetPos);
            }
            WorldMapCtrl.Instance.AutoMove = false;
            return;
        }

        foreach (var item in m_TransDic.Values)
        {
            if(item.TargetSceneId == WorldMapCtrl.Instance.ToMoveSceneId)
            {
                if(GlobalInit.Instance.CurrPlayer != null)
                {
                    GlobalInit.Instance.CurrPlayer.MoveTo(item.transform.position);
                }
                break;
            }
        }

        WorldMapCtrl.Instance.NextScene();
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        RemoveListener();
        ClearRecycleRes();
    }

    private void ClearRecycleRes()
    {
        foreach (var item in m_AllRoleDic.Values)
        {
            if (item.CurrRoleType == RoleType.MainPlayer) continue;
            item.RoleRecycle();
        }

        m_AllRoleDic.Clear();
    }
}