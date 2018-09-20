//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-09-17 20:26:08
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// 返回测试协议
/// </summary>
public struct TestProtocolResponseProto : IProto
{
    public ushort ProtoCode { get { return 50002; } }

    public bool IsSucess; //是否成功
    public string Name; //名称
    public int ErrorCode; //错误码

    public byte[] ToArray()
    {
        using (MMO_MemoryStream ms = new MMO_MemoryStream())
        {
            ms.WriteUShort(ProtoCode);
            ms.WriteBool(IsSucess);
            if(IsSucess)
            {
                ms.WriteUTF8String(Name);
            }
            else
            {
                ms.WriteInt(ErrorCode);
            }
            return ms.ToArray();
        }
    }

    public static TestProtocolResponseProto GetProto(byte[] buffer)
    {
        TestProtocolResponseProto proto = new TestProtocolResponseProto();
        using (MMO_MemoryStream ms = new MMO_MemoryStream(buffer))
        {
            proto.IsSucess = ms.ReadBool();
            if(proto.IsSucess)
            {
                proto.Name = ms.ReadUTF8String();
            }
            else
            {
                proto.ErrorCode = ms.ReadInt();
            }
        }
        return proto;
    }
}