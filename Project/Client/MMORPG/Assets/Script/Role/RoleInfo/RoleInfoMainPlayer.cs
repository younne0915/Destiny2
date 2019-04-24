//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:58:43
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 主角信息
/// </summary>
public class RoleInfoMainPlayer : RoleInfoBase
{
    public int Money; //元宝
    public int Gold; //金币
    public byte JobId; //职业编号
    public int TotalRechargeMoney; //总充值金额

    public RoleInfoMainPlayer()
    {

    }

    public RoleInfoMainPlayer(RoleOperation_SelectRoleInfoReturnProto proto) :base()
    {
        RoleId = proto.RoldId;
        RoleNickName = proto.RoleNickName;
        Level = proto.Level;
        Exp = proto.Exp;
        MaxHP = proto.MaxHP;
        MaxMP = proto.MaxMP;
        CurrHP = proto.CurrHP + 5009999;
        CurrMP = proto.CurrMP;
        Attack = proto.Attack;
        Defense = proto.Defense;
        Hit = proto.Hit;
        Dodge = proto.Dodge;
        Cri = proto.Cri;
        Res = proto.Res;
        Fighting = proto.Fighting;
        Money = proto.Money;
        Gold = proto.Gold;
        JobId = proto.JobId;
        TotalRechargeMoney = proto.TotalRechargeMoney;

        LastInWorldMapId = proto.LastInWorldMapId;
        string[] arr = proto.LastInWorldMapPos.Split('_');
        if(arr != null && arr.Length >= 4)
        {
            LastInWorldMapPos = new Vector3(arr[0].ToFloat(), arr[1].ToFloat(), arr[2].ToFloat());
            LastInWorldMapRotateY = arr[3].ToFloat();
        }
    }
}