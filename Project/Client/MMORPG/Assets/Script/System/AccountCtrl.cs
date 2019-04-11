using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AccountCtrl : SystemCtrlBase<AccountCtrl>, ISystemCtrl
{
    private UILogOnView uiLogOnView;
    private UIRegView uiRegView;

    private bool m_AutoLogOn = false;

    public AccountCtrl()
    {
        AddEventHandler(ConstDefine.UILogOnView_btnLogOn, LogOnViewLogOnClick);
        AddEventHandler(ConstDefine.UILogOnView_btnToReg, LogOnViewToRegClick);

        AddEventHandler(ConstDefine.UIRegView_btnReg, RegViewRegClick);
        AddEventHandler(ConstDefine.UIRegView_btnToLogOn, RegViewToLogOnClick);
    }

    private void SetCurrentSelectGameServer(RetAccountEntity entity)
    {
        RetGameServerEntity retGameServerEntity = new RetGameServerEntity();
        retGameServerEntity.Id = entity.LastServerId;
        retGameServerEntity.Name = entity.LastServerName;
        retGameServerEntity.Ip = entity.LastServerIp;
        retGameServerEntity.Port = entity.LastPort;
        GlobalInit.Instance.CurrentSelectGameServer = retGameServerEntity;
        GlobalInit.Instance.CurrentAccount = entity;
    }

    private void RegViewToLogOnClick(object[] param)
    {
        if(uiRegView != null)
        {
            uiRegView.CloseAndOpenNext(WindowUIType.LogOn);
        }
    }

    private void RegViewRegClick(object[] param)
    {
        if (string.IsNullOrEmpty(uiRegView.txtUserName.text))
        {
            //AppDebug.LogError("请输入用户名");
            ShowMessage("注册提示", "请输入用户名");
            return;
        }

        if (string.IsNullOrEmpty(uiRegView.txtPwd.text))
        {
            //AppDebug.LogError("请输入密码");
            ShowMessage("注册提示", "请输入密码", MessageViewType.OkAndCancel,okAction:()=> 
            {
                LogError("请输入密码");
            });
            return;
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 0;
        dic["UserName"] = uiRegView.txtUserName.text;
        dic["Pwd"] = uiRegView.txtPwd.text;
        dic["ChannelId"] = 0;

        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", OnRegCallBack, isPost: true, dic : dic);
    }

    private void OnRegCallBack(RetValue obj)
    {
        if (obj.HasError)
        {
            //AppDebug.LogError("注册失败！" + obj.ErrorMsg);
            ShowMessage("注册提示", obj.ErrorMsg);
        }
        else
        {
            RetAccountEntity retAccountEntity = LitJson.JsonMapper.ToObject<RetAccountEntity>(obj.Value.ToString());
            SetCurrentSelectGameServer(retAccountEntity);
            if (uiRegView != null)
            {
                PlayerPrefs.SetString(ConstDefine.LogOn_AccountUserName, uiRegView.txtUserName.text);
                PlayerPrefs.SetString(ConstDefine.LogOn_AccountPwd, uiRegView.txtPwd.text);
                PlayerPrefs.SetInt(ConstDefine.LogOn_AccountID, (int)retAccountEntity.Id);

                uiRegView.CloseAndOpenNext(WindowUIType.GameServerEnter);
            }
        }
    }

    private void LogOnViewLogOnClick(object[] param)
    {
        if (string.IsNullOrEmpty(uiLogOnView.txtUserName.text))
        {
            ShowMessage("登录提示", "请输入用户名！");
            return;
        }

        if (string.IsNullOrEmpty(uiLogOnView.txtPwd.text))
        {
            ShowMessage("登录提示", "请输入密码！");
            return;
        }

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;
        dic["UserName"] = uiLogOnView.txtUserName.text;
        dic["Pwd"] = uiLogOnView.txtPwd.text;

        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", OnLogOnCallBack, isPost: true, dic: dic);

    }

    private void OnLogOnCallBack(RetValue obj)
    {
        if (obj.HasError)
        {
            //AppDebug.LogError("登录失败！" + obj.ErrorMsg);
            ShowMessage("登录提示", obj.ErrorMsg);
        }
        else
        {
            RetAccountEntity retAccountEntity = LitJson.JsonMapper.ToObject<RetAccountEntity>(obj.Value.ToString());
            SetCurrentSelectGameServer(retAccountEntity);
            if (!m_AutoLogOn)
            {
                PlayerPrefs.SetString(ConstDefine.LogOn_AccountUserName, uiLogOnView.txtUserName.text);
                PlayerPrefs.SetString(ConstDefine.LogOn_AccountPwd, uiLogOnView.txtPwd.text);
                PlayerPrefs.SetInt(ConstDefine.LogOn_AccountID, retAccountEntity.Id);
                uiLogOnView.CloseAndOpenNext(WindowUIType.GameServerEnter);
            }
            else
            {
                UIViewMgr.Instance.OpenWindow(WindowUIType.GameServerEnter);
            }
        }
    }

    private void LogOnViewToRegClick(object[] param)
    {
        if (uiLogOnView != null)
        {
            uiLogOnView.CloseAndOpenNext(WindowUIType.Reg);
        }
    }

    public void OpenLogOnView()
    {
        uiLogOnView = UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
    }

    private void OpenRegWindow()
    {
        uiRegView = UIViewUtil.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
    }

    public override void Dispose()
    {
        base.Dispose();
        RemoveEventHandler(ConstDefine.UILogOnView_btnLogOn, LogOnViewLogOnClick);
        RemoveEventHandler(ConstDefine.UILogOnView_btnToReg, LogOnViewToRegClick);

        RemoveEventHandler(ConstDefine.UIRegView_btnReg, RegViewRegClick);
        RemoveEventHandler(ConstDefine.UIRegView_btnToLogOn, RegViewToLogOnClick);
    }

    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.LogOn:
                OpenLogOnView();
                break;
            case WindowUIType.Reg:
                OpenRegWindow();
                break;
        }
    }

    public void QuickLogOn()
    {
        //if (!PlayerPrefs.HasKey(ConstDefine.LogOn_AccountID))
        if(true)
        {
            OpenView(WindowUIType.Reg);
        }
        else
        {
            m_AutoLogOn = true;
            string logOnUserName = PlayerPrefs.GetString(ConstDefine.LogOn_AccountUserName);
            string logOnPwd = PlayerPrefs.GetString(ConstDefine.LogOn_AccountPwd);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["Type"] = 1;
            dic["UserName"] = logOnUserName;
            dic["Pwd"] = logOnPwd;

            NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", OnLogOnCallBack, isPost: true, dic: dic);
        }
    }
}
