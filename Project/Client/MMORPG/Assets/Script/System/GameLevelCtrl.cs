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

    private int m_GameLevelId;
    private GameLevelGrade m_Grade;

    public int CurrGameLevelTotalExp;
    public int CurrGameLevelTotalGold;

    public Dictionary<int, int> CurrGameLevelKillMonsterDic = new Dictionary<int, int>();
    public List<GetGoodsEntity> CurrGameLevelGetGoodsList = new List<GetGoodsEntity>();

    public GameLevelCtrl()
    {
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.GameLevel_EnterReturn, OnGameLevel_EnterReturnCallback);
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.GameLevel_ResurgenceReturn, OnGameLevel_ResurgenceReturnCallback);
    }

    private void OnGameLevel_ResurgenceReturnCallback(byte[] p)
    {
        GameLevel_ResurgenceReturnProto proto = GameLevel_ResurgenceReturnProto.GetProto(p);
        if (proto.IsSuccess)
        {
            m_UIGameLevelFailView.Close();
            if (GlobalInit.Instance.CurrPlayer != null)
            {
                GlobalInit.Instance.CurrPlayer.ToResurgence(RoleIdleState.IdleNormal);
            }
        }
        else
        {
            MessageCtrl.Instance.Show("∏¥ªÓ ß∞‹", "∏¥ªÓ ß∞‹«Î÷ÿ ‘!");
        }
    }

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
            GameLevel_ResurgenceProto resurgenceProto = new GameLevel_ResurgenceProto();
            resurgenceProto.GameLevelId = m_GameLevelId;
            resurgenceProto.Grade = ConvertUtil.ConvertToByte(m_Grade);
            resurgenceProto.Type = 0;
            NetWorkSocket.Instance.SendMsg(resurgenceProto.ToArray());
        };

        GameLevel_FailProto proto = new GameLevel_FailProto();
        proto.GameLevelId = m_GameLevelId;
        proto.Grade = ConvertUtil.ConvertToByte(m_Grade);
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    private void OpenGameLevelVictory()
    {
        m_UIGameLevelVictoryView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameLevelVictory).GetComponent<UIGameLevelVictoryView>();

        GameLevelEntity gameLevelEntity = GameLevelDBModel.Instance.Get(m_GameLevelId);
        GameLevelGradeEntity gameLevelGradeEntity = GameLevelGradeDBModel.Instance.GetEntityByGameLevelIdAndGrade(m_GameLevelId, m_Grade);
        if (gameLevelEntity == null || gameLevelGradeEntity == null) return;

        TransferData data = new TransferData();
        data.SetValue(ConstDefine.GameLevelGold, gameLevelGradeEntity.Gold);
        data.SetValue(ConstDefine.GameLevelDesc, gameLevelGradeEntity.Exp);


        CurrGameLevelTotalExp += gameLevelGradeEntity.Exp;
        CurrGameLevelTotalGold += gameLevelGradeEntity.Gold;
        GameLevel_VictoryProto proto = new GameLevel_VictoryProto();
        proto.GameLevelId = m_GameLevelId;
        proto.Grade = ConvertUtil.ConvertToByte(m_Grade);
        proto.Star = 3;
        proto.Exp = CurrGameLevelTotalExp;
        proto.Gold = CurrGameLevelTotalGold;
        proto.KillTotalMonsterCount = CurrGameLevelKillMonsterDic.Count;
        if (proto.KillTotalMonsterCount > 0)
        {
            proto.KillMonsterList = new List<GameLevel_VictoryProto.MonsterItem>();
            GameLevel_VictoryProto.MonsterItem monsterItem;
            foreach (var item in CurrGameLevelKillMonsterDic)
            {
                monsterItem = new GameLevel_VictoryProto.MonsterItem();
                monsterItem.MonsterCount = item.Value;
                monsterItem.MonsterId = item.Key;
                proto.KillMonsterList.Add(monsterItem);
            }
        }
        proto.GoodsTotalCount = CurrGameLevelGetGoodsList.Count;
        if(proto.GoodsTotalCount > 0)
        {
            proto.GetGoodsList = new List<GameLevel_VictoryProto.GoodsItem>();
            for (int i = 0; i < CurrGameLevelGetGoodsList.Count; i++)
            {
                GetGoodsEntity entity = CurrGameLevelGetGoodsList[i];
                GameLevel_VictoryProto.GoodsItem goodsItem = new GameLevel_VictoryProto.GoodsItem();
                goodsItem.GoodsCount = CurrGameLevelGetGoodsList[i].GoodsCount;
                goodsItem.GoodsId = CurrGameLevelGetGoodsList[i].GoodsId;
                goodsItem.GoodsType = CurrGameLevelGetGoodsList[i].GoodsType;
                proto.GetGoodsList.Add(goodsItem);
            }
        }
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    private void OpenGameLevelMapView()
    {
        m_UIGameLevelMapView = UIViewUtil.Instance.OpenWindow(WindowUIType.GameLevelMap).GetComponent<UIGameLevelMapView>();

        TransferData data = new TransferData();
        ChapterEntity entity = ChapterDBModel.Instance.Get(1);
        data.SetValue(ConstDefine.ChapterId, entity.Id);
        data.SetValue(ConstDefine.ChapterName, entity.ChapterName);
        data.SetValue(ConstDefine.ChapterBG, entity.BG_Pic);

        //πÿø®
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
        m_GameLevelId = gameLevelId;
        m_Grade = grade;
        GameLevel_EnterProto proto = new GameLevel_EnterProto();
        proto.GameLevelId = gameLevelId;
        proto.Grade = ConvertUtil.ConvertToByte(grade);
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
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

    private void OnGameLevel_EnterReturnCallback(byte[] p)
    {
        GameLevel_EnterReturnProto proto = GameLevel_EnterReturnProto.GetProto(p);
        if (proto.IsSuccess)
        {
            SceneMgr.Instance.LoadToGameLevel(m_GameLevelId, m_Grade);
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.GameLevel_EnterReturn, OnGameLevel_EnterReturnCallback);
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.GameLevel_ResurgenceReturn, OnGameLevel_ResurgenceReturnCallback);
    }
}
