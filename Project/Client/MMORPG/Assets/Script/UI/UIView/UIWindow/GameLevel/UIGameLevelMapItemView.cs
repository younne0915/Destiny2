using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// 剧情关卡地图上子项的视图
/// </summary>
public class UIGameLevelMapItemView : UISubViewBase
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

    private int m_GameLevelId;

    private Action<int> m_OnGameLevelItemClick;

    protected override void OnStart()
    {
        base.OnStart();

        GetComponent<Button>().onClick.AddListener(OnBtnClick);
    }

    private void OnBtnClick()
    {
        if(m_OnGameLevelItemClick != null)
        {
            m_OnGameLevelItemClick(m_GameLevelId);
        }
    }

    public void SetUI(TransferData data, Action<int> onGameLevelItemClick)
    {
        m_OnGameLevelItemClick = onGameLevelItemClick;
        txtName.SetText(data.GetValue<string>(ConstDefine.GameLevelName));
        m_GameLevelId = data.GetValue<int>(ConstDefine.GameLevelId);
        //imgIco.overrideSprite = GameUtil.LoadGameLevelIcon(data.GetValue<string>(ConstDefine.GameLevelIco));

        LoaderMgr.Instance.LoadOrDownload<Texture2D>(string.Format("Download/Source/UISource/GameLevel/GameLevelIcon/{0}", data.GetValue<string>(ConstDefine.GameLevelIco)), data.GetValue<string>(ConstDefine.GameLevelIco), (Texture2D obj) =>
        {
            if (obj == null) return;

            var iconRect = new Rect(0, 0, obj.width, obj.height);
            var iconSprite = Sprite.Create(obj, iconRect, new Vector2(.5f, .5f));

            imgIco.overrideSprite = iconSprite;
        }, type: 1);
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        txtName = null;
        imgIco = null;
        m_OnGameLevelItemClick = null;
    }
}