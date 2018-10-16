using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;

    private string[] arraTag = { "All", "Scene", "Role", "Effect", "Audio", "None"};
    private int tagIndex = 0;

    private string[] arrBuildTarget = {"Windows", "Android", "IOS" };

#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0;
#elif UNITY_ANDROID
    private BuildTarget target = BuildTarget.Android;
    private int buildTargetIndex = 1;
#elif UNITY_IPHONE
    private BuildTarget target = BuildTarget.iOS;
    private int buildTargetIndex = 2;
#endif

    private Vector3 pos;

    public AssetBundleWindow()
    {
        string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        dal = new AssetBundleDAL(xmlPath);
        m_List = dal.GetList();

        m_Dic = new Dictionary<string, bool>();

        for (int i = 0; i < m_List.Count; i++)
        {
            m_Dic[m_List[i].Key] = true;
        }
    }

    void OnGUI()
    {
        if (m_List == null) return;

        #region 按钮行
        GUILayout.BeginHorizontal("Box");

        tagIndex = EditorGUILayout.Popup(tagIndex, arraTag, GUILayout.Width(100));
        if(GUILayout.Button("select Tag", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTagCallback;
        }

        buildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(100));
        if (GUILayout.Button("select Target", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTargetCallback;
        }

        if (GUILayout.Button("build AssetBundle", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallback;
        }

        if (GUILayout.Button("Clear AssetBundle", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallback;
        }

        EditorGUILayout.Space();

        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("保存路径", GUILayout.Width(200));
        GUILayout.Label("版本", GUILayout.Width(100));
        GUILayout.Label("大小", GUILayout.Width(100));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        pos = EditorGUILayout.BeginScrollView(pos);
        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();


    }

    private void OnClearAssetBundleCallback()
    {
        Debug.LogError("Clear AssetBundle callback");
    }

    private void OnSelectTagCallback()
    {
        Debug.LogError("Tag callback");
    }

    private void OnSelectTargetCallback()
    {
        Debug.LogError("Target callback");
    }

    private void OnAssetBundleCallback()
    {
        Debug.LogError("AssetBundle callback");
    }
}
