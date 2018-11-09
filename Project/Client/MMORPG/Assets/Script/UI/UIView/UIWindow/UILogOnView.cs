using UnityEngine;
using System.Collections;

public class UILogOnView : UIWindowViewBase
{
    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnLogOn":
                EventDispatcher.Instance.DispatchBtn(ConstDefine.UILogOnView_btnLogOn);
                break;
            case "btnToReg":
                EventDispatcher.Instance.DispatchBtn(ConstDefine.UILogOnView_btnToReg);
                break;
        }
    }
}
