using UnityEngine;
using System.Collections;
using System;

public class AccountCtrl : Singleton<AccountCtrl>
{
    private UILogOnView uiLogOnView;
    private UIRegView uiRegView;

    public AccountCtrl()
    {
        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UILogOnView_btnLogOn, LogOnViewLogOnClick);
        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UILogOnView_btnToReg, LogOnViewToRegClick);

        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UIRegView_btnReg, RegViewRegClick);
        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UIRegView_btnToLogOn, RegViewToLogOnClick);
    }

    private void RegViewToLogOnClick(object[] param)
    {
        OpenLogOnView();
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
        uiLogOnView = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
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
        uiRegView = WindowUIMgr.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIRegView>();
    }

    public override void Dispose()
    {
        base.Dispose();
        EventDispatcher.Instance.RemoveBtnEventHandler(ConstDefine.UILogOnView_btnLogOn, LogOnViewLogOnClick);
        EventDispatcher.Instance.RemoveBtnEventHandler(ConstDefine.UILogOnView_btnToReg, LogOnViewToRegClick);

        EventDispatcher.Instance.RemoveBtnEventHandler(ConstDefine.UIRegView_btnReg, RegViewRegClick);
        EventDispatcher.Instance.RemoveBtnEventHandler(ConstDefine.UIRegView_btnToLogOn, RegViewToLogOnClick);
    }

}
