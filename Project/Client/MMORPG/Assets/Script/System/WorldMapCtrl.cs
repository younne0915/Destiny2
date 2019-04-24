using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCtrl : SystemCtrlBase<WorldMapCtrl>, ISystemCtrl
{
    private UIWorldMapView m_UIWorldMapView;
    private UIWorldMapFailView m_UIWorldMapFailView;

    private Dictionary<int, WorldMapSceneNode> m_WorldMapDic;
    private List<WorldMapSceneNode> m_PathList;
    private Queue<int> m_PathQueue;
    private int m_EndSceneId = 0;
    private WorldMapSceneNode m_EndSceneEntity;

    public bool AutoMove = false;
    public int CurrMoveSceneId = 0;
    public int ToMoveSceneId = 0;
    public Vector3 TargetPos = new Vector3(63.85915f, 14.64824f, 111.9766f);
    public string AttackNickName;

    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.WorldMap:
                OpenWorldMapView();
                break;
            case WindowUIType.WorldMapFail:
                OpenWorldMapFailView();
                break;
        }
    }

    private void OpenWorldMapFailView()
    {
        AppDebug.LogError("OpenWorldMapFailView : " + AttackNickName);
        UIViewUtil.Instance.LoadWindow(WindowUIType.WorldMapFail,(GameObject obj)=> 
        {
            if(obj != null)
            {
                m_UIWorldMapFailView = obj.GetComponent<UIWorldMapFailView>();
                TransferData transferData = new TransferData();
                transferData.SetValue(ConstDefine.WorldMapName, AttackNickName);
                m_UIWorldMapFailView.SetUI(transferData);
            }
        });
    }

    private void OpenWorldMapView()
    {
        List<WorldMapEntity> listWorldMap = WorldMapDBModel.Instance.GetList();
        if (listWorldMap == null || listWorldMap.Count == 0) return;
        UIViewUtil.Instance.LoadWindow(WindowUIType.WorldMap,(GameObject obj)=> 
        {
            if(obj != null)
            {
                m_UIWorldMapView = obj.GetComponent<UIWorldMapView>();
                TransferData transferData = new TransferData();
                List<TransferData> list = new List<TransferData>();

                for (int i = 0; i < listWorldMap.Count; i++)
                {
                    WorldMapEntity entity = listWorldMap[i];
                    if (entity.IsShowInMap == 0) continue;

                    TransferData childData = new TransferData();
                    childData.SetValue(ConstDefine.WorldMapId, entity.Id);
                    childData.SetValue(ConstDefine.WorldMapName, entity.Name);
                    childData.SetValue(ConstDefine.WorldMapIco, entity.IcoInMap);

                    string[] arr = entity.PosInMap.Split('_');
                    Vector2 pos = new Vector2();
                    if (arr.Length == 2)
                    {
                        pos.x = arr[0].ToFloat();
                        pos.y = arr[1].ToFloat();
                    }
                    childData.SetValue(ConstDefine.WorldMapPostion, pos);
                    list.Add(childData);
                }
                transferData.SetValue(ConstDefine.WorldMapList, list);
                m_UIWorldMapView.SetUI(transferData, OnWorldMapItemClick);
            }
        });
    }

    private void OnWorldMapItemClick(int obj)
    {
        //SceneMgr.Instance.LoadToWorldMap(obj);
        CalculateWorldMapScenePath(SceneMgr.Instance.CurrWorldMapId, obj);
    }

    private void Calculate(int currSceneId)
    {
        WorldMapEntity entity = WorldMapDBModel.Instance.Get(currSceneId);
        WorldMapSceneNode parent = m_WorldMapDic[currSceneId];
        parent.IsVisit = true;
        if (currSceneId == m_EndSceneId)
        {
            m_EndSceneEntity = parent;
            return;
        }
        string[] nearSceneArr = entity.NearScene.Split('_');
        for (int i = 0; i < nearSceneArr.Length; i++)
        {
            int sceneId = nearSceneArr[i].ToInt();
            WorldMapSceneNode worldMapSceneEntity = m_WorldMapDic[sceneId];
            if (worldMapSceneEntity.IsVisit) continue;
            worldMapSceneEntity.IsVisit = true;
            worldMapSceneEntity.parent = parent;
            if(worldMapSceneEntity.SceneId == m_EndSceneId)
            {
                m_EndSceneEntity = worldMapSceneEntity;
                break;
            }
            else
            {
                Calculate(sceneId);
            }
        }
    }

    public void CalculateWorldMapScenePath(int beganSceneId, int endSceneId)
    {
        m_EndSceneId = endSceneId;

        if (m_WorldMapDic == null)
        {
            m_WorldMapDic = new Dictionary<int, WorldMapSceneNode>();
            m_PathList = new List<WorldMapSceneNode>();
            m_PathQueue = new Queue<int>();

            List<WorldMapEntity> listWorldMap = WorldMapDBModel.Instance.GetList();
            if (listWorldMap == null || listWorldMap.Count == 0) return;
            for (int i = 0; i < listWorldMap.Count; i++)
            {
                WorldMapEntity entity = listWorldMap[i];
                m_WorldMapDic.Add(entity.Id, new WorldMapSceneNode(entity.Id));
            }
        }
        else
        {
            foreach (var item in m_WorldMapDic.Values)
            {
                item.IsVisit = false;
                item.parent = null;
            }
        }

        m_EndSceneEntity = null;
        m_PathList.Clear();
        Calculate(beganSceneId);
        if(m_EndSceneEntity != null)
        {
            while (m_EndSceneEntity != null)
            {
                m_PathList.Add(m_EndSceneEntity);
                m_EndSceneEntity = m_EndSceneEntity.parent;
            }

            for (int i = m_PathList.Count - 1; i >= 0; i--)
            {
                AppDebug.LogError(m_PathList[i].SceneId);
                m_PathQueue.Enqueue(m_PathList[i].SceneId);
            }
        }

        m_UIWorldMapView.Close();
        if(m_PathQueue.Count >= 2)
        {
            CurrMoveSceneId = m_PathQueue.Dequeue();
            ToMoveSceneId = m_PathQueue.Dequeue();
            AutoMove = true;

            if (WorldMapSceneCtrl.Instance != null)
            {
                WorldMapSceneCtrl.Instance.AutoMove();
            }
        }
    }

    public void NextScene()
    {
        CurrMoveSceneId = ToMoveSceneId;
        if(m_PathQueue.Count > 0)
        {
            ToMoveSceneId = m_PathQueue.Dequeue();
        }
    }

    

    public class WorldMapSceneNode
    {
        public int SceneId;
        public bool IsVisit = false;
        public WorldMapSceneNode parent = null;

        public WorldMapSceneNode(int sceneId)
        {
            SceneId = sceneId;
        }
    }
}
