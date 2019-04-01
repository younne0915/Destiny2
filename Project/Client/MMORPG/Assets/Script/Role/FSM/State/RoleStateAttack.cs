//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:55:30
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 攻击状态
/// </summary>
public class RoleStateAttack : RoleStateAbstract
{
    public ToAnimatorCondition AnimatorConditionName;

    public int AnimatorConditionValue;

    public ToAnimatorCondition OldAnimatorConditionName;

    public RoleAnimatorState AnimatorState;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateAttack(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(AnimatorConditionName.ToString(), AnimatorConditionValue);

        if (CurrRoleFSMMgr.CurrRoleCtrl.LockEnemy != null)
        {
            CurrRoleFSMMgr.CurrRoleCtrl.transform.LookAt(new Vector3(CurrRoleFSMMgr.CurrRoleCtrl.LockEnemy.transform.position.x, CurrRoleFSMMgr.CurrRoleCtrl.transform.position.y, CurrRoleFSMMgr.CurrRoleCtrl.LockEnemy.transform.position.z));
        }
        OldAnimatorConditionName = AnimatorConditionName;
        CurrRoleFSMMgr.IsRigidty = true;
    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(AnimatorState.ToString()))
        {
            if (!CheckIsAttackState())
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)AnimatorState);
            }

            //如果动画执行了一遍 就切换待机
            if (CurrRoleAnimatorStateInfo.normalizedTime > 1)
            {
                CurrRoleFSMMgr.CurrRoleCtrl.ToIdle(RoleIdleState.IdleFight);
                CurrRoleFSMMgr.IsRigidty = false;
            }
        }
        else
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(AnimatorConditionName.ToString(), AnimatorConditionValue);
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
        }
    }

    private bool CheckIsAttackState()
    {
        if(CurrState != RoleAnimatorState.PhyAttack1 && CurrState != RoleAnimatorState.PhyAttack2 &&
            CurrState != RoleAnimatorState.PhyAttack3 && CurrState != RoleAnimatorState.Skill1 &&
            CurrState != RoleAnimatorState.Skill2 && CurrState != RoleAnimatorState.Skill3 &&
            CurrState != RoleAnimatorState.Skill4 && CurrState != RoleAnimatorState.Skill5 &&
            CurrState != RoleAnimatorState.Skill6)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(OldAnimatorConditionName.ToString(), 0);
        CurrRoleFSMMgr.IsRigidty = false;
    }
}