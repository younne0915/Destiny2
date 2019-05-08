using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<LuaMgr>();
    }

    private void Start()
    {
        LuaMgr.Instance.DoString("require 'Download/xLuaLogic/Core/Main'");
    }
}
