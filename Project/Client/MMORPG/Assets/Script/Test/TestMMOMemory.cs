using UnityEngine;
using System.Collections;
using System;

public class TestMMOMemory : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //int a = 217380178;
        //byte[] arr = BitConverter.GetBytes(a);

        //for (int i = 0; i < arr.Length; i++)
        //{
        //    Debug.Log(string.Format("arr[{0}] = {1}", i, arr[i]));
        //}

        //82  245 244 12

        byte[] arr = new byte[4];
        arr[0] = 82;
        arr[1] = 245;
        arr[2] = 244;
        arr[3] = 12;

        int a = BitConverter.ToInt32(arr, 0);
        Debug.Log(string.Format("a = {0}", a));
    }

    // Update is called once per frame
    void Update () {
	
	}
}
