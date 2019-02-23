using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class LanguageSettingWindow : EditorWindow
{
    private List<string> m_List = new List<string>();
    private int m_Index;
    public LanguageSettingWindow()
    {
        m_List.Add("CN");
        m_List.Add("EN");
    }

    void OnGUI()
    {
        EditorGUILayout.BeginHorizontal("box");
        m_Index = EditorGUILayout.Popup("语言", m_Index, m_List.ToArray(), new GUILayoutOption[0]);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save", GUILayout.Width(100), GUILayout.Height(30)))
        {
            SaveLanguage();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SaveLanguage()
    {
        LanguageDBModel.Instance.CurrLanguage = (Language)Enum.Parse(typeof(Language), m_List[m_Index]);
        Youyou_TextCom[] comArr = UnityEngine.Object.FindObjectsOfType<Youyou_TextCom>();
        if(comArr != null && comArr.Length > 0)
        {
            for (int i = 0; i < comArr.Length; i++)
            {
                comArr[i].Refresh();
            }
        }
        //通知面板 值改变了
        EditorUtility.SetDirty(this);

        //通知Unity 改变了
        EditorApplication.MarkSceneDirty();
    }
}
