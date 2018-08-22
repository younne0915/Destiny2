using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 商品数据管理
/// </summary>
public class ProductDBModel : System.IDisposable
{
    public List<ProductEntity> lst;

    public ProductDBModel()
    {
        lst = new List<ProductEntity>();
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

    public void Dispose()
    {
        lst.Clear();
        lst = null;
    }
}
