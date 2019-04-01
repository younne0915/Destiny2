using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLevelDBModel
{
    private List<GameLevelEntity> m_RetList = new List<GameLevelEntity>();
	public List<GameLevelEntity> GetListByChapterId(int chapterId)
    {
        m_RetList.Clear();

        if (m_List != null)
        {
            GameLevelEntity entity;
            for (int i = 0; i < m_List.Count; i++)
            {
                entity = m_List[i];
                if (entity.ChapterID == chapterId)
                {
                    m_RetList.Add(entity);
                }
            }
        }

        return m_RetList;
    }
}
