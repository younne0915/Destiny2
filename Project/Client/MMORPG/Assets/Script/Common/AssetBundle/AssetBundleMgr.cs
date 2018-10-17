using UnityEngine;
using System.Collections;

public class AssetBundleMgr : Singleton<AssetBundleMgr>
{
	public GameObject Load(string path, string name)
    {
        using (AssetBundleLoader loader = new AssetBundleLoader(path))
        {
            return loader.LoadAsset<GameObject>(name);
        }
    }

    public GameObject LoadClone(string path, string name)
    {
        using (AssetBundleLoader loader = new AssetBundleLoader(path))
        {
            GameObject obj = loader.LoadAsset<GameObject>(name);
            if (obj != null)
                return Object.Instantiate(obj);
            else
            {
                Debug.LogErrorFormat("LoadClone error path : {0}, name : {1}", path, name);
                return null;
            }
        }
    }
}
