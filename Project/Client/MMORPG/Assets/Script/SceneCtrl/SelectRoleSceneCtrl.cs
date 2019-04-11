using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelectRoleSceneCtrl : MonoBehaviour
{
    private List<JobEntity> m_Jobist;

    public Transform[] CreateRoleContainers;
    private Dictionary<int, RoleCtrl> m_JobRoleCtrl = new Dictionary<int, RoleCtrl>();

    private UISceneSelectRoleView m_SceneSelectRoleView;

    [SerializeField]
    private Transform[] CreateRoleSceneModel;

    private List<RoleOperation_LogOnGameServerReturnProto.RoleItem> m_RoleList;
    private GameObject m_CurrSelectRoleModel;
    private int m_CurrSelectRoleId;

    private bool m_IsCreateRole = false;

    private int m_CurrentJobId;
    private int CurrentJobId
    {
        set
        {
            m_CurrentJobId = value;
            SetSelectJob();
        }
        get
        {
            return m_CurrentJobId;
        }
    }

    void Awake()
    {
        GameObject obj = UISceneCtrl.Instance.LoadSceneUI(UISceneCtrl.SceneUIType.SelectRole);
        if(obj != null)
        {
            m_SceneSelectRoleView = obj.GetComponent<UISceneSelectRoleView>();
            if(m_SceneSelectRoleView != null)
            {
                m_SceneSelectRoleView.SelectRoleDragView.OnSelectRoleDrag = OnSelectRoleDrag;

                if(m_SceneSelectRoleView.JobItems != null && m_SceneSelectRoleView.JobItems.Length > 0)
                {
                    for (int i = 0; i < m_SceneSelectRoleView.JobItems.Length; i++)
                    {
                        m_SceneSelectRoleView.JobItems[i].OnSelectJob = OnSelectJob;
                    }
                }
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        //加载完毕
        if (DelegateDefine.Instance.OnSceneLoadOK != null)
        {
            DelegateDefine.Instance.OnSceneLoadOK();
        }

        AppDebug.Log("SelectRoleSceneCtrl 加载完毕");
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturn);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.RoleOperation_EnterGameReturn, OnEnterGameReturn);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.RoleOperation_DeleteRoleReturn, OnDeleteRoleReturn);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.RoleOperation_SelectRoleInfoReturn, OnSelectRoleInfoReturn);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.RoleData_SkillReturn, OnSkillReturn);

        UIDispatcher.Instance.AddEventHandler(ConstDefine.SelectRole_BeginGame, OnBtnBeginGameClick);
        UIDispatcher.Instance.AddEventHandler(ConstDefine.SelectRole_DeleteRole, OnBtnSelectRole_DeleteRoleClick);
        UIDispatcher.Instance.AddEventHandler(ConstDefine.SelectRole_Return, OnBtnSelectRole_BackClick);
        UIDispatcher.Instance.AddEventHandler(ConstDefine.SelectRole_CreateRole, OnBtnSelectRole_CreateRoleClick);

        LogOnGameServer();
        m_Jobist = JobDBModel.Instance.GetList();
        CurrentJobId = 1;
    }

    private void OnSkillReturn(byte[] p)
    {
        RoleData_SkillReturnProto proto = RoleData_SkillReturnProto.GetProto(p);
        RoleData_SkillReturnProto.SkillData skillData;
        for (int i = 0; i < proto.CurrSkillDataList.Count; i++)
        {
            skillData = proto.CurrSkillDataList[i];
            AppDebug.LogError("skillId : " + skillData.SkillId);
            GlobalInit.Instance.MainPlayerInfo.SkillList.Add(new RoleInfoSkill(skillData.SkillId, skillData.SkillLevel, skillData.SlotsNo));
        }
        SceneMgr.Instance.LoadToWorldMap(GlobalInit.Instance.MainPlayerInfo.LastInWorldMapId);
        //SceneMgr.Instance.LoadToWorldMap(3);
    }

    private void OnDestroy()
    {
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturn);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.RoleOperation_EnterGameReturn, OnEnterGameReturn);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.RoleOperation_DeleteRoleReturn, OnDeleteRoleReturn);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.RoleOperation_SelectRoleInfoReturn, OnSelectRoleInfoReturn);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.RoleData_SkillReturn, OnSkillReturn);

        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.SelectRole_BeginGame, OnBtnBeginGameClick);
        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.SelectRole_DeleteRole, OnBtnSelectRole_DeleteRoleClick);
        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.SelectRole_Return, OnBtnSelectRole_BackClick);
        UIDispatcher.Instance.RemoveEventHandler(ConstDefine.SelectRole_CreateRole, OnBtnSelectRole_CreateRoleClick);
    }

    private void OnBtnSelectRole_CreateRoleClick(object[] p)
    {
        ToCreateRoleUI();
    }

    private void OnBtnSelectRole_BackClick(object[] p)
    {
        if (m_IsCreateRole)
        {
            if(m_RoleList == null || m_RoleList.Count == 0)
            {
                NetWorkSocket.Instance.Disconnect();
                SceneMgr.Instance.LoadToLogOn();
            }
            else
            {
                m_CurrSelectRoleId = 0;
                m_IsCreateRole = false;
                foreach (var item in m_JobRoleCtrl.Values)
                {
                    Destroy(item.gameObject);
                }
                DragTarget.eulerAngles = Vector3.up * 0;
                m_JobRoleCtrl.Clear();
                //选择已有角色
                m_SceneSelectRoleView.SetRoleList(m_RoleList, SelectRoleCallback);
                SelectRoleCallback(m_RoleList[0].RoleId);
                m_SceneSelectRoleView.SetUICreateRoleShow(false);
                SetCreateRoleSceneModelShow(false);
            }
        }
        else
        {
            NetWorkSocket.Instance.Disconnect();
            SceneMgr.Instance.LoadToLogOn();
        }
    }

    private void OnBtnSelectRole_DeleteRoleClick(object[] p)
    {
        RoleOperation_LogOnGameServerReturnProto.RoleItem roleItem = GetRoleItem(m_CurrSelectRoleId);
        m_SceneSelectRoleView.DeleteSelectRole(roleItem.RoleNickName, OnDeleteRoleOkCallback);
    }

    private void OnDeleteRoleOkCallback()
    {
        RoleOperation_DeleteRoleProto proto = new RoleOperation_DeleteRoleProto();
        proto.RoleId = m_CurrSelectRoleId;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    private void OnDeleteRoleReturn(byte[] p)
    {
        RoleOperation_DeleteRoleReturnProto responseProto = RoleOperation_DeleteRoleReturnProto.GetProto(p);
        if (responseProto.IsSuccess)
        {
            AppDebug.Log("删除角色成功");
            DeleteRole(m_CurrSelectRoleId);
            m_SceneSelectRoleView.CloseDeleteRoleView();
        }
        else
        {
            MessageCtrl.Instance.Show("提示","删除角色失败");
        }
    }

    private void DeleteRole(int roleId)
    {
        for (int i = m_RoleList.Count -1; i >= 0; --i)
        {
            if(m_RoleList[i].RoleId == roleId)
            {
                m_RoleList.RemoveAt(i);
            }
        }

        m_IsCreateRole = m_RoleList.Count == 0;
        if (m_IsCreateRole)
        {
            //新建角色
            ToCreateRoleUI();
        }
        else
        {
            //选择角色
            ToSelectRoleUI();
        }
    }

    private void OnBtnBeginGameClick(object[] p)
    {
        if (m_IsCreateRole)
        {
            if (string.IsNullOrEmpty(m_SceneSelectRoleView.txtRoleName.text))
            {
                MessageCtrl.Instance.Show("提示", "请输入您的昵称");
            }
            else
            {
                RoleOperation_CreateRoleProto proto = new RoleOperation_CreateRoleProto();
                proto.JobId = (byte)CurrentJobId;
                proto.RoleNickName = m_SceneSelectRoleView.txtRoleName.text;
                NetWorkSocket.Instance.SendMsg(proto.ToArray());
            }
        }
        else
        {
            RoleOperation_EnterGameProto proto = new RoleOperation_EnterGameProto();
            proto.RoleId = m_CurrSelectRoleId;
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }

    private void ToCreateRoleUI()
    {
        m_IsCreateRole = true;
        if (m_CurrSelectRoleModel != null)
        {
            Destroy(m_CurrSelectRoleModel);
        }
        CloneCreateRoleModel();
        CurrentJobId = 1;
        m_SceneSelectRoleView.ClearRoleListUI();
        m_SceneSelectRoleView.SetUICreateRoleShow(true);
        SetCreateRoleSceneModelShow(true);
    }

    private void ToSelectRoleUI()
    {
        m_SceneSelectRoleView.SetRoleList(m_RoleList, SelectRoleCallback);
        SelectRoleCallback(m_RoleList[0].RoleId);
        m_SceneSelectRoleView.SetUICreateRoleShow(false);
        SetCreateRoleSceneModelShow(false);
    }

    private void OnEnterGameReturn(byte[] p)
    {
        RoleOperation_EnterGameReturnProto responseProto = RoleOperation_EnterGameReturnProto.GetProto(p);
        if (responseProto.IsSuccess)
        {
            AppDebug.Log("登录游戏成功");
        }
        else
        {
            AppDebug.Log("登录游戏失败");
        }
    }

    private void OnCreateRoleReturn(byte[] p)
    {
        RoleOperation_CreateRoleReturnProto proto = RoleOperation_CreateRoleReturnProto.GetProto(p);

        if (proto.IsSuccess)
        {
            AppDebug.Log("创建角色成功");
        }
        else
        {
            MessageCtrl.Instance.Show("提示", "创建角色失败");
        }
    }

    private void LogOnGameServer()
    {
        RoleOperation_LogOnGameServerProto proto = new RoleOperation_LogOnGameServerProto();
        proto.AccountId = GlobalInit.Instance.CurrentAccount.Id;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    private void OnLogOnGameServerReturn(byte[] p)
    {
        CurrentJobId = 1;
        RoleOperation_LogOnGameServerReturnProto resultProto = RoleOperation_LogOnGameServerReturnProto.GetProto(p);
        //AppDebug.Log("roleCount = "+resultProto.RoleCount);
        m_IsCreateRole = resultProto.RoleCount == 0;
        if (m_IsCreateRole)
        {
            //新建角色
            CloneCreateRoleModel();
            CurrentJobId = 1;
        }
        else
        {
            //选择角色
            m_RoleList = resultProto.RoleList;
            m_SceneSelectRoleView.SetRoleList(m_RoleList, SelectRoleCallback);
            SelectRoleCallback(m_RoleList[0].RoleId);
        }
        m_SceneSelectRoleView.SetUICreateRoleShow(m_IsCreateRole);
        SetCreateRoleSceneModelShow(m_IsCreateRole);

        //CloneCreateRole();
        //CurrentJobId = 1;
        //m_SceneSelectRoleView.SetUICreateRoleShow(true);
        //SetCreateRoleSceneModelShow(true);
    }

    private void SelectRoleCallback(int obj)
    {
        if (obj == m_CurrSelectRoleId) return;
        SetSelectRoleModel(obj);
        SetSelectRoleUI(obj);
    }

    private void CloneCreateRoleModel()
    {
        if (CreateRoleContainers == null || CreateRoleContainers.Length == 0) return;

        for (int i = 0; i < m_Jobist.Count; i++)
        {
            GameObject objRole = RoleMgr.Instance.LoadPlayer(m_Jobist[i].Id);
            objRole.transform.parent = CreateRoleContainers[i];

            objRole.transform.localScale = Vector3.one;
            objRole.transform.localPosition = Vector3.zero;
            objRole.transform.localRotation = Quaternion.Euler(Vector3.zero);

            RoleCtrl roleCtrl = objRole.GetComponent<RoleCtrl>();
            if (roleCtrl != null)
            {
                m_JobRoleCtrl[m_Jobist[i].Id] = roleCtrl;
            }
        }
    }

    [SerializeField]
    private Transform DragTarget;

    private float m_RotateAngle = 90;

    private float m_TargetAngle = 0;

    private bool m_IsRotating = false;

    private float m_RotateSpeed = 200f;

    private void OnSelectRoleDrag(int obj)
    {
        if (m_IsRotating) return;
        m_IsRotating = true;
        float rAngle = m_RotateAngle * obj;
        m_TargetAngle = DragTarget.eulerAngles.y + rAngle;

        if(obj == -1)
        {
            CurrentJobId++;
            if(CurrentJobId > 4)
            {
                CurrentJobId = 1;
            }
        }
        else
        {
            CurrentJobId--;
            if(CurrentJobId < 1)
            {
                CurrentJobId = 4;
            }
        }
    }

    private void Update()
    {
        if (m_IsRotating)
        {
            float toAngle = Mathf.MoveTowardsAngle(DragTarget.eulerAngles.y, m_TargetAngle, Time.deltaTime * m_RotateSpeed);
            DragTarget.eulerAngles = Vector3.up * toAngle;
            if (Mathf.Abs(m_TargetAngle - toAngle) <= 0.01f)
            {
                m_IsRotating = false;
                DragTarget.eulerAngles = Vector3.up * m_TargetAngle;
            }
        }
    }

    private void OnSelectJob(int arg1, int arg2)
    {
        if (m_IsRotating) return;
        m_IsRotating = true;
        m_TargetAngle = arg2;
        CurrentJobId = arg1;
    }

    private void SetSelectJob()
    {
        for (int i = 0; i < m_Jobist.Count; i++)
        {
            if(m_Jobist[i].Id == CurrentJobId)
            {
                m_SceneSelectRoleView.SelectRoleJobDescView.SetUI(m_Jobist[i].Name, m_Jobist[i].Desc);
                break;
            }
        }

        if(m_SceneSelectRoleView.JobItems != null)
        {
            for (int i = 0; i < m_SceneSelectRoleView.JobItems.Length; i++)
            {
                m_SceneSelectRoleView.JobItems[i].SetSelected(CurrentJobId);
            }
        }
    }

    public void SetCreateRoleSceneModelShow(bool isShow)
    {
        if(CreateRoleSceneModel != null)
        {
            for (int i = 0; i < CreateRoleSceneModel.Length; i++)
            {
                CreateRoleSceneModel[i].gameObject.SetActive(isShow);
            }
        }
    }

    public void SetSelectRoleModel(int roleId)
    {
        if (m_CurrSelectRoleId == roleId) return;

        RoleOperation_LogOnGameServerReturnProto.RoleItem item =  GetRoleItem(roleId);

        if (CreateRoleContainers == null || CreateRoleContainers.Length == 0) return;

        m_CurrSelectRoleId = roleId;
        if (m_CurrSelectRoleModel != null)
        {
            Destroy(m_CurrSelectRoleModel);
        }

        m_CurrSelectRoleModel = RoleMgr.Instance.LoadPlayer(item.RoleJob);
        m_CurrSelectRoleModel.transform.parent = CreateRoleContainers[0];

        m_CurrSelectRoleModel.transform.localScale = Vector3.one;
        m_CurrSelectRoleModel.transform.localPosition = Vector3.zero;
        m_CurrSelectRoleModel.transform.localRotation = Quaternion.Euler(Vector3.zero);

        RoleCtrl roleCtrl = m_CurrSelectRoleModel.GetComponent<RoleCtrl>();
    }

    private void SetSelectRoleUI(int obj)
    {
        if(m_SceneSelectRoleView.m_RoleItemViewList != null)
        {
            for (int i = 0; i < m_SceneSelectRoleView.m_RoleItemViewList.Count; i++)
            {
                m_SceneSelectRoleView.m_RoleItemViewList[i].SetSelected(obj);
            }
        }
    }

    private RoleOperation_LogOnGameServerReturnProto.RoleItem GetRoleItem(int roleId)
    {
        if(m_RoleList != null)
        {
            for (int i = 0; i < m_RoleList.Count; i++)
            {
                if(m_RoleList[i].RoleId == roleId)
                {
                    return m_RoleList[i];
                }
            }
        }
        return default(RoleOperation_LogOnGameServerReturnProto.RoleItem);
    }

    private void OnSelectRoleInfoReturn(byte[] p)
    {
        RoleOperation_SelectRoleInfoReturnProto proto = RoleOperation_SelectRoleInfoReturnProto.GetProto(p);
        if (proto.IsSuccess)
        {
            GlobalInit.Instance.MainPlayerInfo = new RoleInfoMainPlayer(proto);
        }
        else
        {
            MessageCtrl.Instance.Show("提示", "获取角色信息失败");
        }        
    }
}
