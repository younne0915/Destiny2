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
        List<JobEntity> lst = JobDBModel.instance.GetList();

        for (int i = 0; i < lst.Count; i++)
        {
            Debug.Log("name = " + lst[i].Name);
        }

        //List<ProductEntity> lst = ProductDBModel.instance.GetList();

        //for (int i = 0; i < lst.Count; i++)
        //{
        //    Debug.Log("name = " + lst[i].Name);
        //}
    }

    // Update is called once per frame
    void Update () {
	
	}
}
