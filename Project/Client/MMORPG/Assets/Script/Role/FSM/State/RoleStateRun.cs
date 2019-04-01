//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:54:29
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 跑状态
/// </summary>
public class RoleStateRun : RoleStateAbstract
{
    /// <summary>
    /// 转身速度
    /// </summary>
    private float m_RotationSpeed = 0.2f;

    /// <summary>
    /// 转身的目标方向
    /// </summary>
    private Quaternion m_TargetQuaternion;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateRun(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        m_RotationSpeed = 0;
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), true);
    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        //===========测试代码==================Start

        //if (CurrRoleFSMMgr.CurrRoleCtrl.AStarPath == null)
        //{
        //    CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        //    if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Run.ToString()) && CurrState != RoleAnimatorState.Run)
        //    {
        //        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Run);
        //    }
        //    //else
        //    //{
        //    //    CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), 0);
        //    //}
        //    return;
        //}
        //===========测试代码==================End

        CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
        if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Run.ToString()))
        {
            if(CurrState != RoleAnimatorState.Run)
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Run);
            }
        }
        else
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), true);
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.None);
        }

        if (CurrRoleFSMMgr.CurrRoleCtrl.AStarPath == null)
        {
            if (CurrRoleFSMMgr.CurrRoleCtrl.PreIdleFightTime < 0.01f)
            {
                CurrRoleFSMMgr.CurrRoleCtrl.ToIdle();
            }
            else
            {
                CurrRoleFSMMgr.CurrRoleCtrl.ToIdle(RoleIdleState.IdleFight);
            }
            return;
        }

        int index = CurrRoleFSMMgr.CurrRoleCtrl.AStarCurrWavePointIndex;
        if (index >= CurrRoleFSMMgr.CurrRoleCtrl.AStarPath.vectorPath.Count)
        {
            if (CurrRoleFSMMgr.CurrRoleCtrl.PreIdleFightTime < 0.01f)
            {
                CurrRoleFSMMgr.CurrRoleCtrl.ToIdle();
            }
            else
            {
                CurrRoleFSMMgr.CurrRoleCtrl.ToIdle(RoleIdleState.IdleFight);
            }
            return;
        }

        Vector3 pathProcessPoint = new Vector3(CurrRoleFSMMgr.CurrRoleCtrl.AStarPath.vectorPath[index].x,
            CurrRoleFSMMgr.CurrRoleCtrl.transform.position.y,
           CurrRoleFSMMgr.CurrRoleCtrl.AStarPath.vectorPath[index].z);

        Vector3 direction = pathProcessPoint - CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
        direction = direction.normalized; //归一化
        direction = direction * Time.deltaTime * CurrRoleFSMMgr.CurrRoleCtrl.Speed;
        direction.y = 0;

        //让角色缓慢转身
        if (m_RotationSpeed <= 1)
        {
            m_RotationSpeed += 10f * Time.deltaTime;
            m_TargetQuaternion = Quaternion.LookRotation(direction);
            CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation = Quaternion.Lerp(CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation, m_TargetQuaternion, m_RotationSpeed);

            if (Quaternion.Angle(CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation, m_TargetQuaternion) < 1)
            {
                m_RotationSpeed = 0;
            }
        }

        if(Vector3.Distance(CurrRoleFSMMgr.CurrRoleCtrl.transform.position, pathProcessPoint) <= direction.magnitude + 0.1f)
        {
            CurrRoleFSMMgr.CurrRoleCtrl.AStarCurrWavePointIndex++;
        }

        CurrRoleFSMMgr.CurrRoleCtrl.CharacterController.Move(direction);
    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToRun.ToString(), false);
    }
}