using UnityEngine;
using System.Collections;

public class UIGameLevelFailView : UIWindowViewBase
{
    /// <summary>
    /// ����ί��
    /// </summary>
    public System.Action OnResurgence;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);

        switch (go.name)
        {
            case "btnReturn":
                //��Ҹ���
                GlobalInit.Instance.CurrPlayer.ToResurgence(RoleIdleState.IdleNormal);
                SceneMgr.Instance.LoadToWorldMap(SceneMgr.Instance.CurrWorldMapId);
                break;
            case "btnResurgence":
                if (OnResurgence != null) OnResurgence();
                break;
        }
    }
}
