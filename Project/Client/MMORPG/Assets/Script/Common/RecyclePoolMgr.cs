using PathologicalGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclePoolMgr : SingletonMono<RecyclePoolMgr>
{
    private Dictionary<PoolType, SpawnPool> m_SpawnPoolDic = new Dictionary<PoolType, SpawnPool>();

    private Dictionary<string, GameObject> m_CacheResDic = new Dictionary<string, GameObject>();

    public Transform Spawn(PoolType poolType, ResourLoadType resourLoadType, string path)
    {
        if (!m_CacheResDic.ContainsKey(path))
        {
            if (resourLoadType == ResourLoadType.AssetBundle)
            {
                int index = path.LastIndexOf('/') + 1;
                string prefabName = path.Substring(index, path.Length - index);
                //AppDebug.Log(string.Format("path = {0}, index = {1}, prefabName = {2}", path, index, prefabName));
                m_CacheResDic[path] = AssetBundleMgr.Instance.Load(string.Format("{0}.assetbundle", path), prefabName);
            }
            else
            {
                m_CacheResDic[path] = Resources.Load(path) as GameObject;
                AppDebug.Log(string.Format("Resource path = {0}", path));
            }
        }

        return Spawn(poolType, m_CacheResDic[path].transform);
    }

    public Transform Spawn(PoolType poolType, Transform prefab)
    {
        SpawnPool spawnPool = null;
        if (m_SpawnPoolDic.ContainsKey(poolType))
        {
            spawnPool = m_SpawnPoolDic[poolType];
            if(spawnPool.GetPrefabPool(prefab) == null)
            {
                PrefabPool prefabPool = new PrefabPool(prefab);
                prefabPool.preloadAmount = 1;
                prefabPool.cullDespawned = true;
                prefabPool.cullAbove = 1;
                prefabPool.cullDelay = 2;
                prefabPool.cullMaxPerPass = 5;

                spawnPool.CreatePrefabPool(prefabPool);
            }
        }
        else
        {
            spawnPool = PoolManager.Pools.Create(poolType.ToString());
            spawnPool.group.parent = transform;
            spawnPool.group.localPosition = Vector3.zero;
            spawnPool.group.localScale = Vector3.one;

            PrefabPool prefabPool = new PrefabPool(prefab);
            prefabPool.preloadAmount = 1;
            prefabPool.cullDespawned = true;
            prefabPool.cullAbove = 1;
            prefabPool.cullDelay = 2;
            prefabPool.cullMaxPerPass = 5;

            spawnPool.CreatePrefabPool(prefabPool);
            m_SpawnPoolDic.Add(poolType, spawnPool);
        }

        return spawnPool.Spawn(prefab);
    }

    public void Despawn(PoolType poolType, Transform prefabTrans)
    {
        if (m_SpawnPoolDic.ContainsKey(poolType))
        {
            m_SpawnPoolDic[poolType].Despawn(prefabTrans);
        }
        else
        {
            AppDebug.LogError(string.Format("[RecyclePoolMgr]Pool don't contain PoolType : {0}", poolType));
        }
    }

    private IEnumerator DespawnCorotine(PoolType poolType, Transform prefabTrans, float delay)
    {
        yield return new WaitForSeconds(delay);
        Despawn(poolType, prefabTrans);
    }

    public void Despawn(PoolType poolType, Transform prefabTrans, float delay)
    {
        if(delay > 0)
        {
            StartCoroutine(DespawnCorotine(poolType, prefabTrans, delay));
        }
        else
        {
            Despawn(poolType, prefabTrans);
        }
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        foreach (var item in m_SpawnPoolDic.Values)
        {
            Destroy(item.gameObject);
        }
        m_SpawnPoolDic.Clear();

        m_CacheResDic.Clear();
    }

    public void Clear()
    {
        Destroy(gameObject);
    }
}
