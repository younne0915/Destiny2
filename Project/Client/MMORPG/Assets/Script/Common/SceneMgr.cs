using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 当前场景类型
    /// </summary>
    public SceneType CurrentSceneType
    {
        get;
        private set;
    }

    public int CurrWorldMapId
    {
        get;
        set;
    }

    public int CurrGameLevelId
    {
        get;
        private set;
    }
    public GameLevelGrade CurrGameLevelGrade
    {
        get;
        private set;
    }

    public PlayType CurrPlayType
    {
        get;
        set;
    }

    public int TargetWorldMapTransPosId = 0;

    private int m_WillToSceneId = -1;

    public SceneMgr()
    {
        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.WorldMap_RoleEnterReturn, OnWorldMap_RoleEnterReturn);
    }


    public void LoadToLogOn()
    {
        CurrentSceneType = SceneType.LogOn;
        SceneManager.LoadScene("Scene_Loading");
    }

    public void LoadToSelectRole()
    {
        CurrentSceneType = SceneType.SelectRole;
        SceneManager.LoadScene("Scene_Loading");
    }

    private void OnWorldMap_RoleEnterReturn(byte[] p)
    {
        WorldMap_RoleEnterReturnProto proto = WorldMap_RoleEnterReturnProto.GetProto(p);
        if (proto.IsSuccess)
        {
            CurrPlayType = PlayType.PVP;
            CurrWorldMapId = m_WillToSceneId;
            CurrentSceneType = SceneType.WorldMap;
            SceneManager.LoadScene("Scene_Loading");
        }
    }

    /// <summary>
    /// 去世界地图场景（主程+野外场景）
    /// </summary>
    public void LoadToWorldMap(int worldMapId)
    {
        if (CurrWorldMapId == worldMapId) return;
        m_WillToSceneId = worldMapId;
        WorldMap_RoleEnterProto proto = new WorldMap_RoleEnterProto();
        proto.WorldMapSceneId = worldMapId;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    public void LoadToShamo()
    {
        CurrentSceneType = SceneType.Shamo;
        SceneManager.LoadScene("Scene_Loading");
    }

    public void LoadToGameLevel(int gamelevelId, GameLevelGrade grade)
    {
        CurrPlayType = PlayType.PVE;
        CurrGameLevelId = gamelevelId;
        CurrGameLevelGrade = grade;
        CurrentSceneType = SceneType.GameLevel;
        SceneManager.LoadScene("Scene_Loading");
    }

    public override void Dispose()
    {
        base.Dispose();
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.WorldMap_RoleEnterReturn, OnWorldMap_RoleEnterReturn);
    }
}