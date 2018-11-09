using System.Collections;
using System.Collections.Generic;
using System;

public class EventDispatcher : Singleton<EventDispatcher>
{
    public delegate void OnActionHandler(byte[] buffer);

    private Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

    public void AddEventHandler(ushort protoCode, OnActionHandler handler)
    {
        if (dic.ContainsKey(protoCode))
        {
            if (!dic[protoCode].Contains(handler))
            {
                dic[protoCode].Add(handler);
            }
        }
        else
        {
            List<OnActionHandler> list = new List<OnActionHandler>();
            list.Add(handler);
            dic[protoCode] = list;
        }
    }

    public void RemoveEventHandler(ushort protoCode, OnActionHandler handler)
    {
        if (dic.ContainsKey(protoCode))
        {
            List<OnActionHandler> list = dic[protoCode];
            if (list.Contains(handler))
            {
                list.Remove(handler);
                if(list.Count == 0)
                {
                    dic.Remove(protoCode);
                }
            }
            else
            {
                UnityEngine.Debug.LogError("list don't contains OnActionHandler : " + handler);
            }
        }
    }

    public void Dispatch(ushort protoCode, byte[] buffer)
    {
        if (dic.ContainsKey(protoCode))
        {
            List<OnActionHandler> list = dic[protoCode];
            if(list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if(list[i] != null)
                    {
                        list[i](buffer);
                    }
                }
            }
        }
    }


    //-------------------------------------------------------------
    public delegate void OnButtonClickHandler(params object[] param);

    private Dictionary<string, List<OnButtonClickHandler>> dic_ButtonClick = new Dictionary<string, List<OnButtonClickHandler>>();

    public void AddBtnEventHandler(string btnKey, OnButtonClickHandler handler)
    {
        if (dic_ButtonClick.ContainsKey(btnKey))
        {
            if (!dic_ButtonClick[btnKey].Contains(handler))
            {
                dic_ButtonClick[btnKey].Add(handler);
            }
        }
        else
        {
            List<OnButtonClickHandler> list = new List<OnButtonClickHandler>();
            list.Add(handler);
            dic_ButtonClick[btnKey] = list;
        }
    }

    public void RemoveBtnEventHandler(string btnKey, OnButtonClickHandler handler)
    {
        if (dic_ButtonClick.ContainsKey(btnKey))
        {
            List<OnButtonClickHandler> list = dic_ButtonClick[btnKey];
            if (list.Contains(handler))
            {
                list.Remove(handler);
                if (list.Count == 0)
                {
                    dic_ButtonClick.Remove(btnKey);
                }
            }
            else
            {
                UnityEngine.Debug.LogErrorFormat("list don't contains OnButtonClickHandler : {0}", handler);
            }
        }
    }

    public void DispatchBtn(string btnKey, params object[] param)
    {
        if (dic_ButtonClick.ContainsKey(btnKey))
        {
            List<OnButtonClickHandler> list = dic_ButtonClick[btnKey];
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
