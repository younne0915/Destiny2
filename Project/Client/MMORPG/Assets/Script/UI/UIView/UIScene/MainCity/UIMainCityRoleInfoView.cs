//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-06-28 22:38:20
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMainCityRoleInfoView : UISubViewBase
{
    public static UIMainCityRoleInfoView Instance = null;
    /// <summary>
    /// 头像
    /// </summary>
    [SerializeField]
    private Image imgHeadPic;

    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private Text lblNickName;

    /// <summary>
    /// 等级
    /// </summary>
    [SerializeField]
    private Text lblLV;

    /// <summary>
    /// 元宝
    /// </summary>
    [SerializeField]
    private Text lblMoney;

    /// <summary>
    /// 金币
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