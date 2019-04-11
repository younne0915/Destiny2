using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorldMapFailView : UIWindowViewBase
{
    [SerializeField]
    private Text AttackNameText;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);

        switch (go.name)
        {
            case "btnReturn":
                //Íæ¼Ò¸´»î
                GlobalInit.Instance.CurrPlayer.ToResurgence(RoleIdleState.IdleNormal);
                SceneMgr.Instance.LoadToWorldMap(2);
                break;
            case "btnResurgence":
                EventDispatcher.Instance.Dispatch(ConstDefine.PlayerResurgenceEvent);
                Close();
                break;
        }
    }

    public void SetUI(TransferData transferData)
    {
        string str = string.Format(AttackNameText.text, transferData.GetValue<string>(ConstDefine.WorldMapName));
        AttackNameText.SetText(str);
    }
}
