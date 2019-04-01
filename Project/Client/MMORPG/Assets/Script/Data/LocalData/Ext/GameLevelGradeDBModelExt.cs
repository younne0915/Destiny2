using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLevelGradeDBModel
{
    public GameLevelGradeEntity GetEntityByGameLevelIdAndGrade(int gameLevelId, GameLevelGrade grade)
    {
        if(m_List != null)
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                if(m_List[i].GameLevelId == gameLevelId && m_List[i].CurrGrade == grade)
                {
                    return m_List[i];
                }
            }
        }
        return null;
    }
}
