using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIGameServerPageItemView : MonoBehaviour
{
    private int m_PageIndex;

    [SerializeField]
    private Text m_Name;

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
        m_Name.text = entity.Name;
    }
}
