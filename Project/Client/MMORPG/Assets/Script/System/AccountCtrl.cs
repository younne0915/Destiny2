using UnityEngine;
using System.Collections;
using System;

public class AccountCtrl : Singleton<AccountCtrl>
{
    private UILogOnView uiLogOnView;

    public AccountCtrl()
    {
        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UILogOnView_btnLogOn, LogOnViewLogOnClick);
        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UILogOnView_btnToReg, LogOnViewToRegClick);

        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UIRegView_btnReg, RegViewRegClick);
        EventDispatcher.Instance.AddBtnEventHandler(ConstDefine.UIRegView_btnToLogOn, RegViewToLogOnClick);
    }

    private void RegViewToLogOnClick(object[] param)
    {
        WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);

        Debug.LogError("RegViewToLogOnClick");
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
        //if (uiLogOnView != null)
        //{
        //    uiLogOnView.Close();
        //    uiLogOnView.NextOpenWindow = WindowUIType.Reg;
        //}
        WindowUIMgr.Instance.OpenWindow(WindowUIType.Reg);
    }

    public void OpenLogOnView()
    {
        uiLogOnView = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
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
