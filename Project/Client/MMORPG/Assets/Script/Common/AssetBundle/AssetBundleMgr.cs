using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class AssetBundleMgr : Singleton<AssetBundleMgr>
{
    private Dictionary<string, UnityEngine.Object> m_AssetDic = new Dictionary<string, UnityEngine.Object>();
    private Dictionary<string, AssetBundleLoader> m_AssetBundleDic = new Dictionary<string, AssetBundleLoader>();

    private AssetBundleManifest m_AssetBundleManifest;
    public AssetBundleManifest assetBundleManifest
    {
        get
        {
            if (m_AssetBundleManifest == null)
            {
#if UNITY_EDITOR
                string target = "Windows";
#elif UNITY_ANDROID
    string target = "Android";
#elif UNITY_IPHONE
    string target = "iOS";
#else
string target = "Windows";
#endif
                m_AssetBundleManifest = Load<AssetBundleManifest>(target, "AssetBundleManifest");
            }
            return m_AssetBundleManifest;
        }
    }

    public T Load<T>(string path, string name) where T : UnityEngine.Object
    {
        AppDebug.Log("Load : path = " + path);

        string fullPath = LocalFileMgr.Instance.LocalFilePath + path;

        if (m_AssetDic.ContainsKey(fullPath.ToLower()))
        {
            return m_AssetDic[fullPath.ToLower()] as T;
        }
        else
        {
            using (AssetBundleLoader loader = new AssetBundleLoader(path))
            {
                T obj = loader.LoadAsset<T>(name);
                m_AssetDic.Add(fullPath.ToLower(), obj);
                return obj;
            }
        }
    }

    public GameObject LoadClone(string path, string name)
    {
        using (AssetBundleLoader loader = new AssetBundleLoader(path))
        {
            GameObject obj = loader.LoadAsset<GameObject>(name);
            if (obj != null)
                return UnityEngine.Object.Instantiate(obj);
            else
            {
                Debug.LogErrorFormat("LoadClone error path : {0}, name : {1}", path, name);
                return null;
            }
        }
    }

    /// <summary>
    /// 检查依赖项
    /// </summary>
    /// <param name="index"></param>
    /// <param name="arrDps"></param>
    /// <param name="onComplete"></param>
    private void CheckDps(int index, string[] arrDps, System.Action onComplete)
    {
        lock (this)
        {
            if (arrDps == null || arrDps.Length == 0)
            {
                if (onComplete != null) onComplete();
                return;
            }

            string fullPath = LocalFileMgr.Instance.LocalFilePath + arrDps[index];

            if (!File.Exists(fullPath))
            {
                //如果文件不存在 需要下载
                DownloadDataEntity entity = DownloadMgr.Instance.GetServerData(arrDps[index]);

                if (entity != null)
                {
                    AssetBundleDownload.Instance.DownloadData(entity,
                        (bool isSuccess) =>
                        {
                            index++;
                            if (index == arrDps.Length)
                            {
                                if (onComplete != null) onComplete();
                                return;
                            }

                            CheckDps(index, arrDps, onComplete);
                        });
                }
            }
            else
            {
                index++;
                if (index == arrDps.Length)
                {
                    if (onComplete != null) onComplete();
                    return;
                }

                CheckDps(index, arrDps, onComplete);
            }
        }
    }


    //public void LoadOrDownload<T>(string path, string name, System.Action<T> onComplete) where T : UnityEngine.Object
    //{
    //    #region 依赖项
    //    string[] desArr = assetBundleManifest.GetAllDependencies(path);
    //    DownLoadDependency(0, desArr, () =>
    //    {
    //        #region 主资源
    //        AppDebug.LogError("加载主资源");
    //        string fullPath = LocalFileMgr.Instance.LocalFilePath + path;
    //        if (!File.Exists(fullPath))
    //        {
    //            //下载
    //            DownloadDataEntity downloadDataEntity = DownloadMgr.Instance.GetServerData(path);
    //            AssetBundleDownload.Instance.DownloadData(downloadDataEntity, (bool isSucess) =>
    //            {
    //                AppDebug.LogError("下载结果 : " + isSucess);
    //                if (isSucess)
    //                {
    //                    //加载依赖项
    //                    if (desArr != null)
    //                    {
    //                        for (int i = 0; i < desArr.Length; i++)
    //                        {
    //                            if (!m_AssetDic.ContainsKey(desArr[i]))
    //                            {
    //                                AssetBundleLoader loader = new AssetBundleLoader(desArr[i]);
    //                                m_AssetDic.Add(desArr[i], loader.LoadAsset<UnityEngine.Object>(GameUtil.GetFileName(desArr[i])));
    //                                m_LoaderDic.Add(desArr[i], loader);
    //                            }
    //                        }
    //                    }

    //                    if (!m_AssetDic.ContainsKey(path))
    //                    {
    //                        using (AssetBundleLoader loader = new AssetBundleLoader(path))
    //                        {
    //                            onComplete(loader.LoadAsset<T>(name));
    //                        }
    //                    }
    //                    else
    //                    {
    //                        onComplete(m_AssetDic[path] as T);
    //                    }
    //                }
    //                else
    //                {
    //                    if (onComplete != null)
    //                    {
    //                        onComplete(default(T));
    //                    }
    //                }
    //            });
    //        }
    //        else
    //        {
    //            //加载依赖项
    //            if (desArr != null)
    //            {
    //                for (int i = 0; i < desArr.Length; i++)
    //                {
    //                    if (!m_AssetDic.ContainsKey(desArr[i]))
    //                    {
    //                        AssetBundleLoader loader = new AssetBundleLoader(desArr[i]);
    //                        m_AssetDic.Add(desArr[i], loader.LoadAsset<UnityEngine.Object>(GameUtil.GetFileName(desArr[i])));
    //                        m_LoaderDic.Add(desArr[i], loader);
    //                    }
    //                }
    //            }

    //            if (!m_AssetDic.ContainsKey(path))
    //            {
    //                using (AssetBundleLoader loader = new AssetBundleLoader(path))
    //                {
    //                    onComplete(loader.LoadAsset<T>(name));
    //                }
    //            }
    //            else
    //            {
    //                onComplete(m_AssetDic[path] as T);
    //            }
    //        }
    //        #endregion
    //    });
    //    #endregion
    //}

    public void LoadOrDownload<T>(string path, string name, System.Action<T> onComplete, xLuaCustomExport.OnCreate onCreate = null) where T : UnityEngine.Object
    {
        lock (this)
        {
            //2.加载依赖项开始
            string[] arrDps = assetBundleManifest.GetAllDependencies(path);
            AppDebug.Log("path = " + path + arrDps.Length);
            //先检查所有依赖项 是否已经下载 没下载的就下载
            CheckDps(0, arrDps, () =>
            {
                //=============下载主资源开始===================
                string fullPath = (LocalFileMgr.Instance.LocalFilePath + path).ToLower();

                AppDebug.Log("fullPath=" + fullPath);

                #region 下载或者加载主资源
                if (!File.Exists(fullPath))
                {
                    #region 如果文件不存在 需要下载
                    DownloadDataEntity entity = DownloadMgr.Instance.GetServerData(path);
                    if (entity != null)
                    {
                        AssetBundleDownload.Instance.DownloadData(entity,
                            (bool isSuccess) =>
                            {
                                if (isSuccess)
                                {
                                    if (m_AssetDic.ContainsKey(fullPath))
                                    {
                                        if (onComplete != null)
                                        {
                                            onComplete(m_AssetDic[fullPath] as T);
                                        }
                                        if(onCreate != null)
                                        {
                                            onCreate(m_AssetDic[fullPath] as GameObject);
                                        }
                                        return;
                                    }

                                    for (int i = 0; i < arrDps.Length; i++)
                                    {
                                        if (!m_AssetDic.ContainsKey((LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()))
                                        {
                                            AssetBundleLoader loader = new AssetBundleLoader(arrDps[i]);
                                            Object obj = loader.LoadAsset<Object>(GameUtil.GetFileName(arrDps[i]));
                                            m_AssetBundleDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = loader;
                                            m_AssetDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = obj;
                                        }
                                    }

                                    //直接加载
                                    using (AssetBundleLoader loader = new AssetBundleLoader(path))
                                    {
                                        Object obj = loader.LoadAsset<T>(name);
                                        m_AssetDic[fullPath] = obj;
                                        if (onComplete != null)
                                        {
                                            //进行回调
                                            onComplete(obj as T);
                                        }

                                        //todu 进行xlua的回调
                                        if (onCreate != null)
                                        {
                                            onCreate(obj as GameObject);
                                        }
                                    }
                                }
                            });
                    }
                    #endregion
                }
                else
                {
                    if (m_AssetDic.ContainsKey(fullPath))
                    {
                        if (onComplete != null)
                        {
                            onComplete(m_AssetDic[fullPath] as T);
                        }
                        if (onCreate != null)
                        {
                            onCreate(m_AssetDic[fullPath] as GameObject);
                        }
                        return;
                    }

                    //===================================
                    for (int i = 0; i < arrDps.Length; i++)
                    {
                        if (!m_AssetDic.ContainsKey((LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()))
                        {
                            AssetBundleLoader loader = new AssetBundleLoader(arrDps[i]);
                            Object obj = loader.LoadAsset<Object>(GameUtil.GetFileName(arrDps[i]));
                            m_AssetBundleDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = loader;
                            m_AssetDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = obj;
                        }
                    }
                    //===================================
                    //直接加载
                    using (AssetBundleLoader loader = new AssetBundleLoader(path))
                    {
                        Object obj = loader.LoadAsset<T>(name);
                        if (obj == null)
                        {
                            AppDebug.LogError(string.Format("Obj =  null : path :{0}, name : {1} type : {2}", path, name, typeof(T)));
                        }
                        m_AssetDic[fullPath] = obj;

                        if (onComplete != null)
                        {
                            //进行回调
                            onComplete(obj as T);
                        }

                        //todu 进行xlua的回调
                        if(onCreate != null)
                        {
                            onCreate(obj as GameObject);
                        }
                    }
                }
                #endregion

                //=============下载主资源结束===================
            });
        }
    }

    public void LoadOrDownloadForLua(string path, string name, xLuaCustomExport.OnCreate onCreate)
    {
        LoadOrDownload<GameObject>(path, name, null, onCreate);
    }

    public void LoadOrDownload(string path, string name, System.Action<GameObject> onComplete)
    {
        LoadOrDownload<GameObject>(path, name, onComplete);
    }

    public AssetBundleLoaderAsync LoadAsync(string path, string name)
    {
        GameObject obj = new GameObject("AssetBundleLoaderAsync");
        AssetBundleLoaderAsync async = obj.GetOrCreatComponent<AssetBundleLoaderAsync>();
        async.Init(path, name);
        return async;
    }


    public void UnLoadAssetBundle()
    {
        foreach (var item in m_AssetBundleDic.Values)
        {
            item.Dispose();
        }

        m_AssetBundleDic.Clear();
        m_AssetDic.Clear();
    }
}
