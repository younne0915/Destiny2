//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-06-11 12:53:31
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
//using XLua;
using UnityEngine.UI;

//[LuaCallCSharp]
public class GameUtil
{
    #region 获取随机名字
    //姓
    static string[] surnameArray = {"司马", "欧阳", "端木", "上官", "独孤", "夏侯", "尉迟", "赫连", "皇甫", "公孙", "慕容", "长孙", "宇文", "司徒", "轩辕", "百里", "呼延", "令狐",
            "诸葛", "南宫", "东方", "西门", "李", "王", "张", "刘", "陈", "杨", "赵", "黄", "周", "胡", "林", "梁", "宋", "郑", "唐", "冯", "董", "程", "曹", "袁", "许", "沈",
            "曾", "彭", "吕", "蒋", "蔡", "魏", "叶", "杜", "夏", "汪", "田", "方", "石", "熊", "白", "秦", "江", "孟", "龙", "万", "段", "雷", "武", "乔", "洪", "鲁", "葛", "柳",
            "岳", "梅", "辛", "耿", "关", "苗", "童", "项", "裴", "鲍", "霍", "甘", "景", "包", "柯", "阮", "华", "滕", "穆", "燕", "敖", "冷", "卓", "花", "蓝", "楚", "荆", "官",
            "尉", "施", "姜", "戚", "邹", "严", "顾", "贺", "陆", "骆", "戴", "贾"};
    //男1名 
    static string[] male1Array = {"峰", "不", "近", "小", "千", "万", "百", "一", "求", "笑", "双", "凌", "伯", "仲", "叔", "飞", "晓", "昌", "霸", "冲", "留", "九", "子", "立", "小", "博",
            "才", "光", "弘", "华", "清", "灿", "俊", "凯", "乐", "良", "明", "健", "辉", "天", "星", "永", "玉", "英", "修", "义", "雪", "嘉", "成", "傲", "欣", "逸", "飘", "凌",
            "威", "火", "森", "杰", "思", "智", "辰", "元", "夕", "苍", "劲", "巨", "潇", "紫", "邪", "尘"};
    //男2名        
    static string[] male2Array = {"败", "悔", "南", "宝", "仞", "刀", "斐", "德", "云", "天", "仁", "岳", "宵", "忌", "爵", "权", "敏", "阳", "狂", "冠", "康", "平", "香", "刚", "强",
            "凡", "邦", "福", "歌", "国", "和", "康", "澜", "民", "宁", "然", "顺", "翔", "晏", "宜", "怡", "易", "志", "雄", "佑", "斌", "河", "元", "墨", "松", "林", "之",
            "翔", "竹", "宇", "轩", "荣", "哲", "风", "霜", "山", "炎", "罡", "盛", "睿", "达", "洪", "武", "耀", "磊", "寒", "冰", "潇", "痕", "岚", "空"};
    //女1名            
    static string[] female1Array = {"思", "冰", "夜", "依", "小", "香", "绿", "向", "映", "含", "曼", "春", "醉", "之", "新", "雨", "天", "如", "若", "涵", "亦", "采", "冬", "芷",
            "绮", "雅", "飞", "又", "寒", "忆", "晓", "乐", "笑", "妙", "元", "碧", "翠", "初", "怀", "幻", "慕", "秋", "语", "觅", "幼", "灵", "傲", "冷", "沛", "念", "寻",
            "水", "紫", "易", "惜", "诗", "妃", "雁", "盼", "尔", "以", "雪", "夏", "凝", "迎", "问", "宛", "梦", "怜", "听", "巧", "凡", "静"};
    //女2名
    static string[] female2Array = {"烟", "琴", "蓝", "梦", "丹", "柳", "冬", "萍", "菱", "寒", "阳", "霜", "白", "丝", "南", "真", "露", "云", "芙", "筠", "容", "香", "荷", "风", "儿",
            "雪", "巧", "蕾", "芹", "柔", "灵", "卉", "夏", "岚", "蓉", "萱", "珍", "彤", "蕊", "曼", "凡", "兰", "晴", "珊", "易", "妃", "春", "玉", "瑶", "文", "双", "竹",
            "凝", "桃", "菡", "绿", "枫", "梅", "旋", "山", "松", "之", "亦", "蝶", "莲", "柏", "波", "安", "天", "薇", "海", "翠", "槐", "秋", "雁", "夜"};

    /// <summary>
    /// 创建角色时随机名字
    /// </summary>
    public static string RandomName()
    {
        string CurName = "";  //当前的名字

        string[] CopyArray1;
        string[] CopyArray2;

        bool isMale = UnityEngine.Random.Range(0, 2) == 0;

        //判断角色是男是女
        //if(角色是男) 将男名数组复制到CopyArray中
        if (isMale)
        {
            CopyArray1 = new string[male1Array.Length];
            CopyArray2 = new string[male2Array.Length];
            male1Array.CopyTo(CopyArray1, 0);
            male2Array.CopyTo(CopyArray2, 0);
        }
        else
        {
            CopyArray1 = new string[female1Array.Length];
            CopyArray2 = new string[female2Array.Length];
            female1Array.CopyTo(CopyArray1, 0);
            female2Array.CopyTo(CopyArray2, 0);
        }

        int LastNameNum = 0;  //名的字数
        int TempRan = UnityEngine.Random.Range(1, 11);
        if (TempRan % 3 == 0)
        {
            LastNameNum = 1;
        }
        else
        {
            LastNameNum = 2;
        }

        //随机姓名+随机名字(名是一个字或者两个字)
        if (LastNameNum == 1)
        {
            int FirstNameIndex = UnityEngine.Random.Range(0, surnameArray.Length);
            int LastName1 = UnityEngine.Random.Range(0, CopyArray1.Length);
            CurName = surnameArray[FirstNameIndex] + CopyArray1[LastName1];
        }
        else if (LastNameNum == 2)
        {
            int FirstNameIndex = UnityEngine.Random.Range(0, surnameArray.Length);
            int LastName1 = UnityEngine.Random.Range(0, CopyArray1.Length);
            int LastName2 = UnityEngine.Random.Range(0, CopyArray2.Length);
            CurName = surnameArray[FirstNameIndex] + CopyArray1[LastName1] + CopyArray2[LastName2];
        }

        return CurName;
    }

    ///// <summary>
    ///// 获取图片资源
    ///// </summary>
    ///// <param name="type"></param>
    ///// <param name="picName"></param>
    ///// <returns></returns>
    //public static Sprite LoadSprite(SpriteSourceType type, string picName)
    //{
    //    string path = string.Empty;
    //    switch (type)
    //    {
    //        case SpriteSourceType.GameLevelIco:
    //            path = "UISource/GameLevel/GameLevelIco";
    //            break;
    //        case SpriteSourceType.GameLevelDetail:
    //            path = "UISource/GameLevel/GameLevelDetail";
    //            break;
    //        case SpriteSourceType.WorldMapIco:
    //            path = "UISource/WorldMap";
    //            break;
    //        case SpriteSourceType.WorldMapSmall:
    //            path = "UISource/SmallMap";
    //            break;
    //    }

    //    return Resources.Load(string.Format("{0}/{1}", path, picName), typeof(Sprite)) as Sprite;
    //}

    ///// <summary>
    ///// 获取道具图片
    ///// </summary>
    ///// <param name="goodsId"></param>
    ///// <param name="type"></param>
    ///// <returns></returns>
    //public static Sprite LoadGoodsImg(int goodsId, GoodsType type)
    //{
    //    string pathName = string.Empty;
    //    switch (type)
    //    {
    //        case GoodsType.Equip:
    //            pathName = "EquipIco";
    //            break;
    //        case GoodsType.Item:
    //            pathName = "ItemIco";
    //            break;
    //        case GoodsType.Material:
    //            pathName = "MaterialIco";
    //            break;
    //    }

    //    return Resources.Load(string.Format("UISource/{0}/{1}", pathName, goodsId), typeof(Sprite)) as Sprite;
    //}
    #endregion

    public static Texture LoadGameLevelMapPic(string pic)
    {
        return Resources.Load<Texture>(string.Format("UI/GameLevel/GameLevelMap/{0}", pic));
    }

    public static Sprite LoadGameLevelIcon(string pic)
    {
        return Resources.Load<Sprite>(string.Format("UI/GameLevel/GameLevelIcon/{0}", pic));
    }


    public static Sprite LoadGameLevelDetailImg(string pic)
    {
        return Resources.Load<Sprite>(string.Format("UI/GameLevel/GameLevelDetail/{0}", pic));
    }

    public static Sprite LoadWorldMapIcon(string pic)
    {
        return Resources.Load<Sprite>(string.Format("UI/WorldMap/{0}", pic));
    }

    public static Vector3 GetRandomPos(Vector3 startPos, Vector3 targetPos, float distance)
    {
        Vector3 direction = startPos - targetPos;
        direction.Normalize();
        direction = Quaternion.Euler(0, UnityEngine.Random.Range(-90, 90), 0) * direction;
        direction = direction * distance * UnityEngine.Random.Range(0.8f, 1);
        return targetPos + direction;
    }

    public static float GetTotalDistance(List<Vector3> path)
    {
        float pathTotalDis = 0;
        for (int i = 0; i < path.Count; i++)
        {
            if (i == path.Count - 1) break;
            float distance = Vector3.Distance(path[i], path[i + 1]);
            pathTotalDis += distance;
        }
        return pathTotalDis;
    }
}