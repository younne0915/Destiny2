using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// HttpͨѶ����
/// </summary>
public class NetWorkHttp : SingletonMono<NetWorkHttp>
{
    #region ����
    /// <summary>
    /// Web����ص�
    /// </summary>
    //private Action<CallBackArgs> m_CallBack;
    //private CallBackArgs m_CallBackArgs = new CallBackArgs();

    private Action<RetValue> m_CallBack;
    private RetValue m_RetValue = new RetValue();

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
    public void SendData(string url, Action<RetValue> callBack, bool isPost = false, Dictionary<string,object> dic = null)
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
            if(dic != null)
            {
                dic["deviceUniqueIdentifier"] = DeviceUtil.DeviceUniqueIdentifier;
                dic["deviceModel"] = DeviceUtil.DeviceModel;
                dic["sign"] = EncryptUtil.Md5(string.Format("{0}:{1}", GlobalInit.Instance.CurrServerTime, DeviceUtil.DeviceUniqueIdentifier));
                dic["t"] = GlobalInit.Instance.CurrServerTime;
            }
            PostUrl(url, dic == null ? "" : LitJson.JsonMapper.ToJson(dic));
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
                    m_RetValue.HasError = true;
                    m_RetValue.ErrorMsg = "δ��������";
                    m_CallBack(m_RetValue);
                    m_CallBack = null;
                }
            }
            else
            {
                if (m_CallBack != null)
                {
                    RetValue retValue =LitJson.JsonMapper.ToObject<RetValue>(data.text);
                    m_CallBack(retValue);
                    m_CallBack = null;
                }
            }
        }
        else
        {
            if (m_CallBack != null)
            {
                RetValue retValue = LitJson.JsonMapper.ToObject<RetValue>(data.text);
                m_CallBack(retValue);
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
