using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : SystemCtrlBase<PlayerCtrl>, ISystemCtrl
{
    private UIRoleInfoView m_UIRoleInfoView;

    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.RoleInfo:
                OpenRoleInfoView();
                break;
        }
    }

    public void SetMainCityData()
    {
        SetMainCityRoleInfo();
        SetMainCityRoleSkill();
    }

    private void SetMainCityRoleSkill()
    {
        RoleInfoMainPlayer mainPlayerInfo = GlobalInit.Instance.MainPlayerInfo;
        List<TransferData> list = new List<TransferData>();

        if(mainPlayerInfo != null && mainPlayerInfo.SkillList != null)
        {
            for (int i = 0; i < mainPlayerInfo.SkillList.Count; i++)
            {
                RoleInfoSkill roleInfoSkill = mainPlayerInfo.SkillList[i];
                TransferData transferData = new TransferData();
                transferData.SetValue(ConstDefine.SkillId, roleInfoSkill.SkillId);
                transferData.SetValue(ConstDefine.SkillLevel, roleInfoSkill.SkillLevel);
                transferData.SetValue(ConstDefine.SkillSlotsNo, roleInfoSkill.SlotsNo);
                SkillLevelEntity skillLevelEntity = SkillLevelDBModel.Instance.GetSkillLevelEntityBySkillIdAndLevel(roleInfoSkill.SkillId, roleInfoSkill.SkillLevel);
                if(skillLevelEntity != null)
                {
                    transferData.SetValue(ConstDefine.SkillCDTime, skillLevelEntity.SkillCDTime);
                }
                list.Add(transferData);
            }
        }
       
        UIMainCitySkillView.Instance.SetUI(list);
    }

    private void SetMainCityRoleInfo()
    {
        RoleInfoMainPlayer mainPlayerInfo = GlobalInit.Instance.MainPlayerInfo;
        string headPic = string.Empty;

        JobEntity jobEntity = JobDBModel.Instance.Get(mainPlayerInfo.JobId);
        if(jobEntity != null)
        {
            headPic = jobEntity.HeadPic;
        }

        GlobalInit.Instance.CurrPlayer.OnHPChange += OnValueChangeCallback;
        GlobalInit.Instance.CurrPlayer.OnMPChange += OnValueChangeCallback;

        UIMainCityRoleInfoView.Instance.SetUI(headPic, mainPlayerInfo.RoleNickName, 1, mainPlayerInfo.Money,
            mainPlayerInfo.Gold, mainPlayerInfo.CurrHP, mainPlayerInfo.MaxHP, mainPlayerInfo.CurrMP, mainPlayerInfo.MaxMP);
    }

    private void OnValueChangeCallback(ValueChangeType type)
    {
        RoleInfoMainPlayer mainPlayerInfo = GlobalInit.Instance.MainPlayerInfo;
        string headPic = string.Empty;
        JobEntity jobEntity = JobDBModel.Instance.Get(mainPlayerInfo.JobId);
        if (jobEntity != null)
        {
            headPic = jobEntity.HeadPic;
        }
        UIMainCityRoleInfoView.Instance.SetUI(headPic, mainPlayerInfo.RoleNickName, 1, mainPlayerInfo.Money,
            mainPlayerInfo.Gold, mainPlayerInfo.CurrHP, mainPlayerInfo.MaxHP, mainPlayerInfo.CurrMP, mainPlayerInfo.MaxMP);
    }

    public void OpenRoleInfoView()
    {
        m_UIRoleInfoView = UIViewUtil.Instance.OpenWindow(WindowUIType.RoleInfo).GetComponent<UIRoleInfoView>();
        RoleInfoMainPlayer roleInfo = GlobalInit.Instance.MainPlayerInfo;
        TransferData data = new TransferData();
        data.SetValue(ConstDefine.JobId, roleInfo.JobId);
        data.SetValue(ConstDefine.NickName, roleInfo.RoleNickName);
        data.SetValue(ConstDefine.Level, roleInfo.Level);
        data.SetValue(ConstDefine.Fighting, roleInfo.Fighting);

        data.SetValue(ConstDefine.Money, roleInfo.Fighting);
        data.SetValue(ConstDefine.Gold, roleInfo.Fighting);
        data.SetValue(ConstDefine.Attack, roleInfo.Fighting);
        data.SetValue(ConstDefine.Defense, roleInfo.Fighting);
        data.SetValue(ConstDefine.Hit, roleInfo.Fighting);
        data.SetValue(ConstDefine.Dodge, roleInfo.Fighting);
        data.SetValue(ConstDefine.Cri, roleInfo.Fighting);
        data.SetValue(ConstDefine.Res, roleInfo.Fighting);


        data.SetValue(ConstDefine.CurrHP, roleInfo.Fighting);
        data.SetValue(ConstDefine.MaxHP, roleInfo.Fighting);

        data.SetValue(ConstDefine.CurrMP, roleInfo.Fighting);
        data.SetValue(ConstDefine.MaxMP, roleInfo.Fighting);

        data.SetValue(ConstDefine.CurrExp, roleInfo.Fighting);
        data.SetValue(ConstDefine.MaxExp, roleInfo.Fighting);

        m_UIRoleInfoView.SetRoleInfo(data);
    }
}
