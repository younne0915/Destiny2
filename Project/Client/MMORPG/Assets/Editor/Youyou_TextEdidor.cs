//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-05-16 22:40:26
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Youyou_TextCom), true)]
public class Youyou_TextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Youyou_TextCom com = base.target as Youyou_TextCom;

        int valueIndex = 0, index = 0;

        //模块
        valueIndex = LanguageDBModel.Instance.GetModules().IndexOf(com.Module);

        index = EditorGUILayout.Popup("模块", valueIndex, LanguageDBModel.Instance.GetModules().ToArray(), new GUILayoutOption[0]);
        if (valueIndex != index)
        {
            com.Module = LanguageDBModel.Instance.GetModules()[index];

            //通知面板 值改变了
            EditorUtility.SetDirty(base.target);

            //通知Unity 改变了
            EditorApplication.MarkSceneDirty();

            com.Refresh();
        }


        //Key
        valueIndex = LanguageDBModel.Instance.GetKeysByModule(com.Module).IndexOf(com.Key);

        index = EditorGUILayout.Popup("Key", valueIndex, LanguageDBModel.Instance.GetKeysByModule(com.Module).ToArray(), new GUILayoutOption[0]);
        if (valueIndex != index)
        {
            com.Key = LanguageDBModel.Instance.GetKeysByModule(com.Module)[index];

            //通知面板 值改变了
            EditorUtility.SetDirty(base.target);

            //通知Unity 改变了
            EditorApplication.MarkSceneDirty();

            com.Refresh();
        }
    }
}