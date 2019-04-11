//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-12 08:58:19
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色信息基类
/// </summary>
public class RoleInfoBase 
{
    public int RoleId; //角色编号
    public string RoleNickName; //角色昵称
    public int Level; //等级
    public int Exp; //经验
    public int MaxHP; //最大HP
    public int MaxMP; //最大MP
    public int CurrHP; //当前HP
    public int CurrMP; //当前MP
    public int Attack; //攻击力
    public int Defense; //防御
    public int Hit; //命中
    public int Dodge; //闪避
    public int Cri; //暴击
    public int Res; //抗性
    public int Fighting; //综合战斗力
    public int LastInWorldMapId; //最后进入的世界地图编号
    public Vector3 LastInWorldMapPos; //最后进入的世界地图坐标
    public float LastInWorldMapRotateY = 0;
    public int Equip_Weapon; //穿戴武器
    public int Equip_Pants; //穿戴护腿
    public int Equip_Clothes; //穿戴衣服
    public int Equip_Belt; //穿戴腰带
    public int Equip_Cuff; //穿戴护腕
    public int Equip_Necklace; //穿戴项链
    public int Equip_Shoe; //穿戴鞋
    public int Equip_Ring; //穿戴戒指
    public int Equip_WeaponTableId; //穿戴武器
    public int Equip_PantsTableId; //穿戴护腿
    public int Equip_ClothesTableId; //穿戴衣服
    public int Equip_BeltTableId; //穿戴腰带
    public int Equip_CuffTableId; //穿戴护腕
    public int Equip_NecklaceTableId; //穿戴项链
    public int Equip_ShoeTableId; //穿戴鞋
    public int Equip_RingTableId; //穿戴戒指

    public List<RoleInfoSkill> SkillList;
    public List<int> PhysicList;

    public RoleInfoBase()
    {
        SkillList = new List<RoleInfoSkill>();
        PhysicList = new List<int>();
    }

    public void SetPhySkillId(string phyIds)
    {
        string[] arr = phyIds.Split(';');
        if(arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                PhysicList.Add(arr[i].ToInt());
            }
        }
    }

    public RoleInfoSkill GetSkillLevelEntityBySkillIdAndSkillLevel(int skillId)
    {
        if(SkillList != null)
        {
            for (int i = 0; i < SkillList.Count; i++)
            {
                if(SkillList[i].SkillId == skillId)
                {
                    return SkillList[i];
                }
            }
        }
        return null;
    }

    public int GetSkillLevel(int skillId)
    {
        if (SkillList != null)
        {
            for (int i = 0; i < SkillList.Count; i++)
            {
                if (SkillList[i].SkillId == skillId)
                {
                    return SkillList[i].SkillLevel;
                }
            }
        }
        return 1;
    }

    public int GetCanUseSkillId()
    {
        if (SkillList != null)
        {
            for (int i = 0; i < SkillList.Count; i++)
            {
                if (Time.time > SkillList[i].SkillCDEndTime && SkillList[i].SpendMP <= GlobalInit.Instance.MainPlayerInfo.CurrMP)
                {
                    return SkillList[i].SkillId;
                }
            }
        }
        return -1;
    }

    public void SetSkillCD(int skillId)
    {
        if (SkillList != null)
        {
            for (int i = 0; i < SkillList.Count; i++)
            {
                if (SkillList[i].SkillId == skillId)
                {
                    SkillList[i].SetSkillEndCDTime();
                    break;
                }
            }
        }
    }
}