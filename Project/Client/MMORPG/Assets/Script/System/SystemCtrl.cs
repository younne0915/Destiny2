using UnityEngine;
using System.Collections;
using System;

public class SystemCtrl<T> : IDisposable where T : new()
{
    #region µ¥Àý
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public virtual void Dispose()
    {

    }
    #endregion

    protected void AddEventHandler(string key, DispatcherBase<UIDispatcher, object[], string> .OnActionHandler handler)
    {
        UIDispatcher.Instance.AddEventHandler(key, handler);
    }

    protected void RemoveEventHandler(string key, DispatcherBase<UIDispatcher, object[], string>.OnActionHandler handler)
    {
        UIDispatcher.Instance.RemoveEventHandler(key, handler);
    }

    protected void ShowMessage(string title, string message, MessageViewType type = MessageViewType.Ok, Action okAction = null, Action cancelAction = null)
    {
        MessageCtrl.Instance.Show(title, message, type, okAction, cancelAction);
    }

    protected void LogError(object message)
    {
        AppDebug.LogError(message);
    }

    public static void Log(object message)
    {
        AppDebug.Log(message);
    }
}
