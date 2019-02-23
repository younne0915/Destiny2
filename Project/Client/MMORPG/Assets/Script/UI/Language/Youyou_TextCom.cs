//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-05-16 22:35:19
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Youyou_TextCom : MonoBehaviour
{
    /// <summary>
    /// 模块
    /// </summary>
    [HideInInspector]
    public string Module;

    /// <summary>
    /// Key
    /// </summary>
    [HideInInspector]
    public string Key;

    public void Refresh()
    {
        if (string.IsNullOrEmpty(Module) || string.IsNullOrEmpty(Key)) return;
        Text text = GetComponent<Text>();
        text.text = LanguageDBModel.Instance.GetText(Module, Key);
    }
}