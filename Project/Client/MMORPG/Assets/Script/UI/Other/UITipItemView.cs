using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITipItemView : UISubViewBase
{

    [SerializeField]
    private Text lblText;

    [SerializeField]
    private Image imgIco;

    public void SetUI(Sprite sprite, string text)
    {
        imgIco.overrideSprite = sprite;
        imgIco.SetNativeSize();
        lblText.SetText(string.Format("+ {0}", text));
    }
}
