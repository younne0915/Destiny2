//===================================================
//��    �ߣ�����  http://www.u3dol.com  QQȺ��87481002
//����ʱ�䣺2016-05-16 22:35:19
//��    ע��
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class Youyou_TextCom : MonoBehaviour
{
    /// <summary>
    /// ģ��
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