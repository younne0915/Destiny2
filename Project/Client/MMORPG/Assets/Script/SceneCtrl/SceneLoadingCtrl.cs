//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-05 11:13:45
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using System.IO;

public class SceneLoadingCtrl : MonoBehaviour 
{
    /// <summary>
    /// UI场景控制器
    /// </summary>
    [SerializeField]
    private UISceneLoadingCtrl m_UILoadingCtrl;

    private AsyncOperation m_Async = null;

    /// <summary>
    /// 当前的进度
    /// </summary>
    private int m_CurrProgress = 0;

	void Start ()
	{
        LoaderMgr.Instance.UnLoadAssetBundle();
        Resources.UnloadUnusedAssets();

        DelegateDefine.Instance.OnSceneLoadOK += OnSceneLoadOK;
        LayerUIMgr.Instance.Reset();
        StartCoroutine(LoadingScene());
        UIViewUtil.Instance.CloseAllWindow();
    }

    private void OnDestroy()
    {
        DelegateDefine.Instance.OnSceneLoadOK -= OnSceneLoadOK;
    }

    private void OnSceneLoadOK()
    {
        if(m_UILoadingCtrl != null)
        {
            Destroy(m_UILoadingCtrl.gameObject);
        }

        Destroy(gameObject);
    }

    private IEnumerator LoadingScene()
    {
        string strSceneName = string.Empty;
        switch (SceneMgr.Instance.CurrentSceneType)
        {
            case SceneType.LogOn:
                strSceneName = "Scene_LogOn";
                break;
            case SceneType.SelectRole:
                strSceneName = "Scene_SelectRole";
                break;
            case SceneType.WorldMap:
                WorldMapEntity entity = WorldMapDBModel.Instance.Get(SceneMgr.Instance.CurrWorldMapId);
                if(entity != null)
                {
                    strSceneName = entity.SceneName;
                }
                if (string.IsNullOrEmpty(strSceneName)) yield break;
                break;
            case SceneType.Shamo:
                strSceneName = "GameScene_Shamo";
                break;
            case SceneType.GameLevel:
                GameLevelEntity gameLevelEntity = GameLevelDBModel.Instance.Get(SceneMgr.Instance.CurrGameLevelId);
                if (gameLevelEntity != null)
                {
                    strSceneName = gameLevelEntity.SceneName;
                }
                if (string.IsNullOrEmpty(strSceneName)) yield break;
                break;
            default:
                AppDebug.LogError("SceneType Error");
                yield break;
        }

        RecyclePoolMgr.Instance.Clear();

        if (SceneMgr.Instance.CurrentSceneType == SceneType.SelectRole || SceneMgr.Instance.CurrentSceneType == SceneType.WorldMap || SceneMgr.Instance.CurrentSceneType == SceneType.GameLevel)
        {
#if DISABLE_ASSETBUNDLE
            yield return null;
            strSceneName = string.Format("Download/Scene/{0}", strSceneName);
            m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
            m_Async.allowSceneActivation = false;
#else
            string scenePath = string.Format("Download/Scene/{0}.unity3d", strSceneName);
            string fullPath = LocalFileMgr.Instance.LocalFilePath + scenePath;
            if (!File.Exists(fullPath))
            {
                DownloadDataEntity downloadDataEntity = DownloadMgr.Instance.GetServerData(scenePath);
                AssetBundleDownload.Instance.DownloadData(downloadDataEntity, (bool isSucess)=> 
                {
                    AppDebug.LogError("下载结果 ： " + isSucess);
                    if (isSucess)
                    {
                        AssetBundleLoaderAsync assetBundleAsync = AssetBundleMgr.Instance.LoadAsync(scenePath, strSceneName);
                        assetBundleAsync.OnLoadCompleteNoObject = () =>
                        {
                            m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
                            m_Async.allowSceneActivation = false;
                        };
                    }
                });
            }
            else
            {
                AssetBundleLoaderAsync assetBundleAsync = AssetBundleMgr.Instance.LoadAsync(scenePath, strSceneName);
                assetBundleAsync.OnLoadCompleteNoObject = () =>
                {
                    m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
                    m_Async.allowSceneActivation = false;
                };
            }
#endif
        }
        else
        {
            m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
            m_Async.allowSceneActivation = false;
            yield return m_Async;
        }
    }

	void Update ()
	{
        if (m_Async == null) return;
        int toProgress = 0;

        if (m_Async.progress < 0.9f)
        {
            toProgress = Mathf.Clamp((int)m_Async.progress * 100, 1, 100);
        }
        else
        {
            toProgress = 100;
        }

        if (m_CurrProgress < toProgress)
        {
            m_CurrProgress++;
        }
        else
        {
            m_Async.allowSceneActivation = true;
        }

        m_UILoadingCtrl.SetProgressValue(m_CurrProgress * 0.01f);
    }
}