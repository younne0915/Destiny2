using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// HttpͨѶ����
/// </summary>
public class NetWorkHttp : MonoBehaviour {

    #region ����
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
    /// Web����ص�
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

    #region GetUrl Get����
    /// <summary>
    /// Get����
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
                    m_CallBackArgs.Error = "δ��������";
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

    #region PostUrl Post����
    private void PostUrl(string url, string json)
    {

    }
    #endregion


    /// <summary>
    /// Web����ص�����
    /// </summary>
    public class CallBackArgs
    {
        /// <summary>
        /// �Ƿ񱨴�
        /// </summary>
        public bool IsError;

        /// <summary>
        /// ����ԭ��
        /// </summary>
        public string Error;

        /// <summary>
        /// Json����
        /// </summary>
        public string Json;

    }

}
