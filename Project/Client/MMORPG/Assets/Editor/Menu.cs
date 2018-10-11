using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class Menu
{
    [MenuItem("YouyouTools/Settings")]
    public static void Settings()
    {
        SettingsWindow win = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }

    [MenuItem("YouyouTools/AssetBundleCreate")]
    public static void AssetBundleCreate()
    {
        string path = Application.dataPath + "/../AssetBundle";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }


    }
}
