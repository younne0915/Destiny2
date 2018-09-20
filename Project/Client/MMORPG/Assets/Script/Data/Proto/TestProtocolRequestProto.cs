//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-09-17 20:26:08
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 测试协议消息
/// </summary>
public struct TestProtocolRequestProto : IProto
{
    public ushort ProtoCode { get { return 50001; } }

    public int ItemCount; //Item数量
    public List<ItemData> ItemDataList; //元素列表

    /// <summary>
    /// 元素列表
    /// </summary>
    public struct ItemData
    {
        public int Id; //元素id
        public string Name; //元素名称
        public float Price; //价格
        public double Percent; //提高百分比
    }

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(ItemCount);
            for (int i = 0; i < ItemCount; i++)
            {
                ms.WriteInt(ItemDataList[i].Id);
                ms.WriteUTF8String(ItemDataList[i].Name);
                ms.WriteFloat(ItemDataList[i].Price);
                ms.WriteDouble(ItemDataList[i].Percent);
            }
            return ms.ToArray();
        }
    }

    public static TestProtocolRequestProto GetProto(byte[] buffer)
    {
        TestProtocolRequestProto proto = new TestProtocolRequestProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.ItemCount = ms.ReadInt();
            proto.ItemDataList = new List<ItemData>();
            for (int i = 0; i < proto.ItemCount; i++)
            {
                ItemData _ItemData = new ItemData();
                _ItemData.Id = ms.ReadInt();
                _ItemData.Name = ms.ReadUTF8String();
                _ItemData.Price = ms.ReadFloat();
                _ItemData.Percent = ms.ReadDouble();
                proto.ItemDataList.Add(_ItemData);
            }
        }
        return proto;
    }
}