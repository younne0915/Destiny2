using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelCtrl : SystemCtrlBase<GameLevelCtrl>, ISystemCtrl
{
    private UIGameLevelMapView m_UIGameLevelMapView;

    private UIGameLevelDetailView m_UIGameLevelDetailView;

    private UIGameLevelVictoryView m_UIGameLevelVictoryView;

    private UIGameLevelFailView m_UIGameLevelFailView;


    public void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.GameLevelMap:
                OpenGameLevelMapView();
                break;
            case WindowUIType.GameLevelVictory:
                OpenGameLevelVictory();
                break;
            case WindowUIType.GameLevelFail:
                OpenGameLevelFail();
                break;
        }
    }

    private void OpenGameLevelFail()
    {
        m_UIGameLevelFailView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameLevelFail).GetComponent<UIGameLevelFailView>();
        m_UIGameLevelFailView.OnResurgence = () => 
        {
            m_UIGameLevelFailView.Close();
            if (GlobalInit.Instance.CurrPlayer != null)
            {
                GlobalInit.Instance.CurrPlayer.ToResurgence(RoleIdleState.IdleNormal);
            }
        };
    }

    private void OpenGameLevelVictory()
    {
        m_UIGameLevelVictoryView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameLevelVictory).GetComponent<UIGameLevelVictoryView>();
    }

    private void OpenGameLevelMapView()
    {
        m_UIGameLevelMapView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameLevelMap).GetComponent<UIGameLevelMapView>();

        TransferData data = new TransferData();
        ChapterEntity entity = ChapterDBModel.Instance.Get(1);
        data.SetValue(ConstDefine.ChapterId, entity.Id);
        data.SetValue(ConstDefine.ChapterName, entity.ChapterName);
        data.SetValue(ConstDefine.ChapterBG, entity.BG_Pic);

        //¹Ø¿¨
        List<GameLevelEntity> gameLevelList = GameLevelDBModel.Instance.GetListByChapterId(entity.Id);
        List<TransferData> list = new List<TransferData>();

        GameLevelEntity gameLevelEntity;
        TransferData levelData;
        for (int i = 0; i < gameLevelList.Count; i++)
        {
            gameLevelEntity = gameLevelList[i];
            levelData = new TransferData();
            levelData.SetValue(ConstDefine.GameLevelId, gameLevelEntity.Id);
            levelData.SetValue(ConstDefine.GameLevelName, gameLevelEntity.Name);
            levelData.SetValue(ConstDefine.GameLevelPostion, gameLevelEntity.Position);
            levelData.SetValue(ConstDefine.GameLevelisBoss, gameLevelEntity.isBoss);
            levelData.SetValue(ConstDefine.GameLevelIco, gameLevelEntity.Ico);
            list.Add(levelData);
        }
        data.SetValue(ConstDefine.GameLevelList, list);
        m_UIGameLevelMapView.SetUI(data, OnGameLevelItemClick);
    }

    private void OnGameLevelItemClick(int obj)
    {
        OpenGameLevelDetail();
        SetGameLevelDetailData(obj, GameLevelGrade.Normal);
    }

    private void OpenGameLevelDetail()
    {
        m_UIGameLevelDetailView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameLevelDetail).GetComponent<UIGameLevelDetailView>();
        m_UIGameLevelDetailView.OnBtnGradeChangeClick = OnBtnGradeChangeClick;
        m_UIGameLevelDetailView.OnBtnEnterGameLevelClick = OnEnterGameLevelClick;
    }

    private void OnEnterGameLevelClick(int gameLevelId, GameLevelGrade grade)
    {
        SceneMgr.Instance.LoadToGameLevel(gameLevelId, grade);
    }

    private void OnBtnGradeChangeClick(int gameLevelId, GameLevelGrade grade)
    {
        SetGameLevelDetailData(gameLevelId, grade);
    }

    private void SetGameLevelDetailData(int gameLevelId, GameLevelGrade grade)
    {
        GameLevelEntity gameLevelEntity = GameLevelDBModel.Instance.Get(gameLevelId);
        GameLevelGradeEntity gameLevelGradeEntity = GameLevelGradeDBModel.Instance.GetEntityByGameLevelIdAndGrade(gameLevelId, grade);
        if (gameLevelEntity == null || gameLevelGradeEntity == null) return;

        TransferData data = new TransferData();
        data.SetValue(ConstDefine.GameLevelId, gameLevelEntity.Id);
        data.SetValue(ConstDefine.GameLevelDlgPic, gameLevelEntity.DlgPic);
        data.SetValue(ConstDefine.GameLevelName, gameLevelEntity.Name);
        data.SetValue(ConstDefine.GameLevelExp, gameLevelGradeEntity.Exp);
        data.SetValue(ConstDefine.GameLevelGold, gameLevelGradeEntity.Gold);
        data.SetValue(ConstDefine.GameLevelDesc, gameLevelGradeEntity.Desc);
        data.SetValue(ConstDefine.GameLevelConditionDesc, gameLevelGradeEntity.ConditionDesc);
        data.SetValue(ConstDefine.GameLevelCommendFighting, gameLevelGradeEntity.CommendFighting);

        m_UIGameLevelDetailView.SetUI(data);
    }
}
