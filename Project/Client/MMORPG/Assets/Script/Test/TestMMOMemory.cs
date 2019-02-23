using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using LitJson;
using System.Text;

public class TestMMOMemory : MonoBehaviour {
    string str;

    void Start ()
    {
        NetWorkSocket.Instance.Connect("172.17.129.37", 4025);

        SocketDispatcher.Instance.AddEventHandler(ProtoCodeDef.GameLevel_EnterReturn, TestProtocolRequestCallback);
    }

    private void TestProtocolRequestCallback(byte[] p)
    {
        GameLevel_EnterReturnProto proto = GameLevel_EnterReturnProto.GetProto(p);
        Debug.LogErrorFormat("TestProtocolResponseProto.IsSuccess : {0}", proto.IsSuccess);
    }

    void OnDestroy()
    {
        SocketDispatcher.Instance.RemoveEventHandler(ProtoCodeDef.GameLevel_EnterReturn, TestProtocolRequestCallback);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameLevel_EnterProto proto = new GameLevel_EnterProto();
            proto.GameLevelId = 10;
            proto.Grade = 20;
            Debug.LogError("Send Length = " + proto.ToArray().Length);
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }
}
