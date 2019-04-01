using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    #region 成员变量或属性
    /// <summary>
    /// 昵称挂点
    /// </summary>
    [SerializeField]
    private Transform m_HeadBarPos;

    /// <summary>
    /// 头顶UI条
    /// </summary>
    private GameObject m_HeadBar;

    private NPCWorldMapData m_CurrNPCWorldMapData;

    private NPCHeadBarView m_NpcHeadBarView;

    private float m_Time = 0;

    private string[] m_NPCTalkContent;
    #endregion

    private void Start()
    {
        InitHeadBar();
        m_NPCTalkContent = m_CurrNPCWorldMapData.npcEntity.Talk.Split('|');
    }

    public void Init(NPCWorldMapData npcData)
    {
        m_CurrNPCWorldMapData = npcData;
    }

    private void Update()
    {
        if(Time.time > m_Time && m_NPCTalkContent != null && m_NPCTalkContent.Length > 0)
        {
            m_Time = Time.time + 7f;
            m_NpcHeadBarView.Talk(m_NPCTalkContent[Random.Range(0, m_NPCTalkContent.Length)], 4f);
        }
    }

    private void InitHeadBar()
    {
        if (RoleHeadBarRoot.Instance == null) return;
        if (m_CurrNPCWorldMapData == null || m_CurrNPCWorldMapData.npcEntity == null) return;
        if (m_HeadBarPos == null) return;

        //克隆预设
        m_HeadBar = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIOther, "NPCHeadBar");
        m_HeadBar.transform.parent = RoleHeadBarRoot.Instance.gameObject.transform;
        m_HeadBar.transform.localScale = Vector3.one;
        m_HeadBar.transform.localPosition = Vector3.zero;

        m_NpcHeadBarView = m_HeadBar.GetComponent<NPCHeadBarView>();

        //给预设赋值
        m_NpcHeadBarView.Init(m_HeadBarPos, m_CurrNPCWorldMapData.npcEntity.Name);
    }
}
