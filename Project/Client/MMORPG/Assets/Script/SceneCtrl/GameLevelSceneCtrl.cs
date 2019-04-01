using PathologicalGames;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelSceneCtrl : GameSceneCtrlBase
{
    public static GameLevelSceneCtrl Instance;

    [SerializeField]
    private List<GameLevelRegionCtrl> regionList;

    private int m_CurrGameLevelId;

    private int m_CurrRegionIndex = 0;

    private GameLevelGrade m_CurrGrade;

    private int m_AllMonsterCount;

    private List<int> m_MonsterIdList;

    private int m_CurrRegionMonsterCount;

    private List<GameLevelMonsterEntity> m_RegionMonsterList = new List<GameLevelMonsterEntity>();

    private float m_MonsterCreateTime = 0;

    private int m_CurrRegionCreatedMonsterCnt = 0;

    private int m_CurrRegionKillMonsterCount;

    private GameLevelRegionCtrl m_CurrGameLevelRegionCtrl;

    private int m_RoleMonsterId;

    private int m_RegionId;

    private UIGameLevelVictoryView m_GameLevelVictoryView;

    private List<RoleCtrl> m_CurrRegionLiveMonsterList;
    public List<RoleCtrl> CurrRegionLiveMonsterList
    {
        get { return m_CurrRegionLiveMonsterList; }
    }

    public bool CurrRegionHasMonster
    {
        get { return m_CurrRegionKillMonsterCount < m_CurrRegionMonsterCount; }
    }

    public bool IsLastRegion
    {
        get { return m_CurrRegionIndex > regionList.Count - 1; }
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        Instance = this;
    }

    protected override void OnStart()
    {
        base.OnStart();
        m_CurrGameLevelId = SceneMgr.Instance.CurrGameLevelId;
        m_CurrGrade = SceneMgr.Instance.CurrGameLevelGrade;
    }

    protected override void OnLoadUIMainCityView()
    {
        base.OnLoadUIMainCityView();

        PlayerCtrl.Instance.SetMainCityData();
        RoleMgr.Instance.InitMainPlayer();

        m_AllMonsterCount = GameLevelMonsterDBModel.Instance.GetGameLevelMonsterTotalCount(m_CurrGameLevelId, m_CurrGrade);
        m_MonsterIdList = GameLevelMonsterDBModel.Instance.GetGameLevelMonsterIdList(m_CurrGameLevelId, m_CurrGrade);

        m_CurrRegionLiveMonsterList = new List<RoleCtrl>();

        EnterRegionCtrl();
    }

    private void OnMainPlayerDieCallback(RoleCtrl obj)
    {
        UIViewMgr.Instance.OpenWindow(WindowUIType.GameLevelFail);
    }

    private void EnterRegionCtrl()
    {
        GameLevelEntity gameLevelEntity = GameLevelDBModel.Instance.Get(m_CurrGameLevelId);
        if (gameLevelEntity == null) return;

        int regionId = gameLevelEntity.GetRegionIdByIndex(m_CurrRegionIndex);
        if (regionId == -1) return;

        m_CurrGameLevelRegionCtrl = GetRegionCtrlByRegionId(regionId);
        if (m_CurrGameLevelRegionCtrl == null) return;

        m_CurrGameLevelRegionCtrl.DestroyRegionMask();

        m_CurrRegionCreatedMonsterCnt = 0;
        m_CurrRegionKillMonsterCount = 0;

        m_CurrRegionLiveMonsterList.Clear();

        if (m_CurrRegionIndex == 0)
        {
            PlayerBornPos = m_CurrGameLevelRegionCtrl.RoleBornPos.position;

            if (GlobalInit.Instance.CurrPlayer != null)
            {
                GlobalInit.Instance.CurrPlayer.transform.eulerAngles = Vector3.zero;
                GlobalInit.Instance.CurrPlayer.Born(PlayerBornPos);
                GlobalInit.Instance.CurrPlayer.ToIdle(RoleIdleState.IdleFight);
                GlobalInit.Instance.CurrPlayer.OnRoleDie = OnMainPlayerDieCallback;
            }


            //加载完毕
            if (DelegateDefine.Instance.OnSceneLoadOK != null)
            {
                DelegateDefine.Instance.OnSceneLoadOK();
            }
        }
        else
        {
            GameLevelDoorCtrl toNextDoor = m_CurrGameLevelRegionCtrl.GetToNextRegionDoor(m_RegionId);
            if (toNextDoor != null)
            {
                toNextDoor.gameObject.SetActive(false);
                if (toNextDoor.ConnectDoor != null)
                {
                    toNextDoor.ConnectDoor.gameObject.SetActive(false);
                }
            }
        }

        m_RegionId = regionId;

        m_CurrRegionMonsterCount = GameLevelMonsterDBModel.Instance.GetRegionMonsterTotalCount(m_CurrGameLevelId, m_CurrGrade, regionId);

        GameLevelMonsterDBModel.Instance.GetGameLevelMonsterEntityList(m_CurrGameLevelId, m_CurrGrade, regionId, m_RegionMonsterList);
    }

    private GameLevelRegionCtrl GetRegionCtrlByRegionId(int regionId)
    {
        if(regionList != null)
        {
            for (int i = 0; i < regionList.Count; i++)
            {
                if(regionList[i].regionId == regionId)
                {
                    return regionList[i];
                }
            }
        }

        return null;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if(m_CurrRegionCreatedMonsterCnt <= m_AllMonsterCount)
        {
            if (Time.time > m_MonsterCreateTime)
            {
                m_MonsterCreateTime = Time.time + 1;

                CreateMonster();
            }
        }
    }

    private void CreateMonster()
    {
        //if (m_CurrRegionCreatedMonsterCnt > 1) return;

        m_CurrRegionCreatedMonsterCnt++;

        if(m_RegionMonsterList.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, m_RegionMonsterList.Count);
            GameLevelMonsterEntity gameLevelMonsterEntity = m_RegionMonsterList[index];
            if (gameLevelMonsterEntity.SpriteCount > 0)
            {
                Transform monsterTransform = RecyclePoolMgr.Instance.Spawn(PoolType.Monster, ResourLoadType.AssetBundle, string.Format("Role/{0}", SpriteDBModel.Instance.Get(gameLevelMonsterEntity.SpriteId).PrefabName));
                Transform monsterBornTransform = m_CurrGameLevelRegionCtrl.MonsterBornPos[UnityEngine.Random.Range(0, m_CurrGameLevelRegionCtrl.MonsterBornPos.Count)];
                monsterTransform.localScale = Vector3.one;
                RoleCtrl roleMonster = monsterTransform.GetComponent<RoleCtrl>();
                if(roleMonster != null)
                {
                    SpriteEntity spriteEntity = SpriteDBModel.Instance.Get(gameLevelMonsterEntity.SpriteId);
                    RoleInfoMonster infoMonster = new RoleInfoMonster();
                    if (spriteEntity != null)
                    {
                        infoMonster.RoleId = ++m_RoleMonsterId;
                        infoMonster.RoleNickName = spriteEntity.Name;
                        infoMonster.Level = spriteEntity.Level;
                        infoMonster.Attack = spriteEntity.Attack;
                        infoMonster.Defense = spriteEntity.Defense;
                        infoMonster.Hit = spriteEntity.Hit;
                        infoMonster.Dodge = spriteEntity.Dodge;
                        infoMonster.Cri = spriteEntity.Cri;
                        infoMonster.Res = spriteEntity.Res;
                        infoMonster.Fighting = spriteEntity.Fighting;
                        infoMonster.CurrHP = infoMonster.MaxHP = spriteEntity.HP;
                        infoMonster.CurrMP = infoMonster.MaxMP = spriteEntity.MP;
                        infoMonster.spriteEntity = spriteEntity;

                        roleMonster.ViewRange = spriteEntity.Range_View;
                        roleMonster.Speed = spriteEntity.MoveSpeed;
                    }
                    roleMonster.Init(RoleType.Monster, infoMonster, new GameLevelRoleMonsterAI(roleMonster, infoMonster));
                    roleMonster.Born(monsterBornTransform.TransformPoint(new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), 0, UnityEngine.Random.Range(-0.5f, 0.5f))));
                    roleMonster.OnRoleDie = OnMonsterDieCallback;
                    m_CurrRegionLiveMonsterList.Add(roleMonster);
                }

                gameLevelMonsterEntity.SpriteCount--;
                if(gameLevelMonsterEntity.SpriteCount <= 0)
                {
                    m_RegionMonsterList.RemoveAt(index);
                }
            }
        }
    }

    private void OnMonsterDieCallback(RoleCtrl obj)
    {
        if (m_CurrRegionLiveMonsterList.Contains(obj))
        {
            m_CurrRegionLiveMonsterList.Remove(obj);
        }

        RoleInfoMonster roleInfoMonster = obj.CurrRoleInfo as RoleInfoMonster;
        if(roleInfoMonster != null && roleInfoMonster.spriteEntity != null)
        {
            GameLevelMonsterEntity gameLevelMonsterEntity = GameLevelMonsterDBModel.Instance.GetGameLevelMonsterEntity(m_CurrGameLevelId, m_CurrGrade, m_RegionId, roleInfoMonster.spriteEntity.Id);
            if(gameLevelMonsterEntity != null)
            {
                if (gameLevelMonsterEntity.Exp > 0)
                {
                    UITipView.Instance.ShowTips(0, gameLevelMonsterEntity.Exp.ToString());
                }

                if (gameLevelMonsterEntity.Gold > 0)
                {
                    UITipView.Instance.ShowTips(1, gameLevelMonsterEntity.Gold.ToString());
                }
            }
        }

        m_CurrRegionKillMonsterCount++;
        if (m_CurrRegionKillMonsterCount >= m_CurrRegionMonsterCount)
        {
            m_CurrRegionIndex++;
            if(m_CurrRegionIndex > regionList.Count - 1)
            {
                TimeMgr.Instance.SetTimeScale(0.3f, 5);
                AccurateTimerMgr.Instance.CreateTimer(5000, ()=> 
                {
                    AppDebug.LogError("弹出胜利界面");
                    UIViewMgr.Instance.OpenWindow(WindowUIType.GameLevelVictory);
                });
                return;
            }

            EnterRegionCtrl();
        }
    }

    


#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        
        if(regionList != null)
        {
            for (int i = 0; i < regionList.Count; i++)
            {
                Gizmos.DrawLine(transform.position, regionList[i].transform.position);
            }
        }

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
#endif
}
