//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-02 20:22:50
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using Pathfinding;
using System;

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
    [SerializeField]
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
    [SerializeField]
    public float Speed = 10f;

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

    [HideInInspector]
    public ABPath AStarPath;

    public int AStarCurrWavePointIndex = 1;

    //==============================战斗相关====================================
    public RoleHurt m_Hurt;

    public RoleAttack m_Attack;

    [HideInInspector]
    public RoleAttackInfo CurrAttackInfo;

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

    public delegate void OnValueChangeHandler(ValueChangeType type);
    public OnValueChangeHandler OnHPChange;
    public OnValueChangeHandler OnMPChange;

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
        //m_Attack = new RoleAttack(CurrRoleFSMMgr);
        m_Attack.SetRoleFSMMgr(CurrRoleFSMMgr);

        if (CurrRoleType == RoleType.Monster)
        {
            ToIdle(RoleIdleState.IdleFight);
        }
        else
        {
            ToIdle(RoleIdleState.IdleNormal);
        }
    }

    private void OnRoleDestroyCallback()
    {
        RecyclePoolMgr.Instance.Despawn(PoolType.Monster, transform);

        if (CharacterController != null)
        {
            CharacterController.enabled = true;
        }

        if (CurrRoleType == RoleType.Monster)
        {
            ToIdle(RoleIdleState.IdleFight);
        }
        else
        {
            ToIdle(RoleIdleState.IdleNormal);
        }

        if (roleHeadBarView != null)
        {
            Destroy(roleHeadBarView.gameObject);
        }
    }

    private void OnRoleDieCallback()
    {
        if(CharacterController != null)
        {
            CharacterController.enabled = false;
        }

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
            if(OnHPChange != null)
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

        //如果角色没有AI 直接返回
        if (CurrRoleAI == null) return;
        CurrRoleAI.DoAI();

        if (CharacterController == null) return;

        //让角色贴着地面
        if (!CharacterController.isGrounded)
        {
            CharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }

        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Item")))
            {
                BoxCtrl boxCtrl = hit.collider.GetComponent<BoxCtrl>();
                if (boxCtrl != null)
                {
                    boxCtrl.Hit();
                }
            }
        }

        //让角色贴着地面
        if (!CharacterController.isGrounded)
        {
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
        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "RoleHeadBar");
        m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;
        m_HeadBar.transform.localScale = Vector3.one;
        m_HeadBar.transform.localPosition = Vector3.zero;

        roleHeadBarView = m_HeadBar.GetComponent<RoleHeadBarView>();

        //给预设赋值
        roleHeadBarView.Init(m_HeadBarPos, CurrRoleInfo.RoleNickName, (float)CurrRoleInfo.CurrHP / CurrRoleInfo.MaxHP, isShowHPBar: (CurrRoleType == RoleType.MainPlayer ? false : true));
    }


    #region 控制角色方法

    public void ToResurgence(RoleIdleState roleIdleState = RoleIdleState.IdleNormal)
    {
        if(CharacterController != null)
        {
            CharacterController.enabled = true;
        }

        CurrRoleInfo.CurrHP = CurrRoleInfo.MaxHP;
        CurrRoleInfo.CurrMP = CurrRoleInfo.MaxMP;
        LockEnemy = null;
        ToIdle(roleIdleState);
    }

    public void ToIdle(RoleIdleState idleState = RoleIdleState.IdleNormal)
    {
        //if(CurrRoleType == RoleType.MainPlayer)
        //{
        //    AppDebug.LogError("aaaa");
        //}

        if(idleState == RoleIdleState.IdleFight)
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

    public void MoveTo(Vector3 targetPos)
    {
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
        if (m_Seeker.IsDone())
        {
            m_Seeker.StartPath(transform.position, TargetPos, (Path p) =>
            {
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

    public void ToAttackByIndex(RoleAttackType type, int index)
    {
        //if (LockEnemy == null) return;
        //CurrRoleFSMMgr.ChangeState(RoleState.Attack);

        ////暂时写死
        //LockEnemy.ToHurt(100, 0.5f);
        m_Attack.ToAttackByIndex(type, index);
    }

    public bool ToAttack(RoleAttackType type, int skillId)
    {
        //if (LockEnemy == null) return;
        //CurrRoleFSMMgr.ChangeState(RoleState.Attack);

        ////暂时写死
        //LockEnemy.ToHurt(100, 0.5f);
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

    public void ToDie()
    {
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

    #region OnDestroy 销毁
    /// <summary>
    /// 销毁
    /// </summary>
    void OnDestroy()
    {
        if (m_HeadBar != null)
        {
            Destroy(m_HeadBar);
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

        CameraCtrl.Instance.transform.position = gameObject.transform.position;
        CameraCtrl.Instance.AutoLookAt(gameObject.transform.position);
    }
    #endregion
}