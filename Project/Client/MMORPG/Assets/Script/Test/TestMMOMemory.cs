using UnityEngine;
using System.Collections;
using System;

public class TestMMOMemory : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //int a = 217380178;
        //byte[] arr = BitConverter.GetBytes(a);

        //for (int i = 0; i < arr.Length; i++)
        //{
        //    Debug.Log(string.Format("arr[{0}] = {1}", i, arr[i]));
        //}

        //82  245 244 12

        //byte[] arr = new byte[4];
        //arr[0] = 82;
        //arr[1] = 245;
        //arr[2] = 244;
        //arr[3] = 12;

        //int a = BitConverter.ToInt32(arr, 0);
        //Debug.Log(string.Format("a = {0}", a));

        Item item = new Item(1, "sh ha");

        byte[] arr = null;
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteInt(item.id);
            ms.WriteUTF8String(item.name);
            arr = ms.ToArray();
        }
        //if(arr != null)
        //{
        //    for (int i = 0; i < arr.Length; i++)
        //    {
        //        Debug.Log(string.Format("arr[{0}] = {1}", i, arr[i]));
        //    }
        //}


        if(arr != null)
        {
            Item item2 = new Item(2, "hi");
            using (MMO_MemoryStream ms = new MMO_MemoryStream(arr))
            {
                //item2.id = ms.ReadInt();
                //item2.name = ms.ReadUTF8String();

                Debug.Log(string.Format("item2.id = {0}", item2.id));
                Debug.Log(string.Format("item2.name = {0}", item2.name));
            }
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}

public class Item
{
    public int id;
    public string name;

    public Item(int ID, string Name)
    {
        this.id = ID;
        this.name = Name;
    }
}
