using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISceneSelectRoleView : UISceneViewBase
{
    public UISelectRoleDragView SelectRoleDragView;
    public UISelectRoleJobItemView[] JobItems;
    public UISelectRoleJobDescView SelectRoleJobDescView;

    public InputField txtRoleName;

    [SerializeField]
    private Transform[] UICreateRole;

    [SerializeField]
    private Transform[] UISelectRole;

    [SerializeField]
    private Sprite[] m_RoleHeadPic;

    [SerializeField]
    private Transform m_RoleListContainer;

    [SerializeField]
    private GameObject m_RoleItemPrefab;

    public List<UISelectRoleItemView> m_RoleItemViewList;

    /// <summary>
    /// É¾³ý½ÇÉ«ÊÓÍ¼
    /// </summary>
    [SerializeField]
    private UISelectRoleDeleteRoleView m_DeleteRoleView;

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        SelectRoleDragView = null;
        JobItems.SetNull();
        SelectRoleJobDescView = null;
        txtRoleName = null;
        UICreateRole.SetNull();
        UISelectRole.SetNull();
        m_RoleHeadPic.SetNull();
        m_RoleListContainer = null;
        m_RoleItemPrefab = null;
        if(m_RoleItemViewList != null)
        {
            m_RoleItemViewList.ToArray().SetNull();
        }
        m_DeleteRoleView = null;
    }

    protected override void OnStart()
    {
        base.OnAwake();
        SetUICreateRoleShow(false);
        RandomName();
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnRandomName":
                RandomName();
                break;
            case "btnBeginGame":
                UIDispatcher.Instance.Dispatch(ConstDefine.SelectRole_BeginGame);
                break;
            case "btnDeleteRole":
                UIDispatcher.Instance.Dispatch(ConstDefine.SelectRole_DeleteRole);
                break;
            case "btnReturn":
                UIDispatcher.Instance.Dispatch(ConstDefine.SelectRole_Return);
                break;
            case "btnCreateRole":
                UIDispatcher.Instance.Dispatch(ConstDefine.SelectRole_CreateRole);
                break;
        }
    }

    public void RandomName()
    {
        txtRoleName.text = GameUtil.RandomName();
    }

    public void SetUICreateRoleShow(bool isShow)
    {
        if(UICreateRole != null)
        {
            for (int i = 0; i < UICreateRole.Length; i++)
            {
                UICreateRole[i].gameObject.SetActive(isShow);
            }
        }

        if(UISelectRole != null)
        {
            for (int i = 0; i < UISelectRole.Length; i++)
            {
                UISelectRole[i].gameObject.SetActive(!isShow);
            }
        }
    }

    public void SetRoleList(List<RoleOperation_LogOnGameServerReturnProto.RoleItem> list, Action<int> OnSelectRole)
    {
        if(list != null)
        {
            ClearRoleListUI();

            for (int i = 0; i < list.Count; i++)
            {
                GameObject obj = Instantiate(m_RoleItemPrefab);
                UISelectRoleItemView view = obj.GetComponent<UISelectRoleItemView>();
                if(view != null)
                {
                    view.SetUI(list[i].RoleId, list[i].RoleNickName, list[i].RoleLevel, list[i].RoleJob, m_RoleHeadPic[list[i].RoleJob - 1], OnSelectRole);
                }
                obj.transform.parent = m_RoleListContainer;
                obj.transform.localPosition = new Vector3(0, -100 * i, 0);
                obj.transform.localScale = Vector3.one;
                m_RoleItemViewList.Add(view);
            }
        }
    }

    public void ClearRoleListUI()
    {
        for (int i = 0; i < m_RoleItemViewList.Count; i++)
        {
            Destroy(m_RoleItemViewList[i].gameObject);
        }
        m_RoleItemViewList.Clear();
    }

    public void DeleteSelectRole(string nickName, Action onBtnOkClick)
    {
        m_DeleteRoleView.Show(nickName, onBtnOkClick);
    }

    public void CloseDeleteRoleView()
    {
        m_DeleteRoleView.Close();
    }
}
