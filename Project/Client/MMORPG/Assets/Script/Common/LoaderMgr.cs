using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoaderMgr : Singleton<LoaderMgr>
{
    public GameObject Load(string path, string name)
    {
#if DISABLE_ASSETBUNDLE
        return AssetDatabaseLoad<GameObject>(path, 0);
#else
        path = string.Format("{0}.assetbundle", path);
        return AssetBundleMgr.Instance.Load<GameObject>(path, name);
#endif
    }

    public void LoadOrDownload(string path, string name, Action<GameObject> onComplete)
    {
#if DISABLE_ASSETBUNDLE
        if (onComplete != null)
        {
            onComplete(AssetDatabaseLoad<GameObject>(path, 0));
        }
#else
        path = string.Format("{0}.assetbundle", path);
        AssetBundleMgr.Instance.LoadOrDownload(path, name, onComplete);
#endif
    }

    public void LoadOrDownload<T>(string path, string name, Action<T> onComplete, byte type) where T : UnityEngine.Object
    {
#if DISABLE_ASSETBUNDLE
        if (onComplete != null)
        {
            onComplete(AssetDatabaseLoad<T>(path, type));
        }
#else
        path = string.Format("{0}.assetbundle", path);
        AssetBundleMgr.Instance.LoadOrDownload<T>(path, name, onComplete);
#endif
    }

#if DISABLE_ASSETBUNDLE
    private T AssetDatabaseLoad<T>(string path, byte type) where T : UnityEngine.Object
    {
        switch (type)
        {
            case 0:
                path = string.Format("Assets/{0}.prefab", path);
                break;
            case 1:
                path = string.Format("Assets/{0}.png", path);
                break;
        }
        //AppDebug.LogError("path  = " + path);
        return UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
    }
#endif

    public void UnLoadAssetBundle()
    {
        AssetBundleMgr.Instance.UnLoadAssetBundle();
    }
}
