//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2019-03-24 19:58:13
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 服务器返回购买商城物品消息
/// </summary>
public struct Shop_BuyProductReturnProto : IProto
{
    public ushort ProtoCode { get { return 16002; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSuccess);
            if(!IsSuccess)
            {
            }
            ms.WriteInt(MsgCode);
            return ms.ToArray();
        }
    }

    public static Shop_BuyProductReturnProto GetProto(byte[] buffer)
    {
        Shop_BuyProductReturnProto proto = new Shop_BuyProductReturnProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            ms.ReadUShort();
            proto.IsSuccess = ms.ReadBool();
            if(!proto.IsSuccess)
            {
            }
            proto.MsgCode = ms.ReadInt();
        }
        return proto;
    }
}