//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-07-25 21:32:19
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class UIGameLevelMapView : UIWindowViewBase
{
    /// <summary>
    /// 章名称
    /// </summary>
    [SerializeField]
    private Text txtChapterName;

    /// <summary>
    /// 背景图
    /// </summary>
    [SerializeField]
    private RawImage imgMap;

    /// <summary>
    /// 章编号
    /// </summary>
    [HideInInspector]
    private int m_ChapterId;

    [SerializeField]
    private Transform pointContainer;


    [SerializeField]
    private Transform gameLevelContainer;

    private List<Transform> m_GameLevelList = new List<Transform>();

    private List<TransferData> m_GameLevelDataList;

    private Action<int> m_OnGameLevelItemClick;

    protected override void OnStart()
    {
        base.OnStart();
    }
    
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        txtChapterName = null;
        imgMap = null;
    }

    public void SetUI(TransferData data, Action<int> onGameLevelItemClick)
    {
        m_OnGameLevelItemClick = onGameLevelItemClick;
        m_ChapterId = data.GetValue<int>(ConstDefine.ChapterId);
        txtChapterName.SetText(data.GetValue<string>(ConstDefine.ChapterName));
        //imgMap.texture = GameUtil.LoadGameLevelMapPic(data.GetValue<string>(ConstDefine.ChapterBG));

        LoaderMgr.Instance.LoadOrDownload<Texture>(string.Format("Download/Source/UISource/GameLevel/GameLevelMap/{0}", data.GetValue<string>(ConstDefine.ChapterBG)), data.GetValue<string>(ConstDefine.ChapterBG), (Texture obj) =>
        {
            imgMap.texture = obj;
        }, type: 1);

        m_GameLevelDataList = data.GetValue<List<TransferData>>(ConstDefine.GameLevelList);


        RecyclePoolMgr.Instance.SpawnOrLoadByAssetBundle(PoolType.UI, "Download/Prefab/UIPrefab/UIWindowsChild/GameLevel/GameLevelMapItem", (Transform gameLevelMapTransform) =>
        {
            StartCoroutine(CreateGameLevelMapItemCorotine(gameLevelMapTransform));
        });
    }

    private IEnumerator CreatePointCoroutine(Transform gameLevelMapPointTransform)
    {
        for (int i = 0; i < m_GameLevelList.Count; i++)
        {
            if (i == m_GameLevelList.Count - 1) break;

            Transform beganPoint = m_GameLevelList[i];
            Transform endPoint = m_GameLevelList[i + 1];

            float distance = Vector3.Distance(beganPoint.localPosition, endPoint.localPosition);
            int createCount = Mathf.FloorToInt(distance / 20);

            float deltX = endPoint.localPosition.x - beganPoint.localPosition.x;
            float deltY = endPoint.localPosition.y - beganPoint.localPosition.y;

            float stepX = deltX / createCount;
            float stepY = deltY / createCount;

            for (int j = 0; j < createCount; j++)
            {
                GameObject obj = RecyclePoolMgr.Instance.Spawn(PoolType.UI, gameLevelMapPointTransform).gameObject;
                obj.SetParent(pointContainer);
                obj.transform.localPosition = new Vector3(beganPoint.localPosition.x + j * stepX, beganPoint.localPosition.y + j * stepY, 0);
                UIGameLevelMapPointView view = obj.GetComponent<UIGameLevelMapPointView>();
                view.SetUI(true);
                yield return null;
            }
            yield return null;
        }
    }

    private IEnumerator CreateGameLevelMapItemCorotine(Transform gameLevelMapTransform)
    {
        if (m_GameLevelDataList == null) yield break;
        if (m_GameLevelDataList != null)
        {
            m_GameLevelList.Clear();
            TransferData gameLevelData;

            for (int i = 0; i < m_GameLevelDataList.Count; i++)
            {
                gameLevelData = m_GameLevelDataList[i];
                GameObject obj = RecyclePoolMgr.Instance.Spawn(PoolType.UI, gameLevelMapTransform).gameObject;
                obj.SetParent(gameLevelContainer.transform);
                obj.transform.localPosition = gameLevelData.GetValue<Vector2>(ConstDefine.GameLevelPostion);
                UIGameLevelMapItemView itemView = obj.GetComponent<UIGameLevelMapItemView>();
                itemView.SetUI(gameLevelData, m_OnGameLevelItemClick);
                m_GameLevelList.Add(obj.transform);

                yield return null;
            }

            RecyclePoolMgr.Instance.SpawnOrLoadByAssetBundle(PoolType.UI, "Download/Prefab/UIPrefab/UIWindowsChild/GameLevel/GameLevelMapPoint", (Transform gameLevelMapPointTransform) =>
            {
                StartCoroutine(CreatePointCoroutine(gameLevelMapPointTransform));
            });
        }
    }

    private void OnGameLevelItemClick(int obj)
    {
        AppDebug.Log("点击 GameLevelId = " + obj);
    }
}