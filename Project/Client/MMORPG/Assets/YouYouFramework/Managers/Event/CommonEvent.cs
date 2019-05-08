using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class CommonEvent : System.IDisposable
    {
        public delegate void OnActionHandler(object p);

        private Dictionary<ushort, List<OnActionHandler>> dic = new Dictionary<ushort, List<OnActionHandler>>();

        public void AddEventHandler(ushort key, OnActionHandler handler)
        {
            List<OnActionHandler> list = null;
            dic.TryGetValue(key, out list);
            if(list == null)
            {
                list = new List<OnActionHandler>();
                dic[key] = list;
            }
            list.Add(handler);
        }

        public void RemoveEventHandler(ushort key, OnActionHandler handler)
        {
            List<OnActionHandler> list = null;
            dic.TryGetValue(key, out list);

            if(list != null)
            {
                list.Remove(handler);
                if (list.Count == 0)
                {
                    dic.Remove(key);
                }
            }
        }

        public void Dispatch(ushort key, object param)
        {
            //if (dic.ContainsKey(key))
            //{
            //    List<OnActionHandler> list = dic[key];
            //    if (list != null && list.Count > 0)
            //    {
            //        for (int i = 0; i < list.Count; i++)
            //        {
            //            if (list[i] != null && list[i].Target != null)
            //            {
            //                list[i](param);
            //            }
            //        }
            //    }
            //}

            List<OnActionHandler> list = null;
            dic.TryGetValue(key, out list);

            if (list != null)
            {
                int count = list.Count;
                OnActionHandler onActionHandler = null;

                for (int i = 0; i < count; i++)
                {
                    onActionHandler = list[i];
                    if (onActionHandler != null && onActionHandler.Target != null)
                    {
                        onActionHandler(param);
                    }
                }
            }
        }

        public void Dispose()
        {
            dic.Clear();
        }
    }
}
