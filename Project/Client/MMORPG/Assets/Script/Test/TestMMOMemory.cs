using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using LitJson;
using System.Text;

public class TestMMOMemory : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        NetWorkSocket.Instance.Connect("169.254.116.88", 1011);

         
    }

    void Send(string msg)
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUTF8String(msg);
            NetWorkSocket.Instance.SendMsg(ms.ToArray());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Send("你好Q");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Send("你好W");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 10; i++)
            {
                Send("你好循环 " + i);
            }
        }
    }
}
