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

        ////GlobalInit.Instance.OnReceiveProto += OnReceiveProtoCallback;

        //EventDispatcher.Instance.AddEventHandler(ProtoCodeDef.TestProtocolResponse, OnRecvTestProtocolCallback);

        NetWorkSocket n1 =  NetWorkSocket.Instance;
        NetWorkHttp n2 = NetWorkHttp.Instance;

    }

    private void OnRecvTestProtocolCallback(byte[] buffer)
    {
        TestProtocolResponseProto response = TestProtocolResponseProto.GetProto(buffer);
        Debug.LogError("error = " + response.ErrorCode);
    }

    private void OnReceiveProtoCallback(ushort protoCode, byte[] buffer)
    {
        if (protoCode == ProtoCodeDef.TestProtocolResponse)
        {
            TestProtocolResponseProto response = TestProtocolResponseProto.GetProto(buffer);
            Debug.LogError("error = " + response.ErrorCode);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TestProtocolRequestProto proto = new TestProtocolRequestProto();
            TestProtocolRequestProto.ItemData itemData = new TestProtocolRequestProto.ItemData();
            itemData.Id = 11;
            itemData.Name = "张三";
            itemData.Percent = 0.055d;
            itemData.Price = 5.6f;
            proto.ItemDataList = new List<TestProtocolRequestProto.ItemData>();
            proto.ItemDataList.Add(itemData);
            proto.ItemCount = 1;

            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
    }
}
