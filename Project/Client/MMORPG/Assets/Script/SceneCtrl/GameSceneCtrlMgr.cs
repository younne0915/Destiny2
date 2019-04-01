using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneCtrlMgr : MonoBehaviour
{
    public static GameSceneCtrlMgr Instance;

    [SerializeField]
    private GameLevelSceneCtrl m_GameLevelSceneCtrl;

    [SerializeField]
    private WorldMapSceneCtrl m_WorldMapSceneCtrl;

    private Dictionary<SceneType, GameSceneCtrlBase> m_Dic = new Dictionary<SceneType, GameSceneCtrlBase>();


    [SerializeField]
    private Transform m_Ground;

    private void Awake()
    {
        if(m_GameLevelSceneCtrl != null)
        {
            m_Dic[SceneType.GameLevel] = m_GameLevelSceneCtrl;
        }

        if(m_WorldMapSceneCtrl != null)
        {
            m_Dic[SceneType.WorldMap] = m_WorldMapSceneCtrl;
        }
        Instance = this;
    }

    void Start ()
    {
        SceneType type = SceneMgr.Instance.CurrentSceneType;

        //while (m_Dic.GetEnumerator().MoveNext())
        //{
        //    m_Dic.GetEnumerator().Current.Value.gameObject.SetActive(m_Dic.GetEnumerator().Current.Key == type);
        //    m_Dic.GetEnumerator().MoveNext();
        //}
        foreach (var item in m_Dic)
        {
            item.Value.gameObject.SetActive(item.Key == type);
        }

        Renderer[] arr = m_Ground.GetComponentsInChildren<Renderer>();
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].enabled = false;
            }
        }
    }
    
}
