using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleEntity
{
    /// <summary>
    /// 用于打包时候选定的唯一Key
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
