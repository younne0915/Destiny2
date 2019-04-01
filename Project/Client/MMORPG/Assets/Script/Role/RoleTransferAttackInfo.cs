using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleTransferAttackInfo
{
    public int AttackRoleId;
    public int BeAttackRoleId;
    /// <summary>
    /// 伤害数值
    /// </summary>
    public int HurtValue;
    public int SkillId;
    public int SkillLevel;
    /// <summary>
    /// 是否附加异常状态
    /// </summary>
    public bool IsAbnormal;
    /// <summary>
    /// 是否暴击
    /// </summary>
    public bool IsCri;
}
