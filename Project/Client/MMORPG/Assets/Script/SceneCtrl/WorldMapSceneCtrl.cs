//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-06 07:43:41
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class WorldMapSceneCtrl : GameSceneCtrlBase
{
    private WorldMapEntity CurrWorldMapEntity;

    protected override void OnAwake()
    {
        base.OnAwake();
    }
    
    protected override void OnLoadUIMainCityView()
    {
        RoleMgr.Instance.InitMainPlayer();

        if (GlobalInit.Instance != null && GlobalInit.Instance.CurrPlayer != null)
        {
            CurrWorldMapEntity = WorldMapDBModel.Instance.Get(SceneMgr.Instance.CurrWorldMapId);
            if (CurrWorldMapEntity != null)
            {
                GlobalInit.Instance.CurrPlayer.Born(CurrWorldMapEntity.RoleBirthPosition);
                GlobalInit.Instance.CurrPlayer.transform.eulerAngles = new Vector3(0, CurrWorldMapEntity.RoleBirthEulerAnglesY, 0);
            }
            else
            {
                GlobalInit.Instance.CurrPlayer.Born(PlayerBornPos);
            }

            PlayerCtrl.Instance.SetMainCityData();
        }

        //加载完毕
        if (DelegateDefine.Instance.OnSceneLoadOK != null)
        {
            DelegateDefine.Instance.OnSceneLoadOK();
        }

        StartCoroutine(InitNPC());
    }

    private IEnumerator InitNPC()
    {
        yield return null;

        if (CurrWorldMapEntity == null) yield break;

        for (int i = 0; i < CurrWorldMapEntity.NPCWorldMapDataList.Count; i++)
        {
            NPCWorldMapData data = CurrWorldMapEntity.NPCWorldMapDataList[i];
            GameObject obj = RoleMgr.Instance.LoadNPC(data.npcEntity.PrefabName);
            obj.transform.position = data.NPCPosition;
            obj.transform.eulerAngles = new Vector3(0, data.EulerAnglesY, 0);
            obj.transform.localScale = Vector3.one;
            NPCCtrl ctrl = obj.GetComponent<NPCCtrl>();
            ctrl.Init(data);
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyUp(KeyCode.A))
        {
            SceneMgr.Instance.LoadToShamo();
        }
    }
}