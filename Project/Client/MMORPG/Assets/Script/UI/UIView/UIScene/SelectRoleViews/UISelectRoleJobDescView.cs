using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISelectRoleJobDescView : MonoBehaviour
{
    [SerializeField]
    private Text lblJobName;

    [SerializeField]
    private Text lblJobDesc;

    private float m_MoveTargetY;

    private bool m_IsShow = false;

    private void OnDestroy()
    {
        lblJobName = null;
        lblJobDesc = null;
    }

    private void Awake()
    {
        m_MoveTargetY = transform.localPosition.y;
        transform.localPosition += new Vector3(0, m_MoveTargetY + 500, 0);
        //SetAutoKill����������ɺ��Ƿ����٣������true�����������ֻ�ܲ���һ�Σ������false����ô��һֱ���š�
        transform.DOLocalMoveY(m_MoveTargetY, 0.2f).SetAutoKill(false).SetEase(GlobalInit.Instance.UIAnimationCurve).Pause().OnComplete(()=> 
        {
            m_IsShow = true;
        }).OnRewind(()=> 
        {
            transform.DOPlayForward();
        });
    }

    public void SetUI(string jobName, string jobDesc)
    {
        lblJobName.text = jobName;
        lblJobDesc.text = jobDesc;

        if (m_IsShow)
        {
            transform.DOPlayBackwards();
        }
        else
        {
            transform.DOPlayForward();
        }
    }
}
