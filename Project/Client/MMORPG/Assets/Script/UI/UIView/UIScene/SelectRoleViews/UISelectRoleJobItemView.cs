using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UISelectRoleJobItemView : MonoBehaviour
{
    [SerializeField]
    private int m_JobId;

    [SerializeField]
    private int m_RotateAngle;

    public Action<int, int> OnSelectJob;

    private float m_MoveTargetX;

    private void OnDestroy()
    {
        OnSelectJob = null;
    }

    // Use this for initialization
    void Awake ()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnClick);
        m_MoveTargetX = transform.localPosition.x + 50;
        //SetAutoKill����������ɺ��Ƿ����٣������true�����������ֻ�ܲ���һ�Σ������false����ô��һֱ���š�
        transform.DOLocalMoveX(m_MoveTargetX, 0.2f).SetAutoKill(false).SetEase(GlobalInit.Instance.UIAnimationCurve).Pause();
	}

    private void OnBtnClick()
    {
        if(OnSelectJob != null)
        {
            OnSelectJob(m_JobId, m_RotateAngle);
        }
    }

    public void SetSelected(int selectJobId)
    {
        if(m_JobId == selectJobId)
        {
            transform.DOPlayForward();
        }
        else
        {
            transform.DOPlayBackwards();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
