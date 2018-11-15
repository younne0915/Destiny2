using UnityEngine;
using System.Collections;
using System;

public class AccountCtrl : Singleton<AccountCtrl>, ISystemCtrl
{
    private UILogOnView uiLogOnView;
    private UIRegView uiRegView;

    public AccountCtrl()
    {
        UIDispatcher.Instance.AddEventHandler(ConstDefine.UILogOnView_btnLogOn, LogOnViewLogOnClick);
        UIDispatcher.Instance.AddEventHandler(ConstDefine.UILogOnView_btnToReg, LogOnViewToRegClick);

        UIDispatcher.Instance.AddEventHandler(ConstDefine.UIRegView_btnReg, RegViewRegClick);
        UIDispatcher.Instance.AddEventHandler(ConstDefine.UIRegView_btnToLogOn, RegViewToLogOnClick);
    }

    private void RegViewToLogOnClick(object[] param)
    {
        if(uiRegView != null)
        {
            uiRegView.Close(true);
        }
    }

    private void RegViewRegClick(object[] param)
    {
        
    }

    private void LogOnViewLogOnClick(object[] param)
    {
        Debug.LogError("on btn logOn click");
    }

    private void LogOnViewToRegClick(object[] param)
    {
        if (uiLogOnView != null)
        {
            uiLogOnView.Close(true);
        }
    }

    public void OpenLogOnView()
    {
        uiLogOnView = UIViewUtil.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
        if(uiLogOnView != null)
        {
            uiLogOnView.OnViewClose += () => 
            {
                OpenRegWindow();
            };
        }
    }

    private void OpenRegWindow()
    {
        uiRegView = UIViewUtil.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
        if(uiRegView != null)
        {
            uiRegView.OnViewClose += () =>
            {
                OpenLogOnView();
            };
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.UILogOnView_btnLogOn, LogOnViewLogOnClick);
        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.UILogOnView_btnToReg, LogOnViewToRegClick);

        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.UIRegView_btnReg, RegViewRegClick);
        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.UIRegView_btnToLogOn, RegViewToLogOnClick);
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
}
