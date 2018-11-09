using UnityEngine;
using System.Collections;

public class UIRegView : UIWindowViewBase {

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnReg":
                EventDispatcher.Instance.DispatchBtn(ConstDefine.UIRegView_btnReg);
                break;
            case "btnToLogOn":
                EventDispatcher.Instance.DispatchBtn(ConstDefine.UIRegView_btnToLogOn);
                break;
        }
    }
}
