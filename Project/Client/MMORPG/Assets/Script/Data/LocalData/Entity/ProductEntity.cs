using UnityEngine;
using System.Collections;

/// <summary>
/// ��Ʒʵ��
/// </summary>
public class ProductEntity : AbstractEntity
{
    /// <summary>
    /// ��Ʒ���
    /// </summary>
    public int Id
    {
        get;set;
    }

    /// <summary>
    /// ��Ʒ����
    /// </summary>
    public string Name
    {
        set;get;
    }

    /// <summary>
    /// �۸�
    /// </summary>
    public float Price
    {
        get;set;
    }
    
    /// <summary>
    /// ͼƬ����
    /// </summary>
    public string PicName
    {
        get;set;
    }

    /// <summary>
    /// ����
    /// </summary>
    public string Desc
    {
        get;set;
    }

}
