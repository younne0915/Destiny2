using UnityEngine;
using System.Collections;

public class AccountCtrl : Singleton<AccountCtrl>
{

    public void OpenLogOnView()
    {
        WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }
}
