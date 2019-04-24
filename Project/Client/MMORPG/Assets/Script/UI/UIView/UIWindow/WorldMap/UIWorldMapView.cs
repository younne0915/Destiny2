using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class UIWorldMapView : UIWindowViewBase
{
    [SerializeField]
    private Transform Container;

    private List<Transform> m_TransformList = new List<Transform>();

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        Container = null;
        for (int i = 0; i < m_TransformList.Count; i++)
        {
            RecyclePoolMgr.Instance.Despawn(PoolType.UI, m_TransformList[i]);
        }
        m_TransformList.Clear();
    }

    private List<TransferData> m_lst;

    public void SetUI(TransferData data, Action<int> onWorldMapItemClick)
    {
        if (Container == null) return;

        m_lst = data.GetValue<List<TransferData>>(ConstDefine.WorldMapList);

        //LoadWorldMapItem(0, onWorldMapItemClick);

        LoaderMgr.Instance.LoadOrDownload("Download/Prefab/UIPrefab/UIWindowsChild/WorldMap/WorldMapItem", "WorldMapItem", (GameObject obj) =>
        {
            //克隆了一个预设 只需要克隆一次
            StartCoroutine(LoadWorldMapItem(obj, onWorldMapItemClick));
        });
        //m_TransformList.Clear();
        //for (int i = 0; i < m_lst.Count; i++)
        //{
        //    //GameObject obj = Instantiate(ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindowChild, "WorldMap/WorldMapItem"));
        //    Transform obj = RecyclePoolMgr.Instance.Spawn(PoolType.UI, ResourLoadType.Resources, "UIPrefab/UIWindowsChild/WorldMap/WorldMapItem");
        //    obj.gameObject.SetParent(Container);
        //    Vector2 pos = m_lst[i].GetValue<Vector2>(ConstDefine.WorldMapPostion);
        //    obj.localPosition = new Vector3(pos.x, pos.y, 0);

        //    UIWorldMapItemView view = obj.GetComponent<UIWorldMapItemView>();
        //    if(view != null)
        //    {
        //        view.SetUI(m_lst[i], onWorldMapItemClick);
        //    }
        //    m_TransformList.Add(obj);
        //}
    }

    private IEnumerator LoadWorldMapItem(GameObject obj, Action<int> onWorldMapItemClick)
    {
        m_TransformList.Clear();
        for (int i = 0; i < m_lst.Count; i++)
        {
            GameObject worldMapItemObj = RecyclePoolMgr.Instance.Spawn(PoolType.UI, obj.transform).gameObject;
            worldMapItemObj.SetParent(Container);
            Vector2 pos = m_lst[i].GetValue<Vector2>(ConstDefine.WorldMapPostion);
            worldMapItemObj.transform.localPosition = new Vector3(pos.x, pos.y, 0);
            m_TransformList.Add(worldMapItemObj.transform);

            UIWorldMapItemView view = worldMapItemObj.GetComponent<UIWorldMapItemView>();
            view.SetUI(m_lst[i], onWorldMapItemClick);

            yield return null;
        }
    }
}
