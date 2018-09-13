using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.Collections.Generic;
using System.Text;

public class NetWorkSocket : MonoBehaviour {

    #region 单例
    private static NetWorkSocket instance;

    public static NetWorkSocket Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("NetWorkSocket");
                DontDestroyOnLoad(obj);
                instance = obj.GetOrCreatComponent<NetWorkSocket>();
            }
            return instance;
        }
    }

    #endregion

    private byte[] buffer = new byte[10240];

    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();

    private Action m_CheckSendQueue;

    private Socket m_Client;

    void OnDestroy()
    {
        if(m_Client != null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_Client.Close();
        }
    }

    public void Connect(string ip, int port)
    {
        if (m_Client != null && m_Client.Connected) return;

        m_Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        try
        {
            m_Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            m_CheckSendQueue = OnCheckSendQueueCallback;
            Debug.LogError("连接成功");
        }
        catch (Exception ex)
        {
            Debug.LogError("连接失败" + ex.Message);
        }
    }

    private void OnCheckSendQueueCallback()
    {
        lock (m_SendQueue)
        {
            if(m_SendQueue.Count > 0)
            {
                Send(m_SendQueue.Dequeue());
            }
        }
    }

    private byte[] MakeData(byte[] data)
    {
        byte[] retBuffer = null;
        using(MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort((ushort)data.Length);
            ms.Write(data, 0, data.Length);
            retBuffer = ms.ToArray();
        }
        return retBuffer; 
    }

    /// <summary>
    /// 把消息加入队列
    /// </summary>
    /// <param name="buffer"></param>
    public void SendMsg(byte[] buffer)
    {
        byte[] sendBuffer = MakeData(buffer);

        lock (m_SendQueue)
        {
            m_SendQueue.Enqueue(sendBuffer);

            if (m_CheckSendQueue != null)
            {
                m_CheckSendQueue.BeginInvoke(null, null);
            }
        }
    }

    private void Send(byte[] buffer)
    {
        Debug.LogError("send length = " + buffer.Length);
        m_Client.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCallback, m_Client);
    }

    private void SendCallback(IAsyncResult ar)
    {
        m_Client.EndSend(ar);

        if (m_CheckSendQueue != null)
        {
            m_CheckSendQueue.Invoke();
        }
    }
}
