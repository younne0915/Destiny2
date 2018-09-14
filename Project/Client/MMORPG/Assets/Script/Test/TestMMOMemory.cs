using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using LitJson;
using System.Text;

public class TestMMOMemory : MonoBehaviour {

	void Start ()
    {
        //NetWorkSocket.Instance.Connect("169.254.116.88", 1011);

        TestProto proto = new TestProto();
        proto.id = 10;
        proto.price = 1.5d;
        proto.des = "这是个好的协议";
        proto.date = 2018.8d;

        //string json = JsonMapper.ToJson(proto);
        //Debug.LogError("json = " + json);

        //using(MMO_MemoryStream ms = new MMO_MemoryStream())
        //{
        //    ms.WriteUTF8String(json);
        //    //json 长度为67
        //    Debug.LogError("array.length = " + ms.ToArray().Length);
        //}

        byte[] buffer = proto.ToArray();
        //byte 长度为43
        Debug.LogError("buffer.length = " + buffer.Length);

        TestProto tProto = TestProto.GetTestProto(buffer);
        Debug.LogError("tProto.des = " + tProto.des);
    }

    //void Send(string msg)
    //{
    //    using (MMO_MemoryStream ms = new MMO_MemoryStream())
    //    {
    //        ms.WriteUTF8String(msg);
    //        NetWorkSocket.Instance.SendMsg(ms.ToArray());
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Send("你好Q");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.W))
    //    {
    //        Send("你好W");
    //    }
    //    else if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        for (int i = 0; i < 10; i++)
    //        {
    //            Send("你好循环 " + i);
    //        }
    //    }
    //}
}
