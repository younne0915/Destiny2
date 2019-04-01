
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2019-03-24 17:12:57
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Shop数据管理
/// </summary>
public partial class ShopDBModel : AbstractDBModel<ShopDBModel, ShopEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Shop.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ShopEntity MakeEntity(GameDataTableParser parse)
    {
        ShopEntity entity = new ShopEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.ShopCategoryId = parse.GetFieldValue("ShopCategoryId").ToInt();
        entity.GoodsType = parse.GetFieldValue("GoodsType").ToInt();
        entity.GoodsId = parse.GetFieldValue("GoodsId").ToInt();
        entity.OldPrice = parse.GetFieldValue("OldPrice").ToInt();
        entity.Price = parse.GetFieldValue("Price").ToInt();
        entity.SellStatus = parse.GetFieldValue("SellStatus").ToInt();
        return entity;
    }
}
