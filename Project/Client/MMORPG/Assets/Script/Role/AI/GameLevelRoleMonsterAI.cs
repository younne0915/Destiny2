//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:59:13
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 怪AI
/// </summary>
public class GameLevelRoleMonsterAI : IRoleAI
{
    /// <summary>
    /// 当前角色控制器
    /// </summary>
    public RoleCtrl CurrRole
    {
        get;
        set;
    }

    /// <summary>
    /// 下次巡逻时间
    /// </summary>
    private float m_NextPatrolTime = 0f;

    /// <summary>
    /// 下次攻击时间
    /// </summary>
    private float m_NextAttackTime = 0f;

    private RoleInfoMonster m_Info;

    private RaycastHit m_HitInfo;

    private float m_NextThinkTime = 0;

    private bool m_IsDai = false;

    public GameLevelRoleMonsterAI(RoleCtrl roleCtrl, RoleInfoMonster monsterInfoBase)
    {
        CurrRole = roleCtrl;
        m_Info = monsterInfoBase;
    }

    public void DoAI()
    {
        if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die || CurrRole.CurrRoleFSMMgr.IsRigidty) return;

        if (CurrRole.LockEnemy == null)
        {
            //如果是待机状态
            if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Idle)
            {
                if (Time.time > m_NextPatrolTime)
                {
                    Vector3 targetPos = new Vector3(CurrRole.BornPoint.x + UnityEngine.Random.Range(CurrRole.PatrolRange * -1, CurrRole.PatrolRange), CurrRole.BornPoint.y, CurrRole.BornPoint.z + UnityEngine.Random.Range(CurrRole.PatrolRange * -1, CurrRole.PatrolRange));
                    Vector3 orgionPos = new Vector3(targetPos.x, targetPos.y + 100, targetPos.z);
                    if (Physics.Raycast(orgionPos, new Vector3(0, -100, 0), out m_HitInfo, 1000, 1 << LayerMask.NameToLayer("RegionMask")))
                    {
                        return;
                    }

                    m_NextPatrolTime = Time.time + UnityEngine.Random.Range(5f, 10f);
                    //进行巡逻
                    CurrRole.MoveTo(targetPos);
                }
            }

            ////如果主角在怪的视野范围内
            if (Vector3.Distance(CurrRole.transform.position, GlobalInit.Instance.CurrPlayer.transform.position) <= CurrRole.ViewRange)
            {
                CurrRole.LockEnemy = GlobalInit.Instance.CurrPlayer;
            }
        }
        else
        {
            if (CurrRole.LockEnemy.CurrRoleInfo.CurrHP <= 0)
            {
                CurrRole.LockEnemy = null;
                return;
            }

            if (Time.time > m_NextThinkTime + UnityEngine.Random.Range(2, 2.5f))
            {
                m_NextThinkTime = Time.time;
                m_IsDai = true;
                CurrRole.ToIdle( RoleIdleState.IdleFight);
            }

            if (m_IsDai)
            {
                if(Time.time > m_NextThinkTime + UnityEngine.Random.Range(0.2f, 0.4f))
                {
                    m_IsDai = false;

                }
                else
                {
                    return;
                }
            }

            if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum != RoleState.Idle) return;

            //1.如果我和锁定敌人的距离 超过了我的视野范围 则取消锁定
            if (Vector3.Distance(CurrRole.transform.position, GlobalInit.Instance.CurrPlayer.transform.position) > CurrRole.ViewRange)
            {
                CurrRole.LockEnemy = null;
                return;
            }

            int userSkillId = 0;
            RoleAttackType roleAttackType = RoleAttackType.PhyAttack;
            if (UnityEngine.Random.Range(0, 100) <= m_Info.spriteEntity.PhysicalAttackRate)
            {
                int[] arr = m_Info.spriteEntity.UsedPhyAttackArr;
                if(arr != null && arr.Length > 0)
                {
                    userSkillId = arr[UnityEngine.Random.Range(0, arr.Length)];
                    roleAttackType = RoleAttackType.PhyAttack;
                }
            }
            else
            {
                int[] arr = m_Info.spriteEntity.UsedSkillListArr;
                if (arr != null && arr.Length > 0)
                {
                    userSkillId = arr[UnityEngine.Random.Range(0, arr.Length)];
                    roleAttackType = RoleAttackType.SkillAttack;
                }
            }

            CurrRole.transform.LookAt(CurrRole.LockEnemy.transform);

            SkillEntity skillEntity = SkillDBModel.Instance.Get(userSkillId);
            if (skillEntity == null) return;

            //2.如果在技能攻击范围内 直接攻击
            if (Vector3.Distance(CurrRole.transform.position, GlobalInit.Instance.CurrPlayer.transform.position) <= skillEntity.AttackRange)
            {
                if (Time.time > m_NextAttackTime && CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum != RoleState.Attack)
                {
                    m_NextAttackTime = Time.time + UnityEngine.Random.Range(0f, 1f) + m_Info.spriteEntity.DelaySec_Attack;
                    CurrRole.ToAttack(roleAttackType, userSkillId);
                }
            }
            else
            {
                //3.如果在我的视野范围之内 在技能的攻击范围外 进行追击
                Vector3 targetPos = GameUtil.GetRandomPos(CurrRole.transform.position, CurrRole.LockEnemy.transform.position, skillEntity.AttackRange);
                Vector3 orgionPos = new Vector3(targetPos.x, targetPos.y + 100, targetPos.z);
                if (Physics.Raycast(orgionPos, new Vector3(0, -100, 0), out m_HitInfo, 1000, 1 << LayerMask.NameToLayer("RegionMask")))
                {
                    return;
                }
                CurrRole.MoveTo(targetPos);
            }
        }
    }
}