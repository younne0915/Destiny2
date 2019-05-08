using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestLua : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //LuaHelper.Instance.UISceneCtrl.LoadSceneUI("Download/Prefab/xLuaUIPrefab/UIRootView");
        // Button btn = transform.GetComponent<UnityEngine.UI.Button>();
        //btn.onClick.AddListener(aaaaa);
        //Transform aa = transform.Find("UIRootView/Canvas/ContainerBottomRight/btnOpenTask");

        //transform.Find("detailContainer/txtTaskName").GetComponent<UnityEngine.UI.Text>();
        //transform.Find("detailContainer/txtTaskId").GetComponent<UnityEngine.UI.Text>();
        //transform.Find("detailContainer/txtTaskStatus").GetComponent<UnityEngine.UI.Text>();
        //transform.Find("detailContainer/txtTaskName").GetComponent<UnityEngine.UI.Text>();
        //transform.Find("detailContainer/txtTaskContent").GetComponent<UnityEngine.UI.Text>();

        //transform.parent = null;
        //transform.localPosition = Vector3.zero;
        //transform.localScale = Vector3.one;

        //GetComponent<UnityEngine.UI.Button>().onClick.AddListener = null;

        MMO_MemoryStream ms = new MMO_MemoryStream();
        ms.WriteShort(100);

    }

    private void aaaaa()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
