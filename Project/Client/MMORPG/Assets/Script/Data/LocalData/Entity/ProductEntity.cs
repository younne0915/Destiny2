using UnityEngine;
using System.Collections;

/// <summary>
/// Productʵ��
/// </summary>
public class ProductEntity : AbstractEntity
{
    /// <summary>
    /// Product����
    /// </summary>
    public string Name { set; get; }

    /// <summary>
    /// �۸�
    /// </summary>
    public float Price { get; set; }

    /// <summary>
    /// ͼƬ����
    /// </summary>
    public string PicName { get; set; }

    /// <summary>
    /// ����
    /// </summary>
    public string Desc { get; set; }

}
