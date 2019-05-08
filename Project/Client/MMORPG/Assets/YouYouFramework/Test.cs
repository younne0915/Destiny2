using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(WaitOneFrame());
	}

    IEnumerator WaitOneFrame()
    {
        yield return null;
        Debug.Log("Test: Start");
        YouYou.GameEntry.Event.commonEvent.AddEventHandler(YouYou.CommonEventId.RegComplete, OnRegComplete);
    }

    private void OnRegComplete(object p)
    {
        Debug.Log(p);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.B))
        {
            float startTime = Time.realtimeSinceStartup;
            //AppDebug.LogError("Start Time : " + DateTime.Now.Millisecond);
            for (int i = 0; i < 10000; i++)
            {
                YouYou.GameEntry.Event.commonEvent.Dispatch(YouYou.CommonEventId.RegComplete, 123);
            }
            float deltTime = Time.realtimeSinceStartup - startTime;
            AppDebug.LogError("deltTime Time : " + deltTime);
        }
    }

    private void OnDestroy()
    {
        YouYou.GameEntry.Event.commonEvent.RemoveEventHandler(YouYou.CommonEventId.RegComplete, OnRegComplete);
    }
}
