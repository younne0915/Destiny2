using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 商品数据管理
/// </summary>
public class ProductDBModel : System.IDisposable
{
    private List<ProductEntity> lst;

    public ProductDBModel()
    {
        lst = new List<ProductEntity>();
        Load();
    }

    private static ProductDBModel _instance;

    public static ProductDBModel instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new ProductDBModel();
            }
            return _instance;
        }
    }

    private void Load()
    {
        using (GameDataTableParser paser = new GameDataTableParser(@"E:\Destiny2\Project\Client\MMORPG\www\Data\Product.data"))
        {
            while (!paser.Eof)
            {
                ProductEntity entity = new ProductEntity();
                entity.Id = paser.GetFieldValue("Id").ToInt();
                entity.Name = paser.GetFieldValue("Name");
                entity.Price = paser.GetFieldValue("Price").ToFloat();
                entity.PicName = paser.GetFieldValue("PicName");
                entity.Desc = paser.GetFieldValue("Desc");

                lst.Add(entity);
                paser.Next();
            }
        }
    }

    public List<ProductEntity> GetList()
    {
        return lst;
    }



    public void Dispose()
    {
        lst.Clear();
        lst = null;
    }
}
