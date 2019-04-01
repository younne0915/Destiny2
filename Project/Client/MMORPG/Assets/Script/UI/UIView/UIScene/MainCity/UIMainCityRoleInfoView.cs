//===================================================
//��    �ߣ�����  http://www.u3dol.com  QQȺ��87481002
//����ʱ�䣺2016-06-28 22:38:20
//��    ע��
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainCityRoleInfoView : UISubViewBase
{
    public static UIMainCityRoleInfoView Instance = null;
    /// <summary>
    /// ͷ��
    /// </summary>
    [SerializeField]
    private Image imgHeadPic;

    /// <summary>
    /// �ǳ�
    /// </summary>
    [SerializeField]
    private Text lblNickName;

    /// <summary>
    /// �ȼ�
    /// </summary>
    [SerializeField]
    private Text lblLV;

    /// <summary>
    /// Ԫ��
    /// </summary>
    [SerializeField]
    private Text lblMoney;

    /// <summary>
    /// ���
    /// </summary>
    [SerializeField]
    private Text lblGold;

    /// <summary>
    /// HP
    /// </summary>
    [SerializeField]
    private Slider sliderHP;

    /// <summary>
    /// MP
    /// </summary>
    [SerializeField]
    private Slider sliderMP;

    protected override void OnAwake()
    {
        base.OnAwake();
        Instance = this;
    }

    public void SetUI(string headPic, string nickName, int level, int money, int gold, int currHP, int maxHP, int currMP, int maxMP)
    {
        imgHeadPic.SetImage(RoleMgr.Instance.LoadHeadSprite(headPic));
        lblNickName.SetText(nickName);
        lblLV.SetText(string.Format("LV.{0}", level));
        lblMoney.SetText(money.ToString());
        lblGold.SetText(gold.ToString());
        sliderHP.SetSliderValue((float)currHP / maxHP);
        sliderMP.SetSliderValue((float)currMP / maxMP);
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        imgHeadPic = null;
        lblNickName = null;
        lblLV = null;
        lblMoney = null;
        lblGold = null;
        sliderHP = null;
        sliderMP = null;
    }
}