//===================================================
//��    �ߣ�����  http://www.u3dol.com  QQȺ��87481002
//����ʱ�䣺2017-03-09 21:03:49
//��    ע��������
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// ������
/// </summary>
public class AssetBundleDownloadRoutine : MonoBehaviour
{
    private List<DownloadDataEntity> m_List = new List<DownloadDataEntity>(); //��Ҫ���ص��ļ��б�

    private DownloadDataEntity m_CurrDownloadData; //��ǰ�������ص�����

    /// <summary>
    /// ��Ҫ���ص�����
    /// </summary>
    public int NeedDownloadCount
    {
        private set;
        get;
    }

    /// <summary>
    /// �Ѿ�������ɵ�����
    /// </summary>
    public int CompleteCount
    {
        private set;
        get;
    }

    private int m_DownloadSize; //�Ѿ����غõ��ļ����ܴ�С
    private int m_CurrDownloadSize; //��ǰ���ص��ļ���С

    /// <summary>
    /// ����������Ѿ����صĴ�С
    /// </summary>
    public int DownloadSize
    {
        get { return m_DownloadSize + m_CurrDownloadSize; }
    }

    /// <summary>
    /// �Ƿ�ʼ����
    /// </summary>
    public bool IsStartDownload
    {
        private set;
        get;
    }

    /// <summary>
    /// ������ض���
    /// </summary>
    /// <param name="entity"></param>
    public void AddDownload(DownloadDataEntity entity)
    {
        m_List.Add(entity);
    }

    /// <summary>
    /// ��ʼ����
    /// </summary>
    public void StartDownload()
    {
        IsStartDownload = true;
        NeedDownloadCount = m_List.Count;
    }

    void Update()
    {
        if (IsStartDownload)
        {
            IsStartDownload = false;
            StartCoroutine(DownloadData());
        }
    }

    private IEnumerator DownloadData()
    {
        if (NeedDownloadCount == 0) yield break;
        m_CurrDownloadData = m_List[0];

        string dataUrl = DownloadMgr.DownloadUrl + m_CurrDownloadData.FullName; //��Դ����·��
        AppDebug.Log("dataUrl=" + dataUrl);

        int lastIndex = m_CurrDownloadData.FullName.LastIndexOf('\\');

        if (lastIndex > -1)
        {
            //��·�� ���ڴ����ļ���
            string path = m_CurrDownloadData.FullName.Substring(0, lastIndex);

            //�õ�����·��
            string localFilePath = DownloadMgr.Instance.LocalFilePath + path;

            if (!Directory.Exists(localFilePath))
            {
                Directory.CreateDirectory(localFilePath);
            }
        }


        WWW www = new WWW(dataUrl);

        float timeout = Time.time;
        float progress = www.progress;

        while (www != null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeout = Time.time;
                progress = www.progress;

                m_CurrDownloadSize = (int)(m_CurrDownloadData.Size * progress);
            }

            if ((Time.time - timeout) > DownloadMgr.DownloadTimeOut)
            {
                AppDebug.LogError("����ʧ�� ��ʱ");
                yield break;
            }

            yield return null; //һ��Ҫ��һ֡
        }

        yield return www;

        if (www != null && www.error == null)
        {
            using (FileStream fs = new FileStream(DownloadMgr.Instance.LocalFilePath + m_CurrDownloadData.FullName, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(www.bytes, 0, www.bytes.Length);
            }
        }

        //���سɹ�
        m_CurrDownloadSize = 0;
        m_DownloadSize += m_CurrDownloadData.Size;

        //д�뱾���ļ�
        DownloadMgr.Instance.ModifyLocalData(m_CurrDownloadData);

        m_List.RemoveAt(0);
        CompleteCount++;

        if (m_List.Count == 0)
        {
            m_List.Clear();
        }
        else
        {
            IsStartDownload = true;
        }
    }
}