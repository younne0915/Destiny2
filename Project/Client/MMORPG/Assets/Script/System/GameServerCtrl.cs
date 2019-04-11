using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameServerCtrl : SystemCtrlBase<GameServerCtrl>, ISystemCtrl
{
    private UIGameServerEnterView uiGameServerEnterView;
    private UIGameServerSelectView uiGameServerSelectView;

    private Dictionary<int, List<RetGameServerEntity>> m_GameServerDic = new Dictionary<int, List<RetGameServerEntity>>();

    private bool m_GameServerBusy = false;

    private int m_GameServerPageIndex = 0;

    public GameServerCtrl()
    {
        AddEventHandler(ConstDefine.UIGameServerEnterView_btnSelectGameServer, UIGameServerEnterView_btnSelectGameServerClick);
        AddEventHandler(ConstDefine.UIGameServerEnterView_btnEnterGame, UIGameServerEnterView_btnEnterGameClick);

        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.System_ServerTimeReturn, OnSystem_ServerTimeReturn);

        NetWorkSocket.Instance.OnConnectOK = OnConnectOKCallback;
    }

    private void OnSystem_ServerTimeReturn(byte[] p)
    {
        System_ServerTimeReturnProto proto = System_ServerTimeReturnProto.GetProto(p);
        GlobalInit.Instance.PingValue = (Time.realtimeSinceStartup * 1000 - proto.LocalTime) / 2;
        GlobalInit.Instance.BeganGameServerTime = proto.ServerTime - (long)GlobalInit.Instance.PingValue;

        //AppDebug.LogError(string.Format("PingValue : {0}, GameServerTime : {1}, clientTime : {2}", GlobalInit.Instance.PingValue, GlobalInit.Instance.BeganGameServerTime, proto.LocalTime));

        UpdateLastLogOnServer();
        SceneMgr.Instance.LoadToSelectRole();
        if (uiGameServerEnterView != null)
        {
            uiGameServerEnterView.Close();
            uiGameServerEnterView = null;
        }
    }

    private void UIGameServerEnterView_btnEnterGameClick(object[] p)
    {
        NetWorkSocket.Instance.Connect(GlobalInit.Instance.CurrentSelectGameServer.Ip, GlobalInit.Instance.CurrentSelectGameServer.Port);
    }

    private void OnConnectOKCallback()
    {
        System_SendLocalTimeProto proto = new System_SendLocalTimeProto();
        proto.LocalTime = GlobalInit.Instance.CheckTime = Time.realtimeSinceStartup * 1000;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());

        //AppDebug.LogError("对表 : " + proto.LocalTime);
    }

    private void UpdateLastLogOnServer()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 2;
        dic["AccountId"] = GlobalInit.Instance.CurrentAccount.Id;
        dic["LastLogOnServerId"] = GlobalInit.Instance.CurrentSelectGameServer.Id;
        dic["LastLogOnServerName"] = GlobalInit.Instance.CurrentSelectGameServer.Name;
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", OnUpdateLastLogOnServerCallBack, isPost: true, dic: dic);
    }

    private void OnUpdateLastLogOnServerCallBack(RetValue obj)
    {
        //AppDebug.Log(obj.HasError);
    }

    private void UIGameServerEnterView_btnSelectGameServerClick(object[] p)
    {
        uiGameServerSelectView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerSelect, ()=>
        {
            uiGameServerSelectView.SetGameServerSelectItemView(GlobalInit.Instance.CurrentSelectGameServer);
            GetGameServerPage();
        }).GetComponent<UIGameServerSelectView>();
    }

    /// <summary>
    /// 获取区服页签列表
    /// </summary>
    private void GetGameServerPage()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 0;
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", OnGetGameServerPageListCallBack, isPost: true, dic: dic);
    }

    private void OnGetGameServerPageListCallBack(RetValue obj)
    {
        if (!obj.HasError)
        {
            List<RetGameServerPageEntity> list = LitJson.JsonMapper.ToObject<List<RetGameServerPageEntity>>(obj.Value.ToString());
            if(list != null)
            {
                RetGameServerPageEntity entity = new RetGameServerPageEntity();
                entity.PageIndex = 0;
                entity.Name = "推荐服务器";
                list.Insert(0, entity);
                GetGameServer(0);
                uiGameServerSelectView.SetGameServerPageUI(list);
                uiGameServerSelectView.OnGameServerPageClick = OnGameServerPageClick;
            }
        }
        else
        {
            AppDebug.LogError(obj.ErrorMsg);
        }
    }

    private void OnGameServerPageClick(int obj)
    {
        GetGameServer(obj);
    }

    /// <summary>
    /// 获取区服列表
    /// </summary>
    private void GetGameServer(int pageIndex)
    {
        m_GameServerPageIndex = pageIndex;
        if (m_GameServerDic.ContainsKey(pageIndex))
        {
            //AppDebug.LogError("already contain " + pageIndex);
            uiGameServerSelectView.SetGameServerUI(m_GameServerDic[pageIndex]);
            uiGameServerSelectView.OnGameServerClick = OnGameServerClickCallback;
            return;
        }
        if (m_GameServerBusy) return;
        m_GameServerBusy = true;

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;
        dic["PageIndex"] = pageIndex;
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", OnGetGameServerListCallBack, isPost: true, dic: dic);
    }

    private void OnGetGameServerListCallBack(RetValue obj)
    {
        m_GameServerBusy = false;
        if (!obj.HasError)
        {
            List<RetGameServerEntity> list = LitJson.JsonMapper.ToObject<List<RetGameServerEntity>>(obj.Value.ToString());
            if (list != null)
            {
                uiGameServerSelectView.SetGameServerUI(list);
                uiGameServerSelectView.OnGameServerClick = OnGameServerClickCallback;
                m_GameServerDic[m_GameServerPageIndex] = list;
            }
        }
        else
        {
            AppDebug.LogError(obj.ErrorMsg);
        }
    }

    private void OnGameServerClickCallback(RetGameServerEntity entity)
    {
        GlobalInit.Instance.CurrentSelectGameServer = entity;
        uiGameServerEnterView.SetUI(entity.Name);
        uiGameServerSelectView.SetGameServerSelectItemView(entity);
        uiGameServerSelectView.Close();
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
        uiGameServerEnterView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerEnter, ()=>
        {
            uiGameServerEnterView.SetUI(GlobalInit.Instance.CurrentSelectGameServer.Name);
        }).GetComponent<UIGameServerEnterView>();
    }

    private void OpenGameServerSelectView()
    {
        uiGameServerSelectView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerSelect).GetComponent<UIGameServerSelectView>();
    }

    public override void Dispose()
    {
        base.Dispose();
        RemoveEventHandler(ConstDefine.UIGameServerEnterView_btnEnterGame, UIGameServerEnterView_btnSelectGameServerClick);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.System_ServerTimeReturn, OnSystem_ServerTimeReturn);
    }
}
