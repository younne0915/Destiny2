using UnityEngine;
using System.Collections;

public class AssetBundleLoader : System.IDisposable
{
    private AssetBundle bundle;

    public AssetBundleLoader(string assetBundlePath)
    {
        string fullPath = LocalFileMgr.Instance.LocalFilePath + assetBundlePath;
        byte[] buffer = LocalFileMgr.Instance.GetBuffer(fullPath);
        if(buffer != null)
        {
            bundle = AssetBundle.CreateFromMemoryImmediate(buffer);
        }
        else
        {
            Debug.LogError(string.Format("buffer is null assetBundlePath : ", assetBundlePath));
        }
    }

    public T LoadAsset<T>(string name) where T : Object
    {
        if (bundle == null) return default(T);
        return bundle.LoadAsset(name) as T;
    }

    public void Dispose()
    {
        if (bundle != null) bundle.Unload(false);
    }
}
