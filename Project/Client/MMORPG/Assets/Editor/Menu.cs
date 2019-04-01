using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class Menu
{
    [MenuItem("YouyouTools/Settings")]
    public static void Settings()
    {
        SettingsWindow win = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
        win.titleContent = new GUIContent("全局设置");
        win.Show(true);
    }

    [MenuItem("YouyouTools/LanguageSetting")]
    public static void LanguageSetting()
    {
        LanguageSettingWindow win = (LanguageSettingWindow)EditorWindow.GetWindow(typeof(LanguageSettingWindow));
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }

    [MenuItem("YouyouTools/AssetBundleCreate")]
    public static void AssetBundleCreate()
    {
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("资源打包");
        win.Show(true);
    }

    [MenuItem("YouyouTools/ClearAllPlayerPrefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}
