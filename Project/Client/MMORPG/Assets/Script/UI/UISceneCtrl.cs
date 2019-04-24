//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-29 16:24:29
//备    注：场景UI管理器
//===================================================
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 场景UI管理器
/// </summary>
public class UISceneCtrl: Singleton<UISceneCtrl>
{

    /// <summary>
    /// 场景UI类型
    /// </summary>
    public enum SceneUIType
    {
        /// <summary>
        /// 登录
        /// </summary>
        LogOn,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        SelectRole,
        /// <summary>
        /// 主城
        /// </summary>
        MainCity
    }

    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneViewBase CurrentUIScene;

    #region LoadSceneUI 加载场景UI
    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public void LoadSceneUI(SceneUIType type, Action<GameObject> OnLoadComplete)
    {
        string prefabName = string.Empty;
        switch (type)
        {
            case SceneUIType.LogOn:
                prefabName = "UI_Root_LogOn";
                break;
            case SceneUIType.SelectRole:
                prefabName = "UI_Root_SelectRole";
                break;
            case SceneUIType.Loading:
                break;
            case SceneUIType.MainCity:
                prefabName = "UI_Root_MainCity";
                break;
        }

        LoaderMgr.Instance.LoadOrDownload("Download/Prefab/UIPrefab/UIScene/" + prefabName, prefabName, (GameObject obj)=> 
        {
            if(obj != null)
            {
                obj = UnityEngine.Object.Instantiate(obj);
                CurrentUIScene = obj.GetComponent<UISceneViewBase>();
                if (OnLoadComplete != null)
                {
                    OnLoadComplete(obj);
                }
            }
        });
    }
    #endregion
}