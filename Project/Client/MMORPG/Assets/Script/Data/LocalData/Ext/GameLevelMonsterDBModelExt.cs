using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GameLevelMonsterDBModel
{
    #region GetGameLevelMonsterTotalCount ������Ϸ�ؿ���ź��Ѷȵȼ���ȡ�ֵ�������
    /// <summary>
    /// ������Ϸ�ؿ���ź��Ѷȵȼ���ȡ�ֵ�������
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="grade">�Ѷȵȼ�</param>
    /// <returns></returns>
    public int GetGameLevelMonsterTotalCount(int gameLevelId, GameLevelGrade grade)
    {
        int count = 0;
        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].GameLevelId == gameLevelId && m_List[i].Grade == (int)grade)
            {
                count += m_List[i].SpriteCount;
            }
        }
        return count;
    }
    #endregion

    #region GetGameLevelMonsterId ������Ϸ�ؿ���ź��Ѷȵȼ���ȡ�ֵ�����
    /// <summary>
    /// ������Ϸ�ؿ���ź��Ѷȵȼ���ȡ�ֵ�����
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="grade"></param>
    /// <returns></returns>
    public List<int> GetGameLevelMonsterIdList(int gameLevelId, GameLevelGrade grade)
    {
        List<int> lst = new List<int>();

        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].GameLevelId == gameLevelId && m_List[i].Grade == (int)grade)
            {
                if (!lst.Contains(m_List[i].SpriteId))
                {
                    lst.Add(m_List[i].SpriteId);
                }
            }
        }
        return lst;
    }
    #endregion

    /// <summary>
    /// ������Ϸ�ؿ���ź��Ѷȵȼ��������Ż�ȡ�ֵ�������
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="grade"></param>
    /// <param name="regionId"></param>
    /// <returns></returns>
    public int GetRegionMonsterTotalCount(int gameLevelId, GameLevelGrade grade, int regionId)
    {
        int count = 0;
        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].GameLevelId == gameLevelId && m_List[i].Grade == (int)grade && m_List[i].RegionId == regionId)
            {
                count += m_List[i].SpriteCount;
            }
        }
        return count;
    }

    /// <summary>
    /// ������Ϸ�ؿ���ź��Ѷȵȼ��������Ż�ȡ���й�
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="grade"></param>
    /// <param name="regionId"></param>
    /// <returns></returns>
    public void GetRegionGameLevelMonsterEntityList(int gameLevelId, GameLevelGrade grade, int regionId, List<GameLevelMonsterEntity> retLst)
    {
        if(retLst != null)
        {
            retLst.Clear();
            for (int i = 0; i < m_List.Count; i++)
            {
                if (m_List[i].GameLevelId == gameLevelId && m_List[i].Grade == (int)grade && m_List[i].RegionId == regionId)
                {
                    retLst.Add(m_List[i]);
                }
            }
        }
    }

    /// <summary>
    /// ��ȡ��Ϸ�ؿ������ʵ��
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="grade"></param>
    /// <param name="regionId"></param>
    /// <param name="monsterId"></param>
    /// <returns></returns>
    public GameLevelMonsterEntity GetGameLevelMonsterEntity(int gameLevelId, GameLevelGrade grade, int regionId, int monsterId)
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].GameLevelId == gameLevelId && m_List[i].Grade == (int)grade && m_List[i].RegionId == regionId && m_List[i].SpriteId == monsterId)
            {
                return m_List[i];
            }
        }
        return null;
    }


    public void GetGameLevelMonsterEntityList(int gameLevelId, GameLevelGrade grade, int regionId, List<GameLevelMonsterEntity> regionMonsterEntityList)
    {
        if(regionMonsterEntityList != null)
        {
            regionMonsterEntityList.Clear();
            for (int i = 0; i < m_List.Count; i++)
            {
                if (m_List[i].GameLevelId == gameLevelId && m_List[i].Grade == (int)grade && m_List[i].RegionId == regionId && m_List[i].SpriteCount > 0)
                {
                    regionMonsterEntityList.Add(m_List[i]);
                }
            }
        }
    }
}