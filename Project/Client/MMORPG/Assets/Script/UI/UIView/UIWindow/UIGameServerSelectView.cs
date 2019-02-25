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
        AppDebug.Log(string.Format("µã»÷ÁËµÚ{0}Ò³", obj));
    }
}
