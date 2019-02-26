using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIGameServerPageItemView : MonoBehaviour
{
    private int m_PageIndex;

    [SerializeField]
    private Text m_GameServerName;

    public Action<int> OnGameServerPageClick;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GameServerPageClick);
    }

    private void GameServerPageClick()
    {
        if(OnGameServerPageClick != null)
        {
            OnGameServerPageClick(m_PageIndex);
        }
    }

    public void SetUI(RetGameServerPageEntity entity)
    {
        m_PageIndex = entity.PageIndex;
        m_GameServerName.text = entity.Name;
    }
}
