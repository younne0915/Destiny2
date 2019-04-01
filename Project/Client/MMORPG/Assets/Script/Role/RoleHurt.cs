using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoleHurt
{
    private RoleFSMMgr m_CurrRoleFSMMgr = null;

    public Action OnRoleHurt;

    public RoleHurt(RoleFSMMgr fsm)
    {
        m_CurrRoleFSMMgr = fsm;
    }

    public IEnumerator ToHurt(RoleTransferAttackInfo roleTransferAttackInfo)
    {
        if (roleTransferAttackInfo == null || m_CurrRoleFSMMgr == null) yield break;

        if (m_CurrRoleFSMMgr.CurrRoleStateEnum == RoleState.Die) yield break;

        SkillEntity skillEntity = SkillDBModel.Instance.Get(roleTransferAttackInfo.SkillId);
        SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetSkillLevelEntityBySkillIdAndLevel(roleTransferAttackInfo.SkillId, roleTransferAttackInfo.SkillLevel);
        if (skillEntity == null || skillLevelEntity == null) yield break;

        yield return new WaitForSeconds(skillEntity.ShowHurtEffectDelaySecond);

        m_CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleInfo.CurrHP -= roleTransferAttackInfo.HurtValue;
        if (OnRoleHurt != null)
        {
            OnRoleHurt();
        }
        if (m_CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleInfo.CurrHP <= 0)
        {
            m_CurrRoleFSMMgr.CurrRoleCtrl.CurrRoleInfo.CurrHP = 0;
            m_CurrRoleFSMMgr.CurrRoleCtrl.ToDie();
            yield break;
        }

        Color color = Color.red;
        int frontSize = 8;
        int speed = 20;
        float y1 = -1f;
        float y2 = 2.2f;
        if (roleTransferAttackInfo.IsCri)
        {
            color = Color.yellow;
            frontSize = 16;
            speed = 10;
            y1 = 1f;
            y2 = 4.4f;
        }

        UISceneCtrl.Instance.CurrentUIScene.HUDText.NewText("- " + roleTransferAttackInfo.HurtValue, m_CurrRoleFSMMgr.CurrRoleCtrl.transform, color, frontSize, speed, y1, y2, UnityEngine.Random.Range(0, 2) == 1? bl_Guidance.RightDown: bl_Guidance.LeftDown);

        Transform effectTransform = RecyclePoolMgr.Instance.Spawn(PoolType.Effect, ResourLoadType.AssetBundle, "Effect/Effect_Hurt");
        effectTransform.position = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.position;
        effectTransform.rotation = m_CurrRoleFSMMgr.CurrRoleCtrl.transform.rotation;
        RecyclePoolMgr.Instance.Despawn(PoolType.Effect, effectTransform, 3);

        if (!m_CurrRoleFSMMgr.IsRigidty)
        {
            m_CurrRoleFSMMgr.ChangeState(RoleState.Hurt);
        }

    }
}
