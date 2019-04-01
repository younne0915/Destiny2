using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoleEquipView : UISubViewBase {

    [SerializeField]
    private Transform RoleModelContainer;

    [SerializeField]
    private Text lblNickName;

    [SerializeField]
    private Text lblLevel;

    [SerializeField]
    private Text lblFighting;

    private int m_JobId;

	protected override void OnStart()
    {
        CloneRoleModel();
    }

    public void SetUI(TransferData data)
    {
        m_JobId = data.GetValue<byte>(ConstDefine.JobId);
        lblNickName.text = data.GetValue<string>(ConstDefine.NickName);
        lblLevel.text = string.Format("Lv.{0}", data.GetValue<int>(ConstDefine.Level));
        lblFighting.text = string.Format("×ÛºÏÕ½¶·Á¦£º<color='#ff0000'>{0}</color>", data.GetValue<int>(ConstDefine.Fighting));
    }

    public void CloneRoleModel()
    {
        GameObject obj = RoleMgr.Instance.LoadPlayer(m_JobId);
        obj.SetParent(RoleModelContainer);
        obj.SetLayer("UI");
    }
}
