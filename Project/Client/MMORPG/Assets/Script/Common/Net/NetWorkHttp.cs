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

    #region ����
    /// <summary>
    /// Web����ص�
    /// </summary>
    private Action<CallBackArgs> m_CallBack;
    private CallBackArgs m_CallBackArgs;

    /// <summary>
    /// �Ƿ�æ
    /// </summary>
    private bool m_IsBusy = false;
    public bool IsBusy
    {
        get { return m_IsBusy; }
    }
    #endregion

    #region SendData ����Web����
    /// <summary>
    /// ����Web����
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callBack"></param>
    /// <param name="isPost"></param>
    /// <param name="json"></param>
    public void SendData(string url, Action<CallBackArgs> callBack, bool isPost = false, string json = "")
    {
        if (m_IsBusy) return;
        m_IsBusy = true;

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
    #endregion

    #region GetUrl Get����
    /// <summary>
    /// Get����
    /// </summary>
    /// <param name="url"></param>
    private void GetUrl(string url)
    {
        WWW data = new WWW(url);
        StartCoroutine(Request(data));
    }

    #endregion

    #region PostUrl Post����
    /// <summary>
    /// Post����
    /// </summary>
    /// <param name="url"></param>
    /// <param name="json"></param>
    private void PostUrl(string url, string json)
    {
        //����һ����
        WWWForm form = new WWWForm();

        //�������ֵ
        form.AddField("", json);

        WWW data = new WWW(url, form);
        StartCoroutine(Request(data));
    }
    #endregion

    #region Request ���������
    /// <summary>
    /// ���������
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    private IEnumerator Request(WWW data)
    {
        yield return data;
        m_IsBusy = false;

        if (string.IsNullOrEmpty(data.error))
        {
            if (string.Equals("null", data.text))
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = true;
                    m_CallBackArgs.ErrorMsg = "δ��������";
                    m_CallBack(m_CallBackArgs);
                    m_CallBack = null;
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    m_CallBackArgs.HasError = false;
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
                m_CallBackArgs.HasError = true;
                m_CallBackArgs.ErrorMsg = data.error;
                m_CallBack(m_CallBackArgs);
                m_CallBack = null;
            }
        }
    }
    #endregion

    #region CallBackArgs Web����ص�����
    /// <summary>
    /// Web����ص�����
    /// </summary>
    public class CallBackArgs : EventArgs
    {
        /// <summary>
        /// �Ƿ񱨴�
        /// </summary>
        public bool HasError;

        /// <summary>
        /// ����ԭ��
        /// </summary>
        public string ErrorMsg;

        /// <summary>
        /// Json����
        /// </summary>
        public string Json;

    }
    #endregion
}
