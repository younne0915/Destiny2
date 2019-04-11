using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapTransCtrl : MonoBehaviour
{
    private int m_TransPosId;
    private int m_TargetSceneTransId;

    private int m_TargetSceneId;
    public int TargetSceneId
    {
        get { return m_TargetSceneId; }
    }


    public void SetParam(int posId, int targetTransSceneId, int targetSceneTransId)
    {
        m_TransPosId = posId;
        m_TargetSceneId = targetTransSceneId;
        m_TargetSceneTransId = targetSceneTransId;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoleCtrl ctrl = other.GetComponent<RoleCtrl>();
            if(ctrl != null && ctrl.CurrRoleType == RoleType.MainPlayer)
            {
                SceneMgr.Instance.TargetWorldMapTransPosId = m_TargetSceneTransId;
                SceneMgr.Instance.LoadToWorldMap(m_TargetSceneId);
            }
        }
    }
}
