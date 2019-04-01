using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

    public int CurrWorldMapId
    {
        get;
        private set;
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

    /// <summary>
    /// 去世界地图场景（主程+野外场景）
    /// </summary>
    public void LoadToWorldMap(int worldMapId)
    {
        CurrWorldMapId = worldMapId;
        CurrentSceneType = SceneType.WorldMap;
        SceneManager.LoadScene("Scene_Loading");
    }

    public void LoadToShamo()
    {
        CurrentSceneType = SceneType.Shamo;
        SceneManager.LoadScene("Scene_Loading");
    }

    public void LoadToGameLevel(int gamelevelId, GameLevelGrade grade)
    {
        CurrGameLevelId = gamelevelId;
        CurrGameLevelGrade = grade;
        CurrentSceneType = SceneType.GameLevel;
        SceneManager.LoadScene("Scene_Loading");
    }
}