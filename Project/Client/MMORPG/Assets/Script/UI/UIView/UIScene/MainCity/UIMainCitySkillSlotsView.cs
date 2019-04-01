using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainCitySkillSlotsView :UISubViewBase
{
    [HideInInspector]
    public int skillId;

    [HideInInspector]
    public int skillLevel;

    [HideInInspector]
    public byte skillSlots;

    private Action<int> m_OnClick;

    [SerializeField]
    private Image skillIcon;


    [SerializeField]
    private Image cdMaskImg;

    private float m_CDTime = 0;
    private float m_BeganTime = 0;
    public bool IsCD
    {
        get;
        private set;
    }

    protected override void OnStart()
    {
        base.OnStart();
        cdMaskImg.gameObject.SetActive(false);
    }

    public void SetUI(TransferData data)
    {
        skillId = data.GetValue<int>(ConstDefine.SkillId);
        skillLevel = data.GetValue<int>(ConstDefine.SkillLevel);
        skillSlots = data.GetValue<byte>(ConstDefine.SkillSlotsNo);
        m_CDTime = data.GetValue<float>(ConstDefine.SkillCDTime);
        SkillEntity entity = SkillDBModel.Instance.Get(skillId);
        if(entity != null)
        {
            skillIcon.SetImage(RoleMgr.Instance.LoadSkillPic(entity.SkillPic));
        }
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (IsCD)
        {
            cdMaskImg.fillAmount = Mathf.Lerp(1, 0, (Time.time - m_BeganTime) / m_CDTime);
            if(Time.time > m_CDTime + m_BeganTime)
            {
                IsCD = false;
            }
        }
    }

    public void BeganCD()
    {
        IsCD = true;
        m_BeganTime = Time.time;
        cdMaskImg.gameObject.SetActive(true);
    }
}
