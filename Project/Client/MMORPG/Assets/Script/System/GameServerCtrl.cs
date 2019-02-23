using UnityEngine;
using System.Collections;

public class GameServerCtrl : SystemCtrl<GameServerCtrl>, ISystemCtrl
{
    private UIGameServerEnterView uiGameServerEnterView;
    private UIGameServerSelectView uiGameServerSelectView;

    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.GameServerEnter:
                OpenGameServerEnterView();
                break;
            case WindowUIType.GameServerSelect:
                OpenGameServerSelectView();
                break;
        }
    }

    private void OpenGameServerEnterView()
    {
        uiGameServerEnterView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerEnter).GetComponent<UIGameServerEnterView>();
    }

    private void OpenGameServerSelectView()
    {
        uiGameServerSelectView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameServerSelect).GetComponent<UIGameServerSelectView>();
    }


}
