using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleInfoSkill
{
    public int SkillId; //技能编号
    public int SkillLevel; //技能等级
    public byte SlotsNo; //技能槽编号
    public int SpendMP;
    public float SkillCDEndTime;
    public float SkillCDTime
    {
        get;
        private set;
    }

    public RoleInfoSkill(int skillId, int skillLevel, byte slotsNo)
    {
        SkillId = skillId;
        SkillLevel = skillLevel;
        SlotsNo = slotsNo;

        SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetSkillLevelEntityBySkillIdAndLevel(SkillId, SkillLevel);
        if (skillLevelEntity != null)
        {
            SkillCDTime = skillLevelEntity.SkillCDTime;
            SpendMP = skillLevelEntity.SpendMP;
        }
    }

    public void SetSkillEndCDTime()
    {
        SkillCDEndTime = Time.time + SkillCDTime;
    }
}
