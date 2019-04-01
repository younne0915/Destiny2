using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// ����ؿ���ͼ���������ͼ
/// </summary>
public class UIGameLevelMapItemView : UISubViewBase
{
    /// <summary>
    /// ����
    /// </summary>
    [SerializeField]
    private Text txtName;

    /// <summary>
    /// ͼ��
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
        imgIco.overrideSprite = GameUtil.LoadGameLevelIcon(data.GetValue<string>(ConstDefine.GameLevelIco));
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        txtName = null;
        imgIco = null;
        m_OnGameLevelItemClick = null;
    }
}