//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-06 08:01:34
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

/// <summary>
/// 角色信息窗口控制器
/// </summary>
public class UIRoleInfoCtrl : UIWindowBase 
{
    protected override void OnBtnClick(GameObject go)
    {
        switch (go.gameObject.name)
        {
            case "btnClose":
                Close();
                break;
        }
    }
}