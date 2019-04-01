using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillLevelDBModel
{
    public SkillLevelEntity GetSkillLevelEntityBySkillIdAndLevel(int skillId, int level)
    {
        if (m_List != null)
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                if(m_List[i].SkillId == skillId && m_List[i].Level == level)
                {
                    return m_List[i];
                }
            }
        }
        return null;
    }
}
