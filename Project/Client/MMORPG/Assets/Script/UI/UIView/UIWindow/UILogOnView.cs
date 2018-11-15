using UnityEngine;
using System.Collections;

public class UILogOnView : UIWindowViewBase
{
    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnLogOn":
                UIDispatcher.Instance.Dispatch(ConstDefine.UILogOnView_btnLogOn);
                break;
            case "btnToReg":
                UIDispatcher.Instance.Dispatch(ConstDefine.UILogOnView_btnToReg);
                break;
        }
    }
}
