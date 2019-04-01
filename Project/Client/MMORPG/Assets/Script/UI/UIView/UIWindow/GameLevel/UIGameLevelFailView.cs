using UnityEngine;
using System.Collections;

public class UIGameLevelFailView : UIWindowViewBase
{
    /// <summary>
    /// 复活委托
    /// </summary>
    public System.Action OnResurgence;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);

        switch (go.name)
        {
            case "btnReturn":
                //玩家复活
                GlobalInit.Instance.CurrPlayer.ToResurgence(RoleIdleState.IdleNormal);
                SceneMgr.Instance.LoadToWorldMap(SceneMgr.Instance.CurrWorldMapId);
                break;
            case "btnResurgence":
                if (OnResurgence != null) OnResurgence();
                break;
        }
    }
}
