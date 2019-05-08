using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInstance <T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    private void Awake()
    {
        Instance = this as T;
        OnAwake();
    }

    protected virtual void OnAwake()
    {

    }
}
