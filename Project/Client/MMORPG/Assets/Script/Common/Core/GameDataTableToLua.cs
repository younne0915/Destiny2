using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTableToLua : IDisposable
{
    public int Row { set; get; }
    public int Column { set; get; }
    public string[][] Data { set; get; }

    public void Dispose()
    {
        
    }
}
