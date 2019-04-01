using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameLevelEntity
{
    private Vector2 m_Position = Vector2.zero;

    public Vector2 Position
    {
        get
        {
            if(m_Position == Vector2.zero)
            {
                string[] arr = PosInMap.Split('_');
                if(arr != null && arr.Length >= 2)
                {
                    float x = 0, y = 0;
                    float.TryParse(arr[0], out x);
                    float.TryParse(arr[1], out y);
                    m_Position = new Vector2(x, y);
                }
            }
            return m_Position;
        }
    }

    private List<int> m_RegionIdList = null;
    public List<int> RegionIdList
    {
        get
        {
            if(m_RegionIdList == null)
            {
                m_RegionIdList = new List<int>();
                string[] arr = RegionId.Split('|');
                if(arr != null)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        int tempId = 0;
                        int.TryParse(arr[i], out tempId);
                        m_RegionIdList.Add(tempId);
                    }
                }
            }
            return m_RegionIdList;
        }
    }

    public int GetRegionIdByIndex(int index)
    {
        if(RegionIdList != null && RegionIdList.Count > index)
        {
            return RegionIdList[index];
        }

        return -1;
    }
}
