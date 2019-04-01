using UnityEngine;
using System.Collections;

public class AssetBundleLoaderAsync : MonoBehaviour
{
    private string m_FullPath;
    private string m_Name;

    private AssetBundleCreateRequest request;
    private AssetBundle bundle;

    public System.Action<Object> OnLoadComplete;

    public System.Action OnLoadCompleteNoObject;

    public void Init(string path, string name)
    {
        m_FullPath = LocalFileMgr.Instance.LocalFilePath + path;
        m_Name = name;
    }

    void Start()
    {
        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        request = AssetBundle.LoadFromMemoryAsync(LocalFileMgr.Instance.GetBuffer(m_FullPath));
        yield return request;
        bundle = request.assetBundle;
        if(OnLoadCompleteNoObject != null)
        {
            OnLoadCompleteNoObject();
        }

        if(OnLoadComplete != null)
        {
            OnLoadComplete(bundle.LoadAsset(m_Name));
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (bundle != null)
        {
            bundle.Unload(false);
            bundle = null;
        }
        m_FullPath = null;
        m_Name = null;
        request = null;
    }
}
