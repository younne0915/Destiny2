using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoleInfoView : UIWindowViewBase {

    [SerializeField]
    private UIRoleEquipView m_UIRoleEquipView;

    [SerializeField]
    private UIRoleInfoDetailView m_UIRoleInfoDetailView;

    public void SetRoleInfo(TransferData data)
    {
        m_UIRoleEquipView.SetUI(data);
        m_UIRoleInfoDetailView.SetUI(data);
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);

    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

    }
}
