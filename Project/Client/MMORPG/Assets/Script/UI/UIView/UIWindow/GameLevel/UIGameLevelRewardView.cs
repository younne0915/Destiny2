using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIGameLevelRewardView : UISubViewBase
{
    [SerializeField]
    private Image imgIco;

    [SerializeField]
    private Text lblName;

    public void SetUI(string name, int goodsId, GoodsType type)
    {
        string path = string.Empty;

        switch (type)
        {
            case GoodsType.Equip:
                path = string.Format("Download/Source/UISource/EquipIco/{0}.assetbundle", goodsId);
                break;
            case GoodsType.Item:
                path = string.Format("Download/Source/UISource/ItemIco/{0}.assetbundle", goodsId);
                break;
            case GoodsType.Material:
                path = string.Format("Download/Source/UISource/MaterialIco/{0}.assetbundle", goodsId);
                break;
        }

        //AssetBundleMgr.Instance.LoadOrDownload<Texture2D>(path, goodsId.ToString(), (Texture2D obj) =>
        //{
        //    //if (obj == null) return;
        //    var iconRect = new Rect(0, 0, obj.width, obj.height);
        //    var iconSprite = Sprite.Create(obj, iconRect, new Vector2(.5f, .5f));

        //    imgIco.overrideSprite = iconSprite;
        //}, type: 1);

        lblName.SetText(name);
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        imgIco = null;
        lblName = null;
    }
}