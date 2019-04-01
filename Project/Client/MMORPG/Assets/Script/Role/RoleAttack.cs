using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoleAttack
{
    public List<RoleAttackInfo> PhyAttackInfoList;
    public List<RoleAttackInfo> SkillAttackInfoList;

    private RoleFSMMgr m_CurrRoleFSMMgr = null;
    private RoleCtrl m_CurrRoleCtrl = null;

    private RoleStateAttack m_RoleStateAttack;
    private List<RoleCtrl> m_EnemyList = new List<RoleCtrl>();
    private List<RoleCtrl> m_SearchList = new List<RoleCtrl>();

    public int FollowSkillId = -1;

    public void SetRoleFSMMgr(RoleFSMMgr fsm)
    {
        m_CurrRoleFSMMgr = fsm;
        m_CurrRoleCtrl = fsm.CurrRoleCtrl;
    }

    public void ToAttackByIndex(RoleAttackType type, int index)
    {
        if(m_RoleStateAttack == null)
        {
            m_RoleStateAttack = m_CurrRoleFSMMgr.GetRoleState(RoleState.Attack) as RoleStateAttack;
        }

        if (m_CurrRoleFSMMgr.IsRigidty) return;
        RoleAttackInfo info = GetRoleAttackInfoByIndex(type, index);

        if(info != null)
        {
            m_CurrRoleFSMMgr.CurrRoleCtrl.CurrAttackInfo = info;
            Transform effectTransform = RecyclePoolMgr.Instance.Spawn(PoolType.Effect, ResourLoadType.AssetBundle, string.Format("Effect/{0}", info.EffectName));
            effectTransform.position = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
            effectTransform.rotation = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;
            RecyclePoolMgr.Instance.Despawn(PoolType.Effect, effectTransform, info.EffectLifeTime);
        }


        if (info != null && info.IsDoCameraShake)
        {
            CameraShake(info.CameraShakeDelay);
        }

        if (type == RoleAttackType.PhyAttack)
        {
            m_RoleStateAttack.AnimatorConditionName = ToAnimatorCondition.ToPhyAttack;
        }
        else if(type == RoleAttackType.SkillAttack)
        {
            m_RoleStateAttack.AnimatorConditionName = ToAnimatorCondition.ToSkill;
        }
        m_RoleStateAttack.AnimatorConditionValue = index;
        m_RoleStateAttack.AnimatorState = ConvertToAnimatorState(type, index);
        m_CurrRoleFSMMgr.ChangeState(RoleState.Attack);
    }

    public bool ToAttack(RoleAttackType type, int skillId)
    {
        if (m_RoleStateAttack == null)
        {
            m_RoleStateAttack = m_CurrRoleFSMMgr.GetRoleState(RoleState.Attack) as RoleStateAttack;
        }

        if (m_CurrRoleFSMMgr.IsRigidty)
        {
            if(type == RoleAttackType.SkillAttack)
            {
                FollowSkillId = skillId;
            }
            return false;
        }

        #region 数值相关
        //1.
        if (m_CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer || m_CurrRoleCtrl.CurrRoleType == RoleType.Monster)
        {
            SkillEntity skillEntity = SkillDBModel.Instance.Get(skillId);
            if (skillEntity == null) return false;
            SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetSkillLevelEntityBySkillIdAndLevel(skillId, m_CurrRoleCtrl.CurrRoleInfo.GetSkillLevel(skillId));
            if (skillLevelEntity == null) return false;

            if (skillLevelEntity.SpendMP > m_CurrRoleCtrl.CurrRoleInfo.CurrMP)
            {
                AppDebug.LogError("魔不足");
                return false;
            }

            m_CurrRoleCtrl.CurrRoleInfo.CurrMP -= skillLevelEntity.SpendMP;
            if(m_CurrRoleCtrl.CurrRoleInfo.CurrMP < 0)
            {
                m_CurrRoleCtrl.CurrRoleInfo.CurrMP = 0;
            }

            if (m_CurrRoleCtrl.OnMPChange != null)
            {
                m_CurrRoleCtrl.OnMPChange(ValueChangeType.Subtrack);
            }

            m_EnemyList.Clear();

            if (m_CurrRoleCtrl.CurrRoleType == RoleType.MainPlayer)
            {
                m_SearchList.Clear();
                int attackTargetCnt = skillEntity.AttackTargetCount;
                if(attackTargetCnt == 1)
                {
                    if(m_CurrRoleCtrl.LockEnemy != null)
                    {
                        m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                    }
                    else
                    {
                        Collider[] arr = Physics.OverlapSphere(m_CurrRoleCtrl.transform.position, skillEntity.AreaAttackRadius, 1 << LayerMask.NameToLayer("Role"));
                        if(arr != null && arr.Length > 0)
                        {
                            for (int i = 0; i < arr.Length; i++)
                            {
                                if (arr[i].GetComponent<RoleCtrl>().CurrRoleInfo.RoleId == GlobalInit.Instance.MainPlayerInfo.RoleId) continue;
                                m_SearchList.Add(arr[i].GetComponent<RoleCtrl>());
                            }
 
                            m_SearchList.Sort((RoleCtrl r1, RoleCtrl r2) =>
                            {
                                int ret = 0;
                                float dis1 = Vector3.Distance(r1.transform.position, m_CurrRoleCtrl.transform.position);
                                float dis2 = Vector3.Distance(r1.transform.position, m_CurrRoleCtrl.transform.position);

                                if (dis1 < dis2)
                                {
                                    ret = -1;
                                }
                                else
                                {
                                    ret = 1;
                                }
                                return ret;
                            });

                            m_EnemyList.Add(m_SearchList[0]);
                        }
                    }
                }
                else
                {

                    Collider[] arr = Physics.OverlapSphere(m_CurrRoleCtrl.transform.position, skillEntity.AreaAttackRadius, 1 << LayerMask.NameToLayer("Role"));
                    if (arr != null && arr.Length > 0)
                    {
                        for (int i = 0; i < arr.Length; i++)
                        {
                            if (arr[i].GetComponent<RoleCtrl>().CurrRoleInfo.RoleId == GlobalInit.Instance.MainPlayerInfo.RoleId) continue;
                            m_SearchList.Add(arr[i].GetComponent<RoleCtrl>());
                        }

                        m_SearchList.Sort((RoleCtrl r1, RoleCtrl r2) =>
                        {
                            int ret = 0;
                            float dis1 = Vector3.Distance(r1.transform.position, m_CurrRoleCtrl.transform.position);
                            float dis2 = Vector3.Distance(r1.transform.position, m_CurrRoleCtrl.transform.position);

                            if (dis1 < dis2)
                            {
                                ret = -1;
                            }
                            else
                            {
                                ret = 1;
                            }
                            return ret;
                        });
                    }

                    int needAttack = attackTargetCnt;
                    if (m_CurrRoleCtrl.LockEnemy != null)
                    {
                        m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                        needAttack--;
                    }
                    else
                    {
                        if(m_SearchList.Count > 0)
                        {
                            m_CurrRoleCtrl.LockEnemy = m_SearchList[0];
                            m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                        }
                    }

                    for (int i = 0; i < m_SearchList.Count; i++)
                    {
                        if (m_CurrRoleCtrl.LockEnemy.CurrRoleInfo.RoleId == m_SearchList[i].CurrRoleInfo.RoleId)
                        {
                            needAttack++;
                        }
                        if (i + 1 > needAttack) break;
                        m_EnemyList.Add(m_SearchList[i]);
                    }
                }
            }
            else if(m_CurrRoleCtrl.CurrRoleType == RoleType.Monster)
            {
                if (m_CurrRoleCtrl.LockEnemy != null)
                {
                    m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
                }
            }

            for (int i = 0; i < m_EnemyList.Count; i++)
            {
                RoleTransferAttackInfo roleTransferAttackInfo = CalculateHurtValue(m_CurrRoleCtrl.LockEnemy, skillLevelEntity);
                m_EnemyList[i].ToHurt(roleTransferAttackInfo);
            }
        }

        #endregion

        #region 角色动画相关

        RoleAttackInfo info = GetRoleAttackInfo(type, skillId);
        if (info == null) return false;

        m_CurrRoleFSMMgr.CurrRoleCtrl.CurrAttackInfo = info;

        Transform effectTransform = RecyclePoolMgr.Instance.Spawn(PoolType.Effect, ResourLoadType.AssetBundle, string.Format("Effect/{0}", info.EffectName));
        effectTransform.position = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
        effectTransform.rotation = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;
        RecyclePoolMgr.Instance.Despawn(PoolType.Effect, effectTransform, info.EffectLifeTime);

        if (info != null && info.IsDoCameraShake)
        {
            CameraShake(info.CameraShakeDelay);
        }

        if (type == RoleAttackType.PhyAttack)
        {
            m_RoleStateAttack.AnimatorConditionName = ToAnimatorCondition.ToPhyAttack;
        }
        else if (type == RoleAttackType.SkillAttack)
        {
            m_RoleStateAttack.AnimatorConditionName = ToAnimatorCondition.ToSkill;
        }
        m_RoleStateAttack.AnimatorConditionValue = info.Index;
        m_RoleStateAttack.AnimatorState = ConvertToAnimatorState(type, info.Index);
        m_CurrRoleFSMMgr.ChangeState(RoleState.Attack);

#endregion

        return true;
    }

    public RoleAttackInfo GetRoleAttackInfoByIndex(RoleAttackType type, int index)
    {
        List<RoleAttackInfo> list = null;
        if(type == RoleAttackType.PhyAttack)
        {
            list = PhyAttackInfoList;
        }
        else
        {
            list = SkillAttackInfoList;
        }

        if(list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Index == index) return list[i];
            }
        }
        return null;
    }

    public RoleAttackInfo GetRoleAttackInfo(RoleAttackType type, int skillId)
    {
        List<RoleAttackInfo> list = null;
        if (type == RoleAttackType.PhyAttack)
        {
            list = PhyAttackInfoList;
        }
        else
        {
            list = SkillAttackInfoList;
        }

        if (list != null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].SkillId == skillId) return list[i];
            }
        }
        return null;
    }

    private RoleAnimatorState ConvertToAnimatorState(RoleAttackType type, int index)
    {
        string stateStr = string.Empty;
        if (type == RoleAttackType.PhyAttack)
        {
            stateStr = string.Format("PhyAttack{0}", index);
        }
        else if (type == RoleAttackType.SkillAttack)
        {
            stateStr = string.Format("Skill{0}", index);
        }
        return ConvertUtil.ConvertToEnum<RoleAnimatorState>(stateStr);
        //return (RoleAnimatorState)Enum.Parse(typeof(RoleAnimatorState), stateStr);
    }

    public void CameraShake(float delay = 0, float duration = 0.5f, float strength = 1, int vibrato = 10)
    {
        if(CameraCtrl.Instance != null)
        {
            CameraCtrl.Instance.CameraShake(delay, duration, strength, vibrato);
        }
    }

    private RoleTransferAttackInfo CalculateHurtValue(RoleCtrl enemy, SkillLevelEntity skillLevelEntity)
    {
        if (enemy == null || skillLevelEntity == null) return null;
        SkillEntity skillEntity = SkillDBModel.Instance.Get(skillLevelEntity.SkillId);
        if (skillEntity == null) return null;

        RoleTransferAttackInfo roleTransferAttackInfo = new RoleTransferAttackInfo();
        roleTransferAttackInfo.AttackRoleId = m_CurrRoleCtrl.CurrRoleInfo.RoleId;
        roleTransferAttackInfo.BeAttackRoleId = enemy.CurrRoleInfo.RoleId;
        roleTransferAttackInfo.SkillId = skillLevelEntity.SkillId;
        roleTransferAttackInfo.SkillLevel = skillLevelEntity.Level;
        roleTransferAttackInfo.IsAbnormal = skillLevelEntity.AbnormalRatio == 1;
        //1.攻击数值 = 攻击方的综合战斗力 * (技能的伤害倍率 * 0.01f)
        float attackValue = m_CurrRoleCtrl.CurrRoleInfo.Fighting * (skillLevelEntity.HurtValueRate * 0.01f);
        //2.基础伤害 = 攻击数值 * 攻击数值 / （攻击数值 + 被攻击方的防御）
        float baseHurt = attackValue * attackValue / (attackValue + enemy.CurrRoleInfo.Defense);
        //3.暴击概率 = 0.05f + （攻击方暴击/（攻击方暴击+防御方抗性））* 0.1f
        float cri = 0.05f +( m_CurrRoleCtrl.CurrRoleInfo.Cri / (m_CurrRoleCtrl.CurrRoleInfo.Cri + enemy.CurrRoleInfo.Res)) * 0.1f;
        //暴击概率 = 暴击概率>0.5f？0.5f:暴击概率
        cri = cri > 0.5f ? 0.5f : cri;
        //4.是否暴击 = 0-1的随机数 <= 暴击概率
        bool isCri = UnityEngine.Random.Range(0f, 1f) <= cri;
        //5.暴击攻击伤害倍率 = 有暴击？1.5f：1f
        float criHurt = isCri ? 1.5f : 1;
        //6.随机数 = 0.9f-1.1f
        float random = UnityEngine.Random.Range(0.9f, 1.1f);
        int hurtValue = Mathf.RoundToInt(baseHurt * criHurt * random);
        hurtValue = hurtValue < 1 ? 1 : hurtValue;

        roleTransferAttackInfo.HurtValue = hurtValue;
        roleTransferAttackInfo.IsCri = isCri;

        return roleTransferAttackInfo;
    }
}
