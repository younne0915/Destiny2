using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIGameServerItemView : MonoBehaviour
{
    [SerializeField]
    private Sprite[] m_GameServerStatus;

    [SerializeField]
    private Image m_CurrGameServerStatus;

    [SerializeField]
    private Text m_GameServerName;

    private RetGameServerEntity m_GameServerEntity;

    public Action<RetGameServerEntity> OnGameServerItemClick;

    void Start()
    {
        Button btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(OnGameServerItemClickCallback);
        }
    }

    public void SetUI(RetGameServerEntity entity)
    {
        m_GameServerEntity = entity;
        m_CurrGameServerStatus.overrideSprite = m_GameServerStatus[entity.RunStatus];
        m_GameServerName.text = entity.Name;
    }

    private void OnGameServerItemClickCallback()
    {
        if (OnGameServerItemClick != null) OnGameServerItemClick(m_GameServerEntity);
    }
}
