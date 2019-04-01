using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4MGround : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Renderer[] arr = GetComponentsInChildren<Renderer>(true);	
        if(arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].material.shader = GlobalInit.Instance.T4MShader;
            }
        }
        Destroy(this);
	}
}
