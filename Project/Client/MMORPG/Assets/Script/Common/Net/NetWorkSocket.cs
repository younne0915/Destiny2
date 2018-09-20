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

    // private byte[] buffer = new byte[10240];

    private Socket m_Client;

    #region 发送消息所需变量
    private Queue<byte[]> m_SendQueue = new Queue<byte[]>();

    private Action m_CheckSendQueue;
    #endregion

    #region 接收消息所需变量
    //接受数据包的字节数组缓冲区
    private byte[] m_ReceiveBuffer = new byte[10240];

    //接收数据包的缓冲数据流
    private MMO_MemoryStream m_ReceiveMS = new MMO_MemoryStream();

    private Queue<byte[]> m_ReceiveQueue = new Queue<byte[]>();

    private int m_ReceiveCount = 0;
    #endregion

    void OnDestroy()
    {
        if(m_Client != null && m_Client.Connected)
        {
            m_Client.Shutdown(SocketShutdown.Both);
            m_Client.Close();
        }
    }

    private void Update()
    {
        if(m_ReceiveQueue.Count > 0)
        {
            while (true)
            {
                if (m_ReceiveQueue.Count == 0) break;
                if (m_ReceiveCount <= 5)
                {
                    m_ReceiveCount++;

                    lock (m_ReceiveQueue)
                    {
                        byte[] buffer = m_ReceiveQueue.Dequeue();

                        ushort protoCode = 0;
                        byte[] protoContent = new byte[buffer.Length - 2];
                        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
                        {
                            //协议编号
                            protoCode = ms.ReadUShort();
                            ms.Read(protoContent, 0, protoContent.Length);

                            EventDispatcher.Instance.Dispatch(protoCode, protoContent);
                        }
                    }
                }
                else
                {
                    m_ReceiveCount = 0;
                    break;
                }
            }
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

            ReceieveMsg();
        }
        catch (Exception ex)
        {
            Debug.LogError("连接失败" + ex.Message);
        }
    }

    #region 发送消息所需方法
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
    #endregion


    //======================================

    #region 接收消息方法
    /// <summary>
    /// 接收数据
    /// </summary>
    private void ReceieveMsg()
    {
        //异步接收数据
        m_Client.BeginReceive(m_ReceiveBuffer, 0, m_ReceiveBuffer.Length, SocketFlags.None, ReceiveCallBack, m_Client);
    }

    //接收数据回调
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            int len = m_Client.EndReceive(ar);

            Console.WriteLine("recv length = " + len);

            if (len > 0)
            {
                //已经接收到数据

                //把接收到数据 写入缓冲数据流的尾部
                m_ReceiveMS.Position = m_ReceiveMS.Length;

                //把制定长度的字节 写入数据流
                m_ReceiveMS.Write(m_ReceiveBuffer, 0, len);

                //如果缓存数据流的长度 > 2,说明至少有不完整的包过来了
                //为什么是2？因为客户端进行封包的时候，用的ushort， 长度就是2
                if (m_ReceiveMS.Length > 2)
                {
                    while (true)
                    {
                        m_ReceiveMS.Position = 0;
                        //currMsgLen 包体的长度
                        int currMsgLen = m_ReceiveMS.ReadUShort();
                        //currFulMsgLen 总包的长度 = 包头的长度 + 包体的长度
                        int currFulMsgLen = 2 + currMsgLen;

                        //如果数据流的长度>=整包的长度，说明至少收到一个完整包
                        if (m_ReceiveMS.Length >= currFulMsgLen)
                        {
                            //至少收到一个完整包

                            //定义包体的byte[]数组
                            byte[] buffer = new byte[currMsgLen];

                            //把数据流指针放到2的位置, 也就是包体的位置
                            m_ReceiveMS.Position = 2;

                            //把包体读到byte[]数组
                            m_ReceiveMS.Read(buffer, 0, currMsgLen);

                            lock (m_ReceiveQueue)
                            {
                                m_ReceiveQueue.Enqueue(buffer);
                            }

                            //================处理剩余字节数组===================

                            //剩余字节长度
                            int remainLen = (int)(m_ReceiveMS.Length - currFulMsgLen);
                            if (remainLen > 0)
                            {
                                m_ReceiveMS.Position = currFulMsgLen;
                                byte[] remainBuffer = new byte[remainLen];
                                m_ReceiveMS.Read(remainBuffer, 0, remainLen);

                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                m_ReceiveMS.Write(remainBuffer, 0, remainBuffer.Length);

                                remainBuffer = null;
                            }
                            else
                            {
                                m_ReceiveMS.Position = 0;
                                m_ReceiveMS.SetLength(0);

                                break;
                            }
                        }
                        else
                        {
                            //还没有收到完整包
                            break;
                        }
                    }
                }
                ReceieveMsg();
            }
            else
            {
                //客户端断开连接
                Debug.LogError(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));
            }
        }
        catch
        {
            //客户端断开连接
            Debug.LogError(string.Format("服务器{0}断开连接", m_Client.RemoteEndPoint.ToString()));
        }
    }
    #endregion
}
