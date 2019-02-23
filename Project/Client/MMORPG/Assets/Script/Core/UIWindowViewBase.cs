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

    /// <summary>
    /// 下一个打开窗口类型
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
    /// 关闭窗口
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
    /// 销毁之前执行
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
