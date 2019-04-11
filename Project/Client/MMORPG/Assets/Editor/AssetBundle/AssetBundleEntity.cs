using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleEntity
{
    /// <summary>
    /// ���ڴ��ʱ��ѡ����ΨһKey
    /// </summary>
    public string Key;

    public string Name;
    public string Tag;
    public bool IsFolder;
    public bool IsFirstData;
    public bool IsChecked = false;

    private List<string> m_PathList = new List<string>();

    public List<string> PathList
    {
        get { return m_PathList; }
    }
}
