using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameServerCtrl : SystemCtrl<GameServerCtrl>, ISystemCtrl
{
    private UIGameServerEnterView uiGameServerEnterView;
    private UIGameServerSelectView uiGameServerSelectView;

    public GameServerCtrl()
    {
        AddEventHandler(ConstDefine.UIGameServerEnterView_btnSelectGameServer, UIGameServerEnterView_btnSelectGameServerClick);
    }

    private void UIGameServerEnterView_btnSelectGameServerClick(object[] p)
    {
        //获取区服页签列表
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 0;
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", OnGetGameServerPageListCallBack, isPost: true, dic: dic);

        //获取区服列表
        //Dictionary<string, object> dic = new Dictionary<string, object>();
        //dic["Type"] = 1;
        //dic["PageIndex"] = 0;

        //NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", OnGetGameServerListCallBack, isPost: true, dic: dic);

    }

    private void OnGetGameServerListCallBack(RetValue obj)
    {
        if (!obj.HasError)
        {
            List<RetGameServerEntity> list = LitJson.JsonMapper.ToObject<List<RetGameServerEntity>>(obj.Value.ToString());
            if (list != null)
            {
                AppDebug.Log("数量：" + list.Count);

                for (int i = 0; i < list.Count; i++)
                {
                    AppDebug.Log("名字:" + list[i].Name);
                    AppDebug.Log("Id:" + list[i].Id);
                    AppDebug.Log("Ip:" + list[i].Ip);
                    AppDebug.Log("Port:" + list[i].Port);
                }
            }
        }
        else
        {
            AppDebug.LogError(obj.ErrorMsg);
        }
    }

    private void OnGetGameServerPageListCallBack(RetValue obj)
    {
        if (!obj.HasError)
        {
            List<RetGameServerPageEntity> list = LitJson.JsonMapper.ToObject<List<RetGameServerPageEntity>>(obj.Value.ToString());
            if(list != null)
            {
                AppDebug.Log("数量：" + list.Count);
                uiGameServerEnterView.CloseAndOpenNext(WindowUIType.GameServerSelect);
                uiGameServerSelectView.SetGameServerPageUI(list);
            }
        }
        else
        {
            AppDebug.LogError(obj.ErrorMsg);
        }
    }

    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.GameServerEnter:
                OpenGameServerEnterView();
                break;
            case WindowUIType.GameServerSelect:
                OpenGameServerSelectView();
                break;
        }
    }

    private void OpenGameServerEnterView()
    {
        uiGameServerEnterView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerEnter).GetComponent<UIGameServerEnterView>();
    }

    private void OpenGameServerSelectView()
    {
        uiGameServerSelectView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerSelect).GetComponent<UIGameServerSelectView>();
    }

    public override void Dispose()
    {
        base.Dispose();
        RemoveEventHandler(ConstDefine.UIGameServerEnterView_btnEnterGame, UIGameServerEnterView_btnSelectGameServerClick);
    }
}
