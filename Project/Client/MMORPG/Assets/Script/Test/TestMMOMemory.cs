using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;

public class TestMMOMemory : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account?id=3", callback);
    }

    private void callback(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.IsError)
        {
            Debug.Log(obj.Error);
        }
        else
        {
            AccountEntity entity = LitJson.JsonMapper.ToObject<AccountEntity>(obj.Json);
            Debug.Log("callback Json: " + entity.UserName);
        }

    }
}
