using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneMainCityView : UISceneViewBase
{
    [SerializeField]
    private GameObject fightObj;

    [SerializeField]
    private GameObject autoFightObj;

    [SerializeField]
    private GameObject cancleFightObj;

    public Action<int> OnSkillBtnClick;

    protected override void OnStart()
    {
        base.OnStart();

        bool isGameLevel = SceneMgr.Instance.CurrentSceneType == SceneType.GameLevel;
        fightObj.SetActive(isGameLevel);
    }

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        switch (go.name)
        {
            case "btnTopMenu":
                ChangeMenuState(go);
                break;
            case "btnMenu_Role":
                UIViewMgr.Instance.OpenWindow(WindowUIType.RoleInfo);
                break;
            case "btnMenu_GameLevel":
                UIViewMgr.Instance.OpenWindow(WindowUIType.GameLevelMap);
                break;
            case "BtnSkill1":
                OnSkillRelease(go);
                break;
            case "BtnSkill2":
                OnSkillRelease(go);
                break;
            case "BtnSkill3":
                OnSkillRelease(go);
                break;
            case "btnAutoFight":
                SetAutoFightVisible(false);
                break;
            case "btnCancelAutoFight":
                SetAutoFightVisible(true);
                break;
        }
    }

    private void SetAutoFightVisible(bool isAutoFightVisible)
    {
        autoFightObj.SetActive(isAutoFightVisible);
        cancleFightObj.SetActive(!isAutoFightVisible);

        GlobalInit.Instance.CurrPlayer.IsAutoFight = !isAutoFightVisible;
    }

    private void OnSkillRelease(GameObject obj)
    {
        UIMainCitySkillSlotsView skillSlotsView = obj.GetComponent<UIMainCitySkillSlotsView>();

        if (OnSkillBtnClick != null && skillSlotsView != null && !skillSlotsView.IsCD)
        {
            OnSkillBtnClick(skillSlotsView.skillId);
        }
    }

    private void ChangeMenuState(GameObject go)
    {
        UIMainCityMenusView.Instance.ChangeState(()=>
        {
            go.transform.localScale = new Vector3(go.transform.localScale.x, go.transform.localScale.y*-1, go.transform.localScale.z);
        });
    }


}
