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

    /// <summary>
    /// ��һ���򿪴�������
    /// </summary>
    [HideInInspector]
    public WindowUIType m_OpenNextUIType;

    protected override void OnBtnClick(GameObject go)
    {
        base.OnBtnClick(go);
        if(go.name.Equals("btnClose", System.StringComparison.CurrentCultureIgnoreCase))
        {
            Close();
        }
    }

    /// <summary>
    /// �رմ���
    /// </summary>
    public virtual void Close()
    {
        UIViewUtil.Instance.CloseWindow(CurrentUIType);
    }

    public virtual void CloseAndOpenNext(WindowUIType nextWindowType)
    {
        m_OpenNextUIType = nextWindowType;
        Close();
    }

    /// <summary>
    /// ����֮ǰִ��
    /// </summary>
    protected override void BeforeOnDestroy()
    {
        LayerUIMgr.Instance.CheckOpenWindow();
        if (m_OpenNextUIType != WindowUIType.None)
        {
            UIViewMgr.Instance.OpenWindow(m_OpenNextUIType);
        }
    }
}
