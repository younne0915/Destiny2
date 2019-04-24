//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-15 23:07:03
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class RoleMgr : Singleton<RoleMgr>
{
    private bool m_IsMainPlayerInit = false;
    /// <summary>
    /// 初始化主角
    /// </summary>
    public void InitMainPlayer()
    {
        if (m_IsMainPlayerInit) return;
        if (GlobalInit.Instance.MainPlayerInfo != null)
        {
            JobEntity jobEntity = JobDBModel.Instance.Get(GlobalInit.Instance.MainPlayerInfo.JobId);
            if (jobEntity != null)
            {
                GameObject mainPlayer = GameObject.Instantiate(LoaderMgr.Instance.Load(string.Format("Download/Prefab/RolePrefab/Player/{0}", jobEntity.PrefabName), jobEntity.PrefabName));
                //GameObject mainPlayer = RecyclePoolMgr.Instance.Spawn(PoolType.Player, ResourLoadType.AssetBundle, string.Format("Role/{0}", jobEntity.PrefabName)).gameObject;
                mainPlayer.SetParent(null);
                UnityEngine.Object.DontDestroyOnLoad(mainPlayer);
                GlobalInit.Instance.CurrPlayer = mainPlayer.GetComponent<RoleCtrl>();
                GlobalInit.Instance.MainPlayerInfo.SetPhySkillId(jobEntity.UsedPhyAttackIds);
                GlobalInit.Instance.CurrPlayer.Init(RoleType.MainPlayer, GlobalInit.Instance.MainPlayerInfo, new RoleMainPlayerCityAI(GlobalInit.Instance.CurrPlayer));
            }

        }
        m_IsMainPlayerInit = true;
    }

    #region LoadRole 根据角色预设名称 加载角色
    /// <summary>
    /// 根据角色预设名称 加载角色
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadRole(string name, RoleType type)
    {
        string path = string.Empty;

        switch (type)
        {
            case RoleType.MainPlayer:
                path = "Player";
                break;
            case RoleType.Monster:
                path = "Monster";
                break;
        }

        return ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Role, string.Format("{0}/{1}", path, name), cache: true);
    }
    #endregion

    public GameObject LoadPlayer(int jobId)
    {
        JobEntity jobEntity = JobDBModel.Instance.Get(jobId);
        return RecyclePoolMgr.Instance.Spawn(PoolType.Player, ResourLoadType.AssetBundle, string.Format("Download/Prefab/RolePrefab/Player/{0}", jobEntity.PrefabName)).gameObject;
    }

    public void LoadNPC(string prefabName, Action<GameObject> onComplete)
    {
        LoaderMgr.Instance.LoadOrDownload(string.Format("Download/Prefab/RolePrefab/NPC/{0}", prefabName), prefabName, onComplete);
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    public Sprite LoadHeadSprite(string headPic)
    {
        return Resources.Load<Sprite>(string.Format("UI/HeadImg/{0}", headPic));
    }

    public GameObject LoadMonster(string prefabName)
    {
        return LoaderMgr.Instance.Load(string.Format("Download/Prefab/RolePrefab/Monster/{0}", prefabName), prefabName);
    }

    public Sprite LoadSkillPic(string skillPic)
    {
        return Resources.Load<Sprite>(string.Format("UI/SkillIco/{0}", skillPic));
    }
}