using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DispatcherBase<T, P, X> : IDisposable 
    where T : new()
    where P : class
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

    public delegate void OnActionHandler(P p);

    private Dictionary<X, List<OnActionHandler>> dic = new Dictionary<X, List<OnActionHandler>>();

    public void AddEventHandler(X key, OnActionHandler handler)
    {
        if (dic.ContainsKey(key))
        {
            if (!dic[key].Contains(handler))
            {
                dic[key].Add(handler);
            }
        }
        else
        {
            List<OnActionHandler> list = new List<OnActionHandler>();
            list.Add(handler);
            dic[key] = list;
        }
    }

    public void RemoveEventHandler(X key, OnActionHandler handler)
    {
        if (dic.ContainsKey(key))
        {
            List<OnActionHandler> list = dic[key];
            if (list.Contains(handler))
            {
                list.Remove(handler);
                if (list.Count == 0)
                {
                    dic.Remove(key);
                }
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("list don't contains OnButtonClickHandler : {0}", handler);
            }
        }
    }

    public void Dispatch(X key, P param)
    {
        if (dic.ContainsKey(key))
        {
            List<OnActionHandler> list = dic[key];
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != null)
                    {
                        list[i](param);
                    }
                }
            }
        }
    }
}
