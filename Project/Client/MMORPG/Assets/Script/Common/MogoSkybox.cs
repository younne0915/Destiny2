using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MogoSkybox : MonoBehaviour
{
    void Start()
    {
        Renderer[] arr = GetComponentsInChildren<Renderer>(true);
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].material.shader = GlobalInit.Instance.SkyboxShader;
            }
        }
        Destroy(this);
    }
}
