//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:54:44
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 受伤状态
/// </summary>
public class RoleStateHurt : RoleStateAbstract
{
    private float m_HurtAnimPlayTime = -1;

    private bool m_BeganHurtAnim = false;

    private float m_ShowHurtEffectDelaySecond = -1;
    public float ShowHurtEffectDelaySecond
    {
        get { return m_ShowHurtEffectDelaySecond; }
        set
        {
            if(value >= 0)
            {
                m_ShowHurtEffectDelaySecond = value;
                m_HurtAnimPlayTime = Time.time + m_ShowHurtEffectDelaySecond;
            }
            else
            {
                m_ShowHurtEffectDelaySecond = -1;
                m_HurtAnimPlayTime = -1;
            }
        }
    }
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleFSMMgr">有限状态机管理器</param>
    public RoleStateHurt(RoleFSMMgr roleFSMMgr) : base(roleFSMMgr)
    {

    }

    /// <summary>
    /// 实现基类 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();

        //if (CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleType == RoleType.OtherPlayer)
        //{
        //    AppDebug.LogError("hurt enter nickName : " + CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleInfo.RoleNickName + " , Time : " + Time.realtimeSinceStartup);
        //}
    }

    /// <summary>
    /// 实现基类 执行状态
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(m_HurtAnimPlayTime >= 0 && Time.time >= m_HurtAnimPlayTime)
        {
            CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToHurt.ToString(), true);
            m_HurtAnimPlayTime = -1;
            m_BeganHurtAnim = true;

            //if (CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleType == RoleType.OtherPlayer)
            //{
            //    AppDebug.LogError("m_BeganHurtAnim = true, " + Time.realtimeSinceStartup);
            //}
        }

        if (m_BeganHurtAnim)
        {
            CurrRoleAnimatorStateInfo = CurrRoleFSMMgr.CurrRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
            if (CurrRoleAnimatorStateInfo.IsName(RoleAnimatorState.Hurt.ToString()))
            {
                if (CurrState != RoleAnimatorState.Hurt)
                {
                    CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.Hurt);
                }

                //如果动画执行了一遍 就切换待机
                if (CurrRoleAnimatorStateInfo.normalizedTime > 1)
                {
                    CurrRoleFSMMgr.CurrRoleCtrl.ToIdle(RoleIdleState.IdleFight);
                }
            }
            else
            {
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToHurt.ToString(), true);
                CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAnimatorState.None);
            }
        }
    }

    /// <summary>
    /// 实现基类 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurrRoleFSMMgr.CurrRoleCtrl.Animator.SetBool(ToAnimatorCondition.ToHurt.ToString(), false);
        m_BeganHurtAnim = false;
        ShowHurtEffectDelaySecond = -1;
    }
}