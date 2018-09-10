using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Http通讯管理
/// </summary>
public class NetWorkHttp : MonoBehaviour {

    #region 单例
    private static NetWorkHttp instance;

    public static NetWorkHttp Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject obj = new GameObject("NetWorkHttp");
                DontDestroyOnLoad(obj);
                instance = obj.GetOrCreatComponent<NetWorkHttp>();
                instance.m_CallBackArgs = new CallBackArgs();
            }
            return instance;
        }
    }

    #endregion

    /// <summary>
    /// Web请求回调
    /// </summary>
    private Action<CallBackArgs> m_CallBack;
    private CallBackArgs m_CallBackArgs;

    public void SendData(string url, Action<CallBackArgs> callBack, bool isPost = false, string json = "")
    {
        m_CallBack = callBack;

        if (!isPost)
        {
            GetUrl(url);
        }
        else
        {
            PostUrl(url, json);
        }
    }

    #region GetUrl Get请求
    /// <summary>
    /// Get请求
    /// </summary>
    /// <param name="url"></param>
    private void GetUrl(string url)
    {
        WWW data = new WWW(url);
        StartCoroutine(Get(data));
    }

    private IEnumerator Get(WWW data)
    {
        yield return data;
        if (string.IsNullOrEmpty(data.error))
        {
            if (string.Equals("null", data.text))
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.IsError = true;
                    m_CallBackArgs.Error = "未请求到数据";
                    m_CallBack(m_CallBackArgs);
                    m_CallBack = null;
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.IsError = false;
                    m_CallBackArgs.Json = data.text;
                    m_CallBack(m_CallBackArgs);
                    m_CallBack = null;
                }
            }
        }
        else
        {
            if (m_CallBack != null)
            {
                m_CallBackArgs.IsError = true;
                m_CallBackArgs.Error = data.error;
                m_CallBack(m_CallBackArgs);
                m_CallBack = null;
            }
        }
    }
    #endregion

    #region PostUrl Post请求
    private void PostUrl(string url, string json)
    {

    }
    #endregion


    /// <summary>
    /// Web请求回调数据
    /// </summary>
    public class CallBackArgs
    {
        /// <summary>
        /// 是否报错
        /// </summary>
        public bool IsError;

        /// <summary>
        /// 错误原因
        /// </summary>
        public string Error;

        /// <summary>
        /// Json数据
        /// </summary>
        public string Json;

    }

}
