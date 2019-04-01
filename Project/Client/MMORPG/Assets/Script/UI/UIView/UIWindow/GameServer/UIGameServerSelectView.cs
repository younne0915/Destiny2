using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIGameServerSelectView : UIWindowViewBase
{
    [SerializeField]
    private GameObject GameServerPageItemPrefab;

    [SerializeField]
    private GameObject GameServerPageGrid;

    [SerializeField]
    private GameObject GamseServerItemPrefab;

    [SerializeField]
    private GameObject GameServerGrid;

    private List<GameObject> m_GameServerObjList = new List<GameObject>();

    public Action<int> OnGameServerPageClick;

    public Action<RetGameServerEntity> OnGameServerClick;

    [SerializeField]
    private UIGameServerItemView m_GameServerSelectItemView;

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        GameServerPageItemPrefab = null;
        GameServerPageGrid = null;
        GamseServerItemPrefab = null;
        GameServerGrid = null;
        m_GameServerObjList.ToArray().SetNull();
        OnGameServerPageClick = null;
        OnGameServerClick = null;
        m_GameServerSelectItemView = null;
    }

    protected override void OnStart()
    {
        base.OnStart();
        for (int i = 0; i <10; i++)
        {
            GameObject obj = Instantiate(GamseServerItemPrefab) as GameObject;
            obj.transform.parent = GameServerGrid.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            m_GameServerObjList.Add(obj);
        }
    }

    public void SetGameServerPageUI(List<RetGameServerPageEntity> list)
    {
        if (list == null || GameServerPageItemPrefab == null) return;

        for (int i = 0; i < list.Count; i++)
        {
            GameObject obj = Instantiate(GameServerPageItemPrefab) as GameObject;
            obj.transform.parent = GameServerPageGrid.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;

            UIGameServerPageItemView view = obj.GetComponent<UIGameServerPageItemView>();
            if(view != null)
            {
                view.SetUI(list[i]);
                view.OnGameServerPageClick = OnGameServerPageClickCallback;
            }
        }
    }

    private void OnGameServerPageClickCallback(int obj)
    {
        if (OnGameServerPageClick != null) OnGameServerPageClick(obj);
    }

    public void SetGameServerUI(List<RetGameServerEntity> list)
    {
        if (list == null || GamseServerItemPrefab == null) return;

        for (int i = 0; i < m_GameServerObjList.Count; i++)
        {
            GameObject obj = m_GameServerObjList[i];
            if (i > list.Count - 1)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
                UIGameServerItemView view = obj.GetComponent<UIGameServerItemView>();
                if (view != null)
                {
                    view.SetUI(list[i]);
                    view.OnGameServerItemClick = OnGameServerClickCallback;
                }
            }
        }
    }

    private void OnGameServerClickCallback(RetGameServerEntity entity)
    {
        if (OnGameServerClick != null) OnGameServerClick(entity);
    }

    public void SetGameServerSelectItemView(RetGameServerEntity entity)
    {
        m_GameServerSelectItemView.SetUI(entity);
    }
}
