using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIViewMgr : Singleton<UIViewMgr>
{
    private Dictionary<WindowUIType, ISystemCtrl> m_systemCtrlDic = new Dictionary<WindowUIType, ISystemCtrl>();

    public UIViewMgr()
    {
        m_systemCtrlDic.Add(WindowUIType.LogOn, AccountCtrl.Instance);
        m_systemCtrlDic.Add(WindowUIType.Reg, AccountCtrl.Instance);

        m_systemCtrlDic.Add(WindowUIType.GameServerEnter, GameServerCtrl.Instance);
        m_systemCtrlDic.Add(WindowUIType.GameServerSelect, GameServerCtrl.Instance);

        m_systemCtrlDic.Add(WindowUIType.RoleInfo, PlayerCtrl.Instance);

        m_systemCtrlDic.Add(WindowUIType.GameLevelMap, GameLevelCtrl.Instance);
        m_systemCtrlDic.Add(WindowUIType.GameLevelDetail, GameLevelCtrl.Instance);
        m_systemCtrlDic.Add(WindowUIType.GameLevelVictory, GameLevelCtrl.Instance);
        m_systemCtrlDic.Add(WindowUIType.GameLevelFail, GameLevelCtrl.Instance);
    }

    public void OpenWindow(WindowUIType type)
    {
        if (m_systemCtrlDic.ContainsKey(type))
        {
            m_systemCtrlDic[type].OpenView(type);
        }
        else
        {
            Debug.LogErrorFormat("dont contains WindowUIType : {0}", type);
        }
    }
}
