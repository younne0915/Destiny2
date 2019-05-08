//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-02 20:22:50
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using Pathfinding;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(Seeker))]
[RequireComponent(typeof(FunnelModifier))]

/// <summary>
/// 角色控制器
/// </summary>
public class RoleCtrl : MonoBehaviour 
{
    #region 成员变量或属性
    /// <summary>
    /// 昵称挂点
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;

    /// <summary>
    /// 头顶UI条
    /// </summary>
    private GameObject m_HeadBar;

    /// <summary>
    /// 动画
    /// </summary>
    public Animator Animator;

    /// <summary>
    /// 移动的目标点
    /// </summary>
    [HideInInspector]
    public Vector3 TargetPos = Vector3.zero;

    /// <summary>
    /// 控制器
    /// </summary>
    [HideInInspector]
    public CharacterController CharacterController;

    /// <summary>
    /// 移动速度
    /// </summary>
    [HideInInspector]
    public float Speed = 10f;

    /// <summary>
    /// 移动速度
    /// </summary>
    [HideInInspector]
    public float ModifySpeed = 10f;

    /// <summary>
    /// 出生点
    /// </summary>
    [HideInInspector]
    public Vector3 BornPoint;

    /// <summary>
    /// 视野范围
    /// </summary>
    public float ViewRange;

    /// <summary>
    /// 巡逻范围
    /// </summary>
    public float PatrolRange;

    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange;

    /// <summary>
    /// 当前角色类型
    /// </summary>
    public RoleType CurrRoleType = RoleType.None;

    /// <summary>
    /// 当前角色信息
    /// </summary>
    public RoleInfoBase CurrRoleInfo = null;

    /// <summary>
    /// 当前角色AI
    /// </summary>
    public IRoleAI CurrRoleAI = null;

    /// <summary>
    /// 锁定敌人
    /// </summary>
    [HideInInspector]
    public RoleCtrl LockEnemy;

    /// <summary>
    /// 角色受伤委托
    /// </summary>
    public System.Action OnRoleHurt;

    /// <summary>
    /// 角色死亡
    /// </summary>
    public System.Action<RoleCtrl> OnRoleDie;

    /// <summary>
    /// 当前角色有限状态机管理器
    /// </summary>
    public RoleFSMMgr CurrRoleFSMMgr = null;

    private RoleHeadBarView roleHeadBarView = null;


    //=============================寻路相关=======================================
    private Seeker m_Seeker;
    public Seeker Seeker
    {
        get { return m_Seeker; }
    }

    [HideInInspector]
    public ABPath AStarPath;

    public int AStarCurrWavePointIndex = 1;

    //==============================战斗相关====================================
    public RoleHurt m_Hurt;

    public RoleAttack m_Attack;

    [HideInInspector]
    public SkillEntity CurrSkillEntity;

    [HideInInspector]
    public float PreIdleFightTime = 0;

    private bool m_IsAutoFight = false;

    public bool IsAutoFight
    {
        get { return m_IsAutoFight; }
        set
        {
            m_IsAutoFight = value;
            if (!IsAutoFight)
            {
                LockEnemy = null;
            }
        }
    }

    public bool AlreadyDied = false;

    public delegate void OnValueChangeHandler(ValueChangeType type);
    public OnValueChangeHandler OnHPChange;
    public OnValueChangeHandler OnMPChange;

    private bool m_Init = false;

    #endregion

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="roleType">角色类型</param>
    /// <param name="roleInfo">角色信息</param>
    /// <param name="ai">AI</param>
    public void Init(RoleType roleType, RoleInfoBase roleInfo, IRoleAI ai)
    {
        CurrRoleType = roleType;
        CurrRoleInfo = roleInfo;
        CurrRoleAI = ai;
    }

    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        m_Seeker = GetComponent<Seeker>();
        if (CurrRoleType == RoleType.MainPlayer)
        {
            if (CameraCtrl.Instance != null)
            {
                CameraCtrl.Instance.Init();
            }
        }

        CurrRoleFSMMgr = new RoleFSMMgr(this, OnRoleDieCallback, OnRoleDestroyCallback);
        m_Hurt = new RoleHurt(CurrRoleFSMMgr);
        m_Hurt.OnRoleHurt = OnRoleHurtCallback;
        m_Attack.SetRoleFSMMgr(CurrRoleFSMMgr);
    }

    private void ResetState()
    {
        if (CurrRoleType == RoleType.Monster)
        {
            ToIdle(RoleIdleState.IdleFight);
        }
        else
        {
            if (CurrRoleType == RoleType.OtherPlayer && CurrRoleInfo.CurrHP <= 0)
            {
                ToDie(true);
            }
            else
            {
                ToIdle(RoleIdleState.IdleNormal);
            }
        }

        if (CharacterController != null)
        {
            CharacterController.enabled = true;
        }
    }

    private void OnRoleDestroyCallback()
    {
        if (CurrRoleType == RoleType.Monster)
        {
            RecyclePoolMgr.Instance.Despawn(PoolType.Monster, transform);
            ToIdle(RoleIdleState.IdleFight);
        }

        DespawnHeadBar();

        if (CharacterController != null)
        {
            CharacterController.enabled = false;
        }
    }

    public void RoleRecycle()
    {
        if (CurrRoleType == RoleType.Monster)
        {
            RecyclePoolMgr.Instance.Despawn(PoolType.Monster, transform);
        }
        else if (CurrRoleType == RoleType.OtherPlayer)
        {
            RecyclePoolMgr.Instance.Despawn(PoolType.Player, transform);
        }

        DespawnHeadBar();

        if (CharacterController != null)
        {
            CharacterController.enabled = false;
        }
    }

    public void DespawnHeadBar()
    {
        if (m_HeadBar != null)
        {
            RecyclePoolMgr.Instance.Despawn(PoolType.UI, m_HeadBar.transform);
        }
    }

    private void OnRoleDieCallback()
    {
        LockEnemy = null;

        if(OnRoleDie != null)
        {
            OnRoleDie(this);
        }
    }

    private void OnRoleHurtCallback()
    {
        if(roleHeadBarView != null)
        {
            roleHeadBarView.Hurt(0, (float)CurrRoleInfo.CurrHP / CurrRoleInfo.MaxHP);
        }

        if (CurrRoleType == RoleType.MainPlayer)
        {
            if (OnHPChange != null)
            {
                OnHPChange(ValueChangeType.Subtrack);
            }
        }
    }

    void Update()
    {
        if (CurrRoleFSMMgr != null)
            CurrRoleFSMMgr.OnUpdate();

        if (CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die) return;

        if (!m_Init)
        {
            m_Init = true;
            ResetState();
        }

        //如果角色没有AI 直接返回
        if (CurrRoleAI == null) return;
        CurrRoleAI.DoAI();

        if (CharacterController == null) return;

        //让角色贴着地面
        if (CharacterController.enabled && !CharacterController.isGrounded)
        {
            Vector3 orginVect = transform.position;
            CharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }

        if (CurrRoleType == RoleType.MainPlayer)
        {
            CameraAutoFollow();
            AutoSmallMap();
        }
    }

    private void AutoSmallMap()
    {
        if (SmallMapHelper.Instance == null || UIMainCitySmallMapView.Instance == null) return;

        SmallMapHelper.Instance.transform.position = transform.position;
        UIMainCitySmallMapView.Instance.transform.localPosition = new Vector3(SmallMapHelper.Instance.transform.localPosition.x * -512, SmallMapHelper.Instance.transform.localPosition.z * -512, 1);
        UIMainCitySmallMapView.Instance.SmallMapArr.transform.eulerAngles = new Vector3(0,0, -transform.eulerAngles.y);
    }

    public void Born(Vector3 pos)
    {
        BornPoint = pos;
        transform.position = pos;
        InitHeadBar();
        m_Init = false;
    }

    /// <summary>
    /// 初始化头顶UI条
    /// </summary>
    private void InitHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        if (CurrRoleInfo == null) return;
        if (m_HeadBarPos == null) return;

        //克隆预设
        //m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "RoleHeadBar");
        RecyclePoolMgr.Instance.SpawnOrLoadByAssetBundle(PoolType.UI, "Download/Prefab/UIPrefab/UIOther/RoleHeadBar", (Transform headBarTransform)=>
        {
            m_HeadBar = headBarTransform.gameObject;
            m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;
            m_HeadBar.transform.localScale = Vector3.one;
            m_HeadBar.transform.localPosition = Vector3.zero;

            roleHeadBarView = m_HeadBar.GetComponent<RoleHeadBarView>();
            //给预设赋值
            roleHeadBarView.Init(m_HeadBarPos, CurrRoleInfo.RoleNickName, (float)CurrRoleInfo.CurrHP / CurrRoleInfo.MaxHP, isShowHPBar: (CurrRoleType == RoleType.MainPlayer ? false : true));

        });
    }


    #region 控制角色方法

    public void ToResurgence(RoleIdleState roleIdleState = RoleIdleState.IdleNormal)
    {
        CurrRoleInfo.CurrHP = CurrRoleInfo.MaxHP;
        CurrRoleInfo.CurrMP = CurrRoleInfo.MaxMP;

        LockEnemy = null;
        ToIdle(roleIdleState);
        InitHeadBar();
        if(CharacterController != null)
        {
            CharacterController.enabled = true;
        }
    }

    public void ToIdle(RoleIdleState idleState = RoleIdleState.IdleNormal)
    {
        if (idleState == RoleIdleState.IdleFight)
        {
            PreIdleFightTime = Time.time;
        }
        else
        {
            PreIdleFightTime = 0;
        }

        CurrRoleFSMMgr.ToRoleIdleState = idleState;
        CurrRoleFSMMgr.ChangeState(RoleState.Idle);
    }

    /// <summary>
    /// 临时测试
    /// </summary>
    public void ToRun()
    {
        CurrRoleFSMMgr.ChangeState(RoleState.Run);
    }

    private Path m_Path = null;

    public void MoveTo(Vector3 targetPos)
    {
        if (CurrRoleFSMMgr == null) return;
        if (CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die) return;
        if (CurrRoleFSMMgr.IsRigidty) return;
        //如果目标点不是原点 进行移动
        if (targetPos == Vector3.zero) return;
        TargetPos = targetPos;
        if (m_Seeker == null)
        {
            CurrRoleFSMMgr.ChangeState(RoleState.Idle);
            return;
        }

        if(m_Seeker.IsDone())
        {
            m_Seeker.StartPath(transform.position, TargetPos, (Path p) =>
            {
                m_Path = p;
                if (!p.error)
                {
                    AStarPath = (ABPath)p;
                    if (Vector3.Distance(AStarPath.endPoint, new Vector3(AStarPath.originalEndPoint.x, AStarPath.endPoint.y, AStarPath.originalEndPoint.z)) > 0.5f)
                    {
                        AppDebug.Log("不能到达目标点");
                        AStarPath = null;
                    }
                    else
                    {
                        AttemptSendPVPMove(TargetPos, AStarPath.vectorPath);
                        CurrRoleFSMMgr.ChangeState(RoleState.Run);
                    }
                    CurrRoleFSMMgr.CurrRoleCtrl.AStarCurrWavePointIndex = 1;
                }
                else
                { 
                    AppDebug.Log("寻路出错");
                    AStarPath = null;
                }
            });
        }
    }

    private void AttemptSendPVPMove(Vector3 targetPos, List<Vector3> path)
    {
        if(CurrRoleType == RoleType.MainPlayer && SceneMgr.Instance.CurrPlayType == PlayType.PVP)
        {
            float pathTotalDis = GameUtil.GetTotalDistance(path);

            WorldMap_CurrRoleMoveProto proto = new WorldMap_CurrRoleMoveProto();
            proto.ServerTime = GlobalInit.Instance.GetCurrServetTime();
            proto.TargetPosX = targetPos.x;
            proto.TargetPosY = targetPos.y;
            proto.TargetPosZ = targetPos.z;
            proto.NeedTime = (int)(pathTotalDis / Speed * 1000);
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }

    public bool ToAttack(RoleAttackType type, int skillId)
    {
        return m_Attack.ToAttack(type, skillId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="attackValue">受到的攻击力</param>
    /// <param name="delay">延迟时间</param>
    public void ToHurt(RoleTransferAttackInfo roleTransferAttackInfo)
    {
        StartCoroutine(m_Hurt.ToHurt(roleTransferAttackInfo));
    }

    public void ToDie(bool isDied = false)
    {
        AlreadyDied = isDied;
        if (!AlreadyDied)
        {
            string name = string.Empty;
            if (CurrRoleType == RoleType.Monster)
            {
                RoleInfoMonster roleInfoMonster = CurrRoleInfo as RoleInfoMonster;
                if(roleInfoMonster != null && roleInfoMonster.spriteEntity != null)
                {
                    name = roleInfoMonster.spriteEntity.DieAudioName;
                }
            }
            else
            {
                RoleInfoMainPlayer roleInfoMainPlayer = CurrRoleInfo as RoleInfoMainPlayer;
                if (roleInfoMainPlayer != null)
                {
                    JobEntity jobEntity = JobDBModel.Instance.Get(roleInfoMainPlayer.JobId);
                    if(jobEntity != null)
                    {
                        name = jobEntity.DieAudioName;
                    }
                }
            }
            AudioEffectMgr.Instance.Play(name, transform.position, true);
        }

        CurrRoleInfo.CurrHP = 0;
        CurrRoleFSMMgr.ChangeState(RoleState.Die);
    }

    public void ToSkillAttack(int skillId)
    {
        bool isSuccess = GlobalInit.Instance.CurrPlayer.ToAttack(RoleAttackType.SkillAttack, skillId);
        if (isSuccess)
        {
            GlobalInit.Instance.MainPlayerInfo.SetSkillCD(skillId);
            UIMainCitySkillView.Instance.BeganCD(skillId);
        }
    }

    #endregion

    #region CameraAutoFollow 摄像机自动跟随
    /// <summary>
    /// 摄像机自动跟随
    /// </summary>
    private void CameraAutoFollow()
    {
        if (CameraCtrl.Instance == null) return;
        AudioBackGroundMgr.Instance.transform.position = transform.position;
        CameraCtrl.Instance.transform.position = gameObject.transform.position;
        CameraCtrl.Instance.AutoLookAt(gameObject.transform.position);
    }
    #endregion
}