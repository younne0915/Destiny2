//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-16 21:55:34
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 
/// </summary>
public class TestScene : MonoBehaviour 
{
    public static TestScene Instance;

    //Start之前执行
    void Awake()
    {
        Instance = this;

        //AssetBundleMgr.Instance.LoadClone(@"Role\role_mainplayer.assetbundle", "Role_MainPlayer");

        //AssetBundleLoaderAsync async = AssetBundleMgr.Instance.LoadAsync(@"Role\role_mainplayer.assetbundle", "Role_MainPlayer");
        //async.OnLoadComplete = OnLoadComplete;
    }

    private void OnLoadComplete(UnityEngine.Object obj)
    {
        Debug.Log("OnLoadComplete");
        Instantiate((GameObject)obj);
    }

    private int m_Ret = 0;

    //开始
    void Start()
    {
        //Debug.Log("Start" + gameObject.name);

       // StartCoroutine(Test1(5, 8));
    }

    private IEnumerator Test1(int x, int y)
    {
        yield return new WaitForSeconds(2f);
        m_Ret = x * y;
        Debug.Log("Test1协程执行完毕");
    }

    //每帧执行
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            SceneMgr.Instance.LoadToWorldMap(1);
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            SceneMgr.Instance.LoadToShamo();
        }
    }

    //Update之后执行
    void LateUpdate()
    {
        //Debug.Log("LateUpdate");
    }

    //固定时间间隔执行
    void FixedUpdate()
    {
        //Debug.Log("FixedUpdate");
    }

    //销毁的时候执行
    void OnDestroy()
    {
        //Debug.Log("OnDestroy");
    }

    //脚本可用的时候执行 在Start之前
    void OnEnable()
    {
        //Debug.Log("OnEnable");
    }

    //禁用的时候执行
    void OnDisable()
    {
        //Debug.Log("OnDisable");
    }
}