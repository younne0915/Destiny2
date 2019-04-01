using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWorldMapData
{
    public int NPCId { get; set; }
    public Vector3 NPCPosition { get; set; }
    public float EulerAnglesY { get; set; }
    public string Prologue { get; set; }

    public NPCEntity npcEntity
    {
        get;
        private set;
    }

    public NPCWorldMapData(int npcId, Vector3 npcPos, float eY, string prologue)
    {
        NPCId = npcId;
        NPCPosition = npcPos;
        EulerAnglesY = eY;
        Prologue = prologue;
        npcEntity = NPCDBModel.Instance.Get(npcId);
    }
}
