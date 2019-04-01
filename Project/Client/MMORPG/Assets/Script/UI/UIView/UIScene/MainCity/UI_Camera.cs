using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Camera : SingletonInstance<UI_Camera> {

    [HideInInspector]
    public Camera Camera;

    // Use this for initialization
    void Start ()
    {
        Camera = GetComponent<Camera>();
    }
}
