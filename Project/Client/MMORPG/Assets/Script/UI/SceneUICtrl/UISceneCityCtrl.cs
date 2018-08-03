//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-06 07:44:32
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 主城UI控制器
/// </summary>
public class UISceneCityCtrl : UISceneBase
{
    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnHead":
                OpenRoleInfo();
                break;
        }
    }

    private void OpenRoleInfo()
    {
        WindowUIMgr.Instance.OpenWindow(WindowUIType.RoleInfo);
    }
}