using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectRoleItemView : MonoBehaviour
{
    private int m_RoleId;

    [SerializeField]
    private Text m_LblNickName;

    [SerializeField]
    private Text m_LblLevel;

    [SerializeField]
    private Text m_LblJobName;

    [SerializeField]
    private Image m_ImgRoleHeadPic;

    private Action<int> m_OnSelectRole;

    private float m_MoveTargetX;

    private void OnDestroy()
    {
        m_LblNickName = null;
        m_LblLevel = null;
        m_LblJobName = null;
        m_ImgRoleHeadPic = null;
        m_OnSelectRole = null;
    }

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        if(btn != null)
        {
            btn.onClick.AddListener(RoleItemClick);
        }
        m_MoveTargetX = transform.localPosition.x - 50;
        //SetAutoKill动画播放完成后是否销毁，如果是true，那这个动画只能播放一次，如果是false，那么能一直播放。
        transform.DOLocalMoveX(m_MoveTargetX, 0.2f).SetAutoKill(false).SetEase(GlobalInit.Instance.UIAnimationCurve).Pause();
    }

    private void RoleItemClick()
    {
        if (m_OnSelectRole != null)
            m_OnSelectRole(m_RoleId);
    }

    public void SetUI(int roleId, string nickName, int level, int jobId, Sprite headPic, Action<int> OnSelectRoleCallback)
    {
        m_RoleId = roleId;
        m_LblNickName.text = nickName;
        m_LblLevel.text = string.Format("Lv {0}", level);
        m_LblJobName.text = JobDBModel.Instance.Get(jobId).Name;
        m_ImgRoleHeadPic.sprite = headPic;
        m_OnSelectRole = OnSelectRoleCallback;
    }

    public void SetSelected(int roleId)
    {
        if (m_RoleId == roleId)
        {
            transform.DOPlayForward();
        }
        else
        {
            transform.DOPlayBackwards();
        }
    }
}
