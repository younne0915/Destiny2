using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using XLua;

[LuaCallCSharp]
public class SocketDispatcher : IDisposable/*DispatcherBase<SocketDispatcher, byte[], ushort>*/
{
    #region µ¥Àý
    private static SocketDispatcher instance;

    public static SocketDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SocketDispatcher();
            }
            return instance;
        }
    }

    public void Dispose()
    {

    }
    #endregion

    [CSharpCallLua]
    public delegate void OnActionHandler(byte[] p);

    private Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

    public void AddEventHandler(ushort key, OnActionHandler handler)
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

    public void RemoveEventHandler(ushort key, OnActionHandler handler)
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

    public void Dispatch(ushort key, byte[] param)
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