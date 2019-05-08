using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

[ExecuteInEditMode]
public class SettingsWindow : EditorWindow
{

    private List<MacorItem> m_List = new List<MacorItem>();

    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();

    private string m_Macor = null;

    public SettingsWindow()
    {
    }

    void OnEnable()
    {
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        m_List.Clear();
        //m_List.Add(new MacorItem("DEBUG_MODEL", "调试模式", true, false));
        //m_List.Add(new MacorItem("DEBUG_LOG", "打印日志", true, false));
        //m_List.Add(new MacorItem("STAT_TD", "开启统计", false, true));

        m_List.Add(new MacorItem("DEBUG_MODEL", "DEBUG_MODEL", true, false));
        m_List.Add(new MacorItem("DEBUG_LOG", "DEBUG_LOG", true, false));
        m_List.Add(new MacorItem("STAT_TD", "STAT_TD", false, true));
        m_List.Add(new MacorItem("DEBUG_ROLESTATE", "DEBUG_ROLESTATE", false, false));
        m_List.Add(new MacorItem("DISABLE_ASSETBUNDLE", "禁用AssetBundle", false, false));
        m_List.Add(new MacorItem("HOTFIX_ENABLE", "开启热补丁", false, true));


        for (int i = 0; i < m_List.Count; i++)
        {
            if (!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(m_List[i].Name) != -1)
            {
                m_Dic[m_List[i].Name] = true;
            }
            else
            {
                m_Dic[m_List[i].Name] = false;
            }
        }
    }

    void OnGUI()
    {
        if (m_List == null || m_List.Count == 0) return;
        for (int i = 0; i < m_List.Count; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Save", GUILayout.Width(100), GUILayout.Height(30)))
        {
            SaveMacor();
        }

        if (GUILayout.Button("Debug", GUILayout.Width(100), GUILayout.Height(30)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            SaveMacor();
        }

        if (GUILayout.Button("Release", GUILayout.Width(100), GUILayout.Height(30)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            }
            SaveMacor();
        }
        EditorGUILayout.EndHorizontal();
    }

    private void SaveMacor()
    {
        m_Macor = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macor += string.Format("{0};", item.Key);
            }

            if (item.Key.IndexOf("DISABLE_ASSETBUNDLE") > -1)
            {
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                for (int i = 0; i < scenes.Length; i++)
                {
                    if (scenes[i].path.IndexOf("download", System.StringComparison.InvariantCultureIgnoreCase) > -1)
                    {
                        scenes[i].enabled = item.Value;
                    }
                }
                EditorBuildSettings.scenes = scenes;
            }
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);

    }

    /// <summary>
    /// 宏项目
    /// </summary>
    public class MacorItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisplayName;

        public bool IsDebug;

        public bool IsRelease;

        public MacorItem(string name, string displayName, bool isDebug, bool isRelease)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.IsDebug = isDebug;
            this.IsRelease = isRelease;
        }
    }
}
