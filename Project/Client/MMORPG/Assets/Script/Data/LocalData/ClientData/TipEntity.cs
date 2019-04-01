using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipEntity
{
    public int type;
    public string content;

    public TipEntity(int tipType, string text)
    {
        type = tipType;
        content = text;
    }

}
