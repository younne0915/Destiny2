using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class SettingsWindow : EditorWindow {

    private List<MacorItem> m_List = new List<MacorItem>();

    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();

    private string m_Macor = null;

    public SettingsWindow()
    {
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);

        m_List.Clear();
        //m_List.Add(new MacorItem("DEBUG_MODEL", "调试模式", true, false));
        //m_List.Add(new MacorItem("DEBUG_LOG", "打印日志", true, false));
        //m_List.Add(new MacorItem("STAT_TD", "开启统计", false, true));

        m_List.Add(new MacorItem("DEBUG_MODEL", "DEBUG_MODEL", true, false));
        m_List.Add(new MacorItem("DEBUG_LOG", "DEBUG_LOG", true, false));
        m_List.Add(new MacorItem("STAT_TD", "STAT_TD", false, true));

        for (int i = 0; i < m_List.Count; i++)
        {
            if(!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(m_List[i].Name) != -1)
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
        }
        PlayerSettings.SetScriptingDefineSymbolsForGroup( BuildTargetGroup.Android, m_Macor);
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
