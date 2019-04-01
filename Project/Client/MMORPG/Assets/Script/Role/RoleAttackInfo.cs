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
    /// 让对方受伤延迟
    /// </summary>
    public float HurtDelayTime = 0;
    /// <summary>
    /// 特效的存活时间
    /// </summary>
    public float EffectLifeTime = 0;

    public bool IsDoCameraShake = true;

    public float CameraShakeDelay = 0.2f;

#if DEBUG_ROLESTATE
    public GameObject EffectObj;
#endif

}
