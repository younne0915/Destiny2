using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 商品数据管理
/// </summary>
public class ProductDBModel : AbstractDBModel<ProductDBModel, ProductEntity>
{
    protected override string FileName => "Product.data";

    protected override ProductEntity MakeEntity(GameDataTableParser parser)
    {
        ProductEntity entity = new ProductEntity();
        entity.Id = parser.GetFieldValue("Id").ToInt();
        entity.Name = parser.GetFieldValue("Name");
        entity.Price = parser.GetFieldValue("Price").ToFloat();
        entity.PicName = parser.GetFieldValue("PicName");
        entity.Desc = parser.GetFieldValue("Desc");
        return entity;
    }
}
