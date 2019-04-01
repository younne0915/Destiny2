//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:55:06
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 死亡状态
/// </summary>
public class RoleStateDie : RoleStateAbstract
{
    public Action OnRoleDie;
    public Action OnRoleDestroy;

    private float m_DieDelayTime = 0;
    private bool m_IsDestroy = false;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateDie(RoleFSMMgr roleFSMMgr,Action roleDie, Action roleDestroy) : base(roleFSMMgr)
    {
        OnRoleDie = roleDie;
        OnRoleDestroy = roleDestroy;
    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToDie.ToString(), true);
        if(OnRoleDie != null)
        {
            OnRoleDie();
        }
        m_DieDelayTime = 0;

        Transform effectTransform = RecyclePoolMgr.Instance.Spawn( PoolType.Effect, ResourLoadType.AssetBundle, "Effect/Effect_PenXue");
        effectTransform.position = CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
        effectTransform.rotation = CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;

        m_IsDestroy = false;
    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!m_IsDestroy)
        {
            m_DieDelayTime += Time.deltaTime;
            if (m_DieDelayTime > 3)
            {
                if (OnRoleDestroy != null)
                {
                    OnRoleDestroy();
                }
                m_IsDestroy = true;
            }
        }
        
        CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Die.ToString()))
        {
            if(CurrState != RoleAnimatorState.Die)
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Die);
            }
        }
        else
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToDie.ToString(), true);
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.None);
        }
    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToDie.ToString(), false);
    }
}