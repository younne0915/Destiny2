using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainCityMenusView : SingletonInstance<UIMainCityMenusView>
{
    private Vector3 m_MoveTargetPos;

    private bool m_IsShow = false;

    private Action m_OnChangeSuccess;

    private bool m_IsBusy = false;

    private void Start()
    {
        m_IsShow = true;
        m_MoveTargetPos = transform.localPosition + new Vector3(0,200,0);
        transform.DOLocalMove(m_MoveTargetPos, 0.2f).SetAutoKill(false).SetEase(GlobalInit.Instance.UIAnimationCurve).Pause().OnComplete(() =>
        {
            if (m_OnChangeSuccess != null) m_OnChangeSuccess();
            m_IsBusy = false;
        }).OnRewind(()=> 
        {
            if (m_OnChangeSuccess != null) m_OnChangeSuccess();
            m_IsBusy = false;
        }) ;
    }

    public void ChangeState(Action OnChangeSuccess)
    {
        if (m_IsBusy) return;
        m_IsBusy = true;

        m_OnChangeSuccess = OnChangeSuccess;
        if (m_IsShow)
        {
            transform.DOPlayForward();
        }
        else
        {
            transform.DOPlayBackwards();
        }
        m_IsShow = !m_IsShow;
    }
}
