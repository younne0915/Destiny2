using UnityEngine;
using System.Collections;

public class UILogOnView : UIWindowViewBase
{
    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnLogOn":
                Debug.Log("click btnLogOn");
                break;
            case "btnToReg":
                Debug.Log("click btnToReg");
                break;
        }
    }
}
