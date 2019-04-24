//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:57:48
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 主角主城AI
/// </summary>
public class RoleMainPlayerCityAI : IRoleAI
{
    public RoleCtrl CurrRole
    {
        get;
        set;
    }

    private int m_PhyIndex = 0;

    public RoleMainPlayerCityAI(RoleCtrl roleCtrl)
    {
        CurrRole = roleCtrl;
    }

    public void DoAI()
    {
        if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die) return;

        //if(CurrRole.CurrRoleType == RoleType.MainPlayer)
        //{
        //    AppDebug.LogError(CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum);
        //}

        if (CurrRole.IsAutoFight)
        {
            AutoFightState();
        }
        else
        {
            NormalFightState();
        }
    }

    private void AutoFightState()
    {
        if(SceneMgr.Instance.CurrentSceneType == SceneType.GameLevel)
        {
            if (GameLevelSceneCtrl.Instance == null) return;

            if (!GameLevelSceneCtrl.Instance.CurrRegionHasMonster)
            {
                if (GameLevelSceneCtrl.Instance.IsLastRegion) return;
                CurrRole.MoveTo(GameLevelSceneCtrl.Instance.PlayerBornPos);
            }
            else
            {
                if (GameLevelSceneCtrl.Instance.CurrRegionLiveMonsterList.Count > 0)
                {
                    if (CurrRole.LockEnemy == null)
                    {
                        GameLevelSceneCtrl.Instance.CurrRegionLiveMonsterList.Sort((RoleCtrl r1, RoleCtrl r2) =>
                        {
                            int ret = 0;
                            float dis1 = Vector3.Distance(r1.transform.position, CurrRole.transform.position);
                            float dis2 = Vector3.Distance(r2.transform.position, CurrRole.transform.position);

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
                        CurrRole.LockEnemy = GameLevelSceneCtrl.Instance.CurrRegionLiveMonsterList[0];
                    }
                    else
                    {
                        if (CurrRole.LockEnemy.CurrRoleInfo.CurrHP <= 0)
                        {
                            CurrRole.LockEnemy = null;
                            return;
                        }


                        if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Idle)
                        {
                            int skillId = 0;
                            RoleAttackType type = RoleAttackType.SkillAttack;

                            skillId = GlobalInit.Instance.MainPlayerInfo.GetCanUseSkillId();
                            if(skillId > 0)
                            {
                                type = RoleAttackType.SkillAttack;
                            }
                            else
                            {
                                if (GlobalInit.Instance.MainPlayerInfo.PhysicList != null && GlobalInit.Instance.MainPlayerInfo.PhysicList.Count > 0)
                                {
                                    type = RoleAttackType.PhyAttack;
                                    skillId = GlobalInit.Instance.MainPlayerInfo.PhysicList[m_PhyIndex];
                                    m_PhyIndex++;
                                    if (m_PhyIndex >= GlobalInit.Instance.MainPlayerInfo.PhysicList.Count)
                                    {
                                        m_PhyIndex = 0;
                                    }
                                }
                            }

                            SkillEntity skillEntity = SkillDBModel.Instance.Get(skillId);
                            if (skillEntity == null) return;

                            //如果在技能攻击范围内 直接攻击
                            if (Vector3.Distance(CurrRole.transform.position, CurrRole.LockEnemy.transform.position) <= skillEntity.AttackRange)
                            {
                                if (type == RoleAttackType.SkillAttack)
                                {
                                    CurrRole.ToSkillAttack(skillId);
                                }
                                else
                                {
                                    CurrRole.ToAttack(type, skillId);
                                }

                            }
                            else
                            {
                                RaycastHit hitInfo;
                                //如果在我的视野范围之内 在技能的攻击范围外 进行追击
                                Vector3 targetPos = GameUtil.GetRandomPos(CurrRole.transform.position, CurrRole.LockEnemy.transform.position, skillEntity.AttackRange);
                                Vector3 orgionPos = new Vector3(targetPos.x, targetPos.y + 100, targetPos.z);
                                if (Physics.Raycast(orgionPos, new Vector3(0, -100, 0), out hitInfo, 1000, 1 << LayerMask.NameToLayer("RegionMask")))
                                {
                                    return;
                                }
                                Debug.DrawLine(CurrRole.transform.position, targetPos, Color.red);
                                CurrRole.MoveTo(targetPos);
                            }
                        }
                    }
                }
            }
        }
    }

    private void NormalFightState()
    {
        //执行AI
        //1.如果我有锁定敌人 就行攻击
        if (CurrRole.LockEnemy != null)
        {
            if (CurrRole.LockEnemy.CurrRoleInfo.CurrHP <= 0)
            {
                CurrRole.LockEnemy = null;
                return;
            }

            //if (CurrRole.LockEnemy.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die)
            //{
            //    CurrRole.LockEnemy = null;
            //    return;
            //}

            if (CurrRole.CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Idle)
            {
                if (CurrRole.m_Attack.FollowSkillId > 0)
                {
                    CurrRole.ToSkillAttack(CurrRole.m_Attack.FollowSkillId);
                    CurrRole.m_Attack.FollowSkillId = -1;
                }
                else
                {
                    if (GlobalInit.Instance.MainPlayerInfo.PhysicList != null && GlobalInit.Instance.MainPlayerInfo.PhysicList.Count > 0)
                    {
                        int physicId = GlobalInit.Instance.MainPlayerInfo.PhysicList[m_PhyIndex];
                        CurrRole.ToAttack(RoleAttackType.PhyAttack, physicId);
                        m_PhyIndex++;
                        if (m_PhyIndex >= GlobalInit.Instance.MainPlayerInfo.PhysicList.Count)
                        {
                            m_PhyIndex = 0;
                        }
                    }
                }
            }
        }
        else
        {
            if (CurrRole.PreIdleFightTime > 0.01f)
            {
                if (Time.time > CurrRole.PreIdleFightTime + 10)
                {
                    CurrRole.ToIdle();
                    CurrRole.PreIdleFightTime = 0;
                }
            }
        }
    }
}