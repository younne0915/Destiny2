using UnityEngine;
using System.Collections;

/// <summary>
/// Product实体
/// </summary>
public class ProductEntity : AbstractEntity
{
    /// <summary>
    /// Product名称
    /// </summary>
    public string Name { set; get; }

    /// <summary>
    /// 价格
    /// </summary>
    public float Price { get; set; }

    /// <summary>
    /// 图片名称
    /// </summary>
    public string PicName { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string Desc { get; set; }

}
