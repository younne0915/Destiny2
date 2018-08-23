using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class TestMMOMemory : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        List<ProductEntity> lst = ProductDBModel.instance.GetList();

        for (int i = 0; i < lst.Count; i++)
        {
            Debug.Log(string.Format("list[{0}] = {1}", i, lst[i].Name));
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
