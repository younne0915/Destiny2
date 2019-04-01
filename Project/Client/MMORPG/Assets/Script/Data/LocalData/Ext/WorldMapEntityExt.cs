using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class WorldMapEntity
{
    private bool m_InitRoleBirthPosition = false;
    private Vector3 m_RoleBirthPosition = Vector3.zero;
    public Vector3 RoleBirthPosition
    {
        get
        {
            if (!m_InitRoleBirthPosition)
            {
                m_InitRoleBirthPosition = true;
                string[] arr = RoleBirthPos.Split('_');
                if (arr == null || arr.Length < 3) return Vector3.zero;
                float x = 0, y = 0, z = 0;
                float.TryParse(arr[0], out x);
                float.TryParse(arr[1], out y);
                float.TryParse(arr[2], out z);
                m_RoleBirthPosition = new Vector3(x, y, z);
            }
            return m_RoleBirthPosition;
        }
    }

    private bool m_InitRoleBirthYRotation = false;
    private float m_RoleBirthYRotation;
    public float RoleBirthEulerAnglesY
    {
        get
        {
            if (!m_InitRoleBirthYRotation)
            {
                m_InitRoleBirthYRotation = true;
                string[] arr = RoleBirthPos.Split('_');
                if (arr == null || arr.Length < 4) return 0;
                float.TryParse(arr[3], out m_RoleBirthYRotation);
            }
            return m_RoleBirthYRotation;
        }
    }

    private List<NPCWorldMapData> m_NPCWorldMapDataList;
    public List<NPCWorldMapData> NPCWorldMapDataList
    {
        get
        {
            if(m_NPCWorldMapDataList == null)
            {
                string[] arr1 = NPCList.Split('|');
                if(arr1 != null)
                {
                    m_NPCWorldMapDataList = new List<NPCWorldMapData>();
                    NPCWorldMapData nPCWorldMapData;
                    for (int i = 0; i < arr1.Length; i++)
                    {
                        string[] arr2 = arr1[i].Split('_');
                        if (arr2 == null || arr2.Length < 6) continue;
                        int npcId = 0;
                        int.TryParse(arr2[0], out npcId);
                        float x = 0, y = 0, z = 0, angleY = 0;
                        float.TryParse(arr2[1], out x);
                        float.TryParse(arr2[2], out y);
                        float.TryParse(arr2[3], out z);
                        float.TryParse(arr2[4], out angleY);
                        string prologue = arr2[5];
                        nPCWorldMapData = new NPCWorldMapData(npcId, new Vector3(x, y, z), angleY, prologue);
                        m_NPCWorldMapDataList.Add(nPCWorldMapData);
                    }
                }
            }
            return m_NPCWorldMapDataList;
        }
    }
}
