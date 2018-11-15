using UnityEngine;
using System.Collections;
using System;

public class UIWindowViewBase : UIViewBase
{
    /// <summary>
    /// �ҵ�����
    /// </summary>
    [SerializeField]
    public WindowUIContainerType containerType = WindowUIContainerType.Center;

    /// <summary>
    /// �򿪷�ʽ
    /// </summary>
    [SerializeField]
    public WindowShowStyle showStyle = WindowShowStyle.Normal;

    /// <summary>
    /// �򿪻�رն���Ч������ʱ��
    /// </summary>
    [SerializeField]
    public float duration = 0.2f;

    /// <summary>
    /// ��ǰ��������
    /// </summary>
    [HideInInspector]
    public WindowUIType CurrentUIType;

    public Action OnViewClose;

    private bool m_OpenNext = false;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        if(go.name.Equals("btnClose", System.StringComparison.CurrentCultureIgnoreCase))
        {
            Close(false);
        }
    }

    /// <summary>
    /// �رմ���
    /// </summary>
    public virtual void Close(bool openNext)
    {
        m_OpenNext = openNext;
        UIViewUtil.Instance.CloseWindow(CurrentUIType);
    }

    /// <summary>
    /// ����֮ǰִ��
    /// </summary>
    protected override void BeforeOnDestroy()
    {
        LayerUIMgr.Instance.CheckOpenWindow();
        if (m_OpenNext)
        {
            if (OnViewClose != null)
            {
                OnViewClose();
            }
        }
    }
}
