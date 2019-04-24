//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-07-25 21:32:52
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIGameLevelDetailView : UIWindowViewBase
{
    /// <summary>
    /// 关卡名称
    /// </summary>
    [SerializeField]
    private Text lblGameLevelName;

    [SerializeField]
    private Image imgDetail;

    [SerializeField]
    private Text lblGold;

    [SerializeField]
    private Text lblExp;

    [SerializeField]
    private Text lblDescription;

    [SerializeField]
    private Text lblCondition;

    [SerializeField]
    private Text lblCommendFighting;

    /// <summary>
    /// 已经选择的难度等级颜色
    /// </summary>
    [SerializeField]
    private Color selectedGradeColor;

    //默认的颜色
    [SerializeField]
    private Color normalGradeColor;

    /// <summary>
    /// 难度等级按钮数组
    /// </summary>
    [SerializeField]
    private Image[] btnGrades;

    private int m_GameLevelId;

    public delegate void OnBtnGradeClick(int gameLevelId, GameLevelGrade grade);
    public OnBtnGradeClick OnBtnGradeChangeClick;

    public OnBtnGradeClick OnBtnEnterGameLevelClick;

    private GameLevelGrade m_CurrSelectGrade = GameLevelGrade.Normal;

    protected override void OnStart()
    {
        base.OnStart();
        ResetGradeColor();
        if(btnGrades != null && btnGrades.Length > 0)
        {
            btnGrades[0].color = selectedGradeColor;
        }
    }

    private void ResetGradeColor()
    {
        if (btnGrades != null)
        {
            for (int i = 0; i < btnGrades.Length; i++)
            {
                btnGrades[i].color = normalGradeColor;
            }
        }
    }


    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);

        switch (go.name)
        {
            case "btnNormal":
                BtnGradeChangeClick(GameLevelGrade.Normal);
                break;
            case "btnHard":
                BtnGradeChangeClick(GameLevelGrade.Hard);
                break;
            case "btnHell":
                BtnGradeChangeClick(GameLevelGrade.Hell);
                break;
            case "btnEnter":
                if(OnBtnEnterGameLevelClick != null)
                {
                    OnBtnEnterGameLevelClick(m_GameLevelId, m_CurrSelectGrade);
                }
                break;
        }
    }

    private void BtnGradeChangeClick(GameLevelGrade grade)
    {
        if (m_CurrSelectGrade == grade) return;
        m_CurrSelectGrade = grade;

        ResetGradeColor();
        btnGrades[(int)m_CurrSelectGrade].color = selectedGradeColor;

        if (OnBtnGradeChangeClick != null)
        {
            OnBtnGradeChangeClick(m_GameLevelId, grade);
        }
    }

    public void SetUI(TransferData data)
    {
        m_GameLevelId = data.GetValue<int>(ConstDefine.GameLevelId);
        //imgDetail.SetImage(GameUtil.LoadGameLevelDetailImg(data.GetValue<string>(ConstDefine.GameLevelDlgPic)));
        LoaderMgr.Instance.LoadOrDownload<Sprite>(string.Format("Download/Source/UISource/GameLevel/GameLevelDetail/{0}", data.GetValue<string>(ConstDefine.GameLevelDlgPic)), data.GetValue<string>(ConstDefine.GameLevelDlgPic), (Sprite obj) =>
        {
            imgDetail.overrideSprite = obj;
        }, type: 1);

        lblGameLevelName.SetText(data.GetValue<string>(ConstDefine.GameLevelName));

        lblExp.SetText(data.GetValue<int>(ConstDefine.GameLevelExp).ToString());
        lblGold.SetText(data.GetValue<int>(ConstDefine.GameLevelGold).ToString());
        lblDescription.SetText(data.GetValue<string>(ConstDefine.GameLevelDesc));
        lblCondition.SetText(data.GetValue<string>(ConstDefine.GameLevelConditionDesc));
        lblCommendFighting.SetText(data.GetValue<int>(ConstDefine.GameLevelCommendFighting).ToString());

    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        lblGameLevelName = null;
        imgDetail = null;
        lblGold = null;
        lblExp = null;
        lblDescription = null;
        lblCondition = null;
        lblCommendFighting = null;
    }
}