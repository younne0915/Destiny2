using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.IO;
using LitJson;

public class TestMMOMemory : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        //if (!NetWorkHttp.Instance.IsBusy)
        //{
        //    NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account?id=3", GetCallback);
        //}

        if (!NetWorkHttp.Instance.IsBusy)
        {
            JsonData jsonData = new JsonData();
            jsonData["Type"] = 0;       //0:×¢²á£¬1:µÇÂ¼
            jsonData["UserName"] = "xxx";
            jsonData["Pwd"] = "";
            NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account", PostCallback, isPost:true, json : jsonData.ToJson());
        }
    }

    private void GetCallback(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            Debug.Log(obj.ErrorMsg);
        }
        else
        {
            AccountEntity entity = LitJson.JsonMapper.ToObject<AccountEntity>(obj.Json);
            if (entity != null)
            {
                Debug.Log("callback Json: " + entity.UserName);
            }
            else
            {
                Debug.Log("entity == null ");
            }
        }

    }

    private void PostCallback(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            Debug.Log(obj.ErrorMsg);
        }
        else
        {
            Debug.Log("callback Json: " + obj.Json);

            RetValue ret = JsonMapper.ToObject<RetValue>(obj.Json);
        }

    }
}
