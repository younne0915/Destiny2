using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoleAttack
{
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

        #region 找敌人相关
        SkillEntity skillEntity = SkillDBModel.Instance.Get(skillId);
        if (skillEntity == null) return false;
        SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetSkillLevelEntityBySkillIdAndLevel(skillId, m_CurrRoleCtrl.CurrRoleInfo.GetSkillLevel(skillId));
        if (skillLevelEntity == null) return false;

        if (skillLevelEntity.SpendMP > m_CurrRoleCtrl.CurrRoleInfo.CurrMP)
        {
            AppDebug.LogError("魔不足");
            return false;
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
            if (attackTargetCnt == 1)
            {
                if (m_CurrRoleCtrl.LockEnemy != null)
                {
                    m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
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
                    if (m_SearchList.Count > 0)
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
                        continue;
                    }
                    if (i + 1 > needAttack) break;
                    m_EnemyList.Add(m_SearchList[i]);
                }
            }

            if (SceneMgr.Instance.CurrPlayType == PlayType.PVP)
            {
                //如果是PVP发送技能消息给服务器
                if (m_EnemyList.Count > 0)
                {
                    WorldMap_CurrRoleUseSkillProto proto = new WorldMap_CurrRoleUseSkillProto();
                    proto.BeAttackCount = m_EnemyList.Count;
                    proto.RolePosX = m_CurrRoleCtrl.transform.position.x;
                    proto.RolePosY = m_CurrRoleCtrl.transform.position.y;
                    proto.RolePosZ = m_CurrRoleCtrl.transform.position.z;
                    proto.RoleYAngle = m_CurrRoleCtrl.transform.eulerAngles.y;
                    proto.SkillId = skillId;
                    proto.SkillLevel = skillLevelEntity.Level;

                    proto.ItemList = new List<WorldMap_CurrRoleUseSkillProto.BeAttackItem>();
                    WorldMap_CurrRoleUseSkillProto.BeAttackItem item;
                    for (int i = 0; i < m_EnemyList.Count; i++)
                    {
                        item = new WorldMap_CurrRoleUseSkillProto.BeAttackItem();
                        item.BeAttackRoleId = m_EnemyList[i].CurrRoleInfo.RoleId;
                        proto.ItemList.Add(item);

                        //AppDebug.LogError(string.Format("发送角色攻击 : {0}, skillId : {1}", item.BeAttackRoleId, skillId));
                    }
                    NetWorkSocket.Instance.SendMsg(proto.ToArray());
                }
            }
        }
        else if (m_CurrRoleCtrl.CurrRoleType == RoleType.Monster)
        {
            if (m_CurrRoleCtrl.LockEnemy != null)
            {
                m_EnemyList.Add(m_CurrRoleCtrl.LockEnemy);
            }
        }

        if (m_EnemyList.Count == 0) return false;

        #region 数值相关

        if (SceneMgr.Instance.CurrPlayType == PlayType.PVE)
        {
            for (int i = 0; i < m_EnemyList.Count; i++)
            {
                RoleTransferAttackInfo roleTransferAttackInfo = CalculateHurtValue(m_CurrRoleCtrl.LockEnemy, skillLevelEntity);
                m_EnemyList[i].ToHurt(roleTransferAttackInfo);
            }
        }

        #endregion

        #endregion

        #region 角色动画相关
        bool isPlayAttack = true;
        if (SceneMgr.Instance.CurrPlayType == PlayType.PVE)
        {
            isPlayAttack = PlayAttack(skillId);
        }
        #endregion

        if (isPlayAttack)
        {
            m_CurrRoleCtrl.CurrRoleInfo.CurrMP -= skillLevelEntity.SpendMP;
            if (m_CurrRoleCtrl.CurrRoleInfo.CurrMP < 0)
            {
                m_CurrRoleCtrl.CurrRoleInfo.CurrMP = 0;
            }
        }

        return isPlayAttack;
    }

    public bool PlayAttack(int skillId)
    {
        if (m_RoleStateAttack == null)
        {
            m_RoleStateAttack = m_CurrRoleFSMMgr.GetRoleState(RoleState.Attack) as RoleStateAttack;
        }

        SkillEntity info = SkillDBModel.Instance.Get(skillId);
        if (info == null) return false;

        m_CurrRoleFSMMgr.CurrRoleCtrl.CurrSkillEntity = info;

        if (string.IsNullOrEmpty(info.EffectName)) return false;

         RecyclePoolMgr.Instance.SpawnOrLoadByAssetBundle(PoolType.Effect, string.Format("Download/Prefab/Effect/Role/{0}", info.EffectName), (Transform effectTransform) => 
        {
            effectTransform.position = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
            effectTransform.rotation = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;
            RecyclePoolMgr.Instance.Despawn(PoolType.Effect, effectTransform, info.EffectLifeTime);
        });

        if (info.IsDoCameraShake == 1)
        {
            CameraShake(info.CameraShakeDelay);
        }

        if (info.IsPhyAttack == 1)
        {
            m_RoleStateAttack.AnimatorConditionName = ToAnimatorCondition.ToPhyAttack;
        }
        else
        {
            m_RoleStateAttack.AnimatorConditionName = ToAnimatorCondition.ToSkill;
        }
        m_RoleStateAttack.AnimatorConditionValue = info.AnimatorConditionValue;
        m_RoleStateAttack.AnimatorState = ConvertUtil.ConvertToEnum<RoleAnimatorState>(info.AnimatorState);
        m_CurrRoleFSMMgr.ChangeState(RoleState.Attack);

        return true;
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
