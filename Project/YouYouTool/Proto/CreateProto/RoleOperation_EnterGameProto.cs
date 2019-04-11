//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2019-04-04 12:24:09
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 客户端发送进入游戏消息
/// </summary>
public struct RoleOperation_EnterGameProto : IProto
{
    public ushort ProtoCode { get { return 10007; } }

    public int RoleId; //角色编号
    public int ChannelId; //渠道号

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteInt(RoleId);
            ms.WriteInt(ChannelId);
            return ms.ToArray();
        }
    }

    public static RoleOperation_EnterGameProto GetProto(byte[] buffer)
    {
        RoleOperation_EnterGameProto proto = new RoleOperation_EnterGameProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            ms.ReadUShort();
            proto.RoleId = ms.ReadInt();
            proto.ChannelId = ms.ReadInt();
        }
        return proto;
    }
}