using UnityEngine;
using System.Collections;

public struct TestProto
{
    public int id;
    public double price;
    public string des;
    public double date;

    public byte[] ToArray()
    {
        using(MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteInt(id);
            ms.WriteDouble(price);
            ms.WriteUTF8String(des);
            ms.WriteDouble(date);
            return ms.ToArray();
        }
    }

    public static TestProto GetTestProto(byte[] buffer)
    {
        TestProto proto = new TestProto();
        using(MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.id = ms.ReadInt();
            proto.price = ms.ReadDouble();
            proto.des = ms.ReadUTF8String();
            proto.date = ms.ReadDouble();
        }
        return proto;
    } 
}
