using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoleAttackInfo
{
    public int Index;
    public int SkillId;
    public string EffectName;
    public float AttackRange = 0;
    /// <summary>
    /// �öԷ������ӳ�
    /// </summary>
    public float HurtDelayTime = 0;
    /// <summary>
    /// ��Ч�Ĵ��ʱ��
    /// </summary>
    public float EffectLifeTime = 0;

    public bool IsDoCameraShake = true;

    public float CameraShakeDelay = 0.2f;

#if DEBUG_ROLESTATE
    public GameObject EffectObj;
#endif

}
