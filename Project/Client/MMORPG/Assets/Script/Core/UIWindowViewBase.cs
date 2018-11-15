using UnityEngine;
using System.Collections;
using System;

public class UIWindowViewBase : UIViewBase
{
    /// <summary>
    /// 挂点类型
    /// </summary>
    [SerializeField]
    public WindowUIContainerType containerType = WindowUIContainerType.Center;

    /// <summary>
    /// 打开方式
    /// </summary>
    [SerializeField]
    public WindowShowStyle showStyle = WindowShowStyle.Normal;

    /// <summary>
    /// 打开或关闭动画效果持续时间
    /// </summary>
    [SerializeField]
    public float duration = 0.2f;

    /// <summary>
    /// 当前窗口类型
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
    /// 关闭窗口
    /// </summary>
    public virtual void Close(bool openNext)
    {
        m_OpenNext = openNext;
        UIViewUtil.Instance.CloseWindow(CurrentUIType);
    }

    /// <summary>
    /// 销毁之前执行
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
