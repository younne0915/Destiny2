using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIWorldMapItemView : UISubViewBase
{
    /// <summary>
    /// 名称
    /// </summary>
    [SerializeField]
    private Text txtName;

    /// <summary>
    /// 图标
    /// </summary>
    [SerializeField]
    private Image imgIco;

    private int m_WroldMapSceneId;

    public Action<int> OnWorldMapItemClick;

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(BtnClick);
    }

    /// <summary>
    /// 按钮点击
    /// </summary>
    private void BtnClick()
    {
        if (OnWorldMapItemClick != null)
        {
            OnWorldMapItemClick(m_WroldMapSceneId);
        }
    }

    public void SetUI(TransferData data, Action<int> onWorldMapItemClick)
    {
        OnWorldMapItemClick = onWorldMapItemClick;

        txtName.SetText(data.GetValue<string>(ConstDefine.WorldMapName));
        m_WroldMapSceneId = data.GetValue<int>(ConstDefine.WorldMapId);

        string picName = data.GetValue<string>(ConstDefine.WorldMapIco);

        imgIco.overrideSprite = GameUtil.LoadWorldMapIcon(picName);
        //AssetBundleMgr.Instance.LoadOrDownload<Texture2D>(string.Format("Download/Source/UISource/WorldMap/{0}.assetbundle", picName), picName, (Texture2D obj) =>
        //{
        //    if (obj == null) return;

        //    var iconRect = new Rect(0, 0, obj.width, obj.height);
        //    var iconSprite = Sprite.Create(obj, iconRect, new Vector2(.5f, .5f));

        //    imgIco.overrideSprite = iconSprite;
        //}, type: 1);
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        txtName = null;
        imgIco = null;
    }
}
