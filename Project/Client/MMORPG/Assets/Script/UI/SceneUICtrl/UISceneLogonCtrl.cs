//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-29 16:34:03
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 登录场景UI控制器
/// </summary>
public class UISceneLogonCtrl : UISceneBase
{
    protected override void OnStart()
    {
        base.OnStart();

        StartCoroutine(OpenLogOnWindow());
    }

    private IEnumerator OpenLogOnWindow()
    {
        yield return new WaitForSeconds(.2f);
        GameObject obj = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }
}