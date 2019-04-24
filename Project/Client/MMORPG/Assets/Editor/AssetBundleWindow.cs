using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

[ExecuteInEditMode]
public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_List;
    private Dictionary<string, bool> m_Dic;

    private string[] arraTag = { "All", "Scene", "Role", "Effect", "Audio", "UI", "None" };
    private int tagIndex = 0;
    private int selectTagIndex = -1;

    private string[] arrBuildTarget = { "Windows", "Android", "iOS" };

    private int selectBuildTargetIndex = -1;
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

    private void OnEnable()
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

        selectTagIndex = EditorGUILayout.Popup(tagIndex, arraTag, GUILayout.Width(150));
        if(selectTagIndex != tagIndex)
        {
            tagIndex = selectTagIndex;
            EditorApplication.delayCall = OnSelectTagCallback;
        }

        selectBuildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(150));
        if(selectBuildTargetIndex != buildTargetIndex)
        {
            buildTargetIndex = selectBuildTargetIndex;
            EditorApplication.delayCall = OnSelectTargetCallback;
        }

        if (GUILayout.Button("保存设置", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnSaveAssetBundleCallBack;
        }

        if (GUILayout.Button("build AssetBundle", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnAssetBundleCallback;
        }

        if (GUILayout.Button("Clear AssetBundle", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallback;
        }

        if (GUILayout.Button("拷贝数据表", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCopyDataTableCallBack;
        }

        if (GUILayout.Button("生成版本文件", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCreateVersionFileCallBack;
        }

        if (GUILayout.Button("清除DownloadLession名称", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnClearAssetbundleName;
        }

        if (GUILayout.Button("一键打包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnStepBundleCallback;
        }

        EditorGUILayout.Space();

        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("Name");
        GUILayout.Label("Tag", GUILayout.Width(200));
        GUILayout.Label("IsFolder", GUILayout.Width(200));
        GUILayout.Label("IsFirstData", GUILayout.Width(100));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        pos = EditorGUILayout.BeginScrollView(pos);
        for (int i = 0; i < m_List.Count; i++)
        {
            AssetBundleEntity entity = m_List[i];

            GUILayout.BeginHorizontal("box");

            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(200));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();

            foreach (var path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();

    }

    private void OnStepBundleCallback()
    {
        OnClearAssetBundleCallback();
        OnSaveAssetBundleCallBack();
        OnAssetBundleCallback();
        OnCopyDataTableCallBack();
        OnCreateVersionFileCallBack();
    }

    private void OnClearAssetBundleCallback()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        Debug.LogError("Clear AssetBundle callback");
    }

    private void OnSelectTagCallback()
    {
        switch (tagIndex)
        {
            case 0://全选
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = true;
                }
                break;
            case 1://Scene
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 2:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 3:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 4:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 5:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("UI", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 6:
                foreach (AssetBundleEntity entity in m_List)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
        Debug.Log(string.Format("select tag is {0}", arraTag[tagIndex]));
    }

    private void OnSelectTargetCallback()
    {
        switch (buildTargetIndex)
        {
            case 0:
                target = BuildTarget.StandaloneWindows;
                break;
            case 1:
                target = BuildTarget.Android;
                break;
            case 2:
                target = BuildTarget.iOS;
                break;
        }
        Debug.Log(string.Format("select BuildTarget is {0}", arrBuildTarget[buildTargetIndex]));
    }

    /// <summary>
    /// 保存设置
    /// </summary>
    private void OnSaveAssetBundleCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> lst = new List<AssetBundleEntity>();

        foreach (AssetBundleEntity entity in m_List)
        {
            if (m_Dic[entity.Key])
            {
                entity.IsChecked = true;
                lst.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                lst.Add(entity);
            }
        }

        //循环设置文件夹包括子文件里边的项
        for (int i = 0; i < lst.Count; i++)
        {
            EditorUtility.DisplayProgressBar("设置AssetBundle名称", "正在设置AssetBundle名称中...", 1f * i / lst.Count);
            AssetBundleEntity entity = lst[i];//取到一个节点
            if (entity.IsFolder)
            {
                //如果这个节点配置的是一个文件夹，那么需要遍历文件夹
                //需要把路变成绝对路径
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                }
                SaveFolderSettings(folderArr, !entity.IsChecked);
            }
            else
            {
                //如果不是文件夹 只需要设置里边的项
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                    SaveFileSetting(folderArr[j], !entity.IsChecked);
                }
            }
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.RemoveUnusedAssetBundleNames();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("保存设置完毕");
    }

    private void SaveFolderSettings(string[] folderArr, bool isSetNull)
    {
        foreach (string folderPath in folderArr)
        {
            //1.先看这个文件夹下的文件
            string[] arrFile = Directory.GetFiles(folderPath); //文件夹下的文件

            //2.对文件进行设置
            foreach (string filePath in arrFile)
            {
                Debug.Log("filePath=" + filePath);
                //进行设置
                SaveFileSetting(filePath, isSetNull);
            }

            //3.看这个文件夹下的子文件夹
            string[] arrFolder = Directory.GetDirectories(folderPath);
            SaveFolderSettings(arrFolder, isSetNull);
        }
    }

    private void SaveFileSetting(string filePath, bool isSetNull)
    {
        FileInfo file = new FileInfo(filePath);
        if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
        {
            //Debug.Log("filePath=" + filePath);
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);

            //路径
            string newPath = filePath.Substring(index);
            Debug.Log("newPath=" + newPath);

            //文件名
            string fileName = newPath.Replace("Assets/", "").Replace(file.Extension, "");

            //后缀
            string variant = file.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle";

            AssetImporter import = AssetImporter.GetAtPath(newPath);
            import.SetAssetBundleNameAndVariant(fileName, variant);

            if (isSetNull)
            {
                import.SetAssetBundleNameAndVariant(null, null);
            }
            import.SaveAndReimport();
        }
    }

    private void OnAssetBundleCallback()
    {
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];

        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }

        BuildPipeline.BuildAssetBundles(toPath, BuildAssetBundleOptions.None, target);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.LogError("打包成功");
    }

    /// <summary>
    /// 拷贝数据表
    /// </summary>
    private void OnCopyDataTableCallBack()
    {
        string fromPath = Application.dataPath + "/Download/DataTable";
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + "/Download/datatable";
        IOUtil.CopyDirectory(fromPath, toPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("拷贝数据表完毕");
    }

    /// <summary>
    /// 生成版本文件
    /// </summary>
    private void OnCreateVersionFileCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string strVersionFilePath = path + "/VersionFile.txt"; //版本文件路径

        //如果版本文件存在 则删除
        IOUtil.DeleteFile(strVersionFilePath);

        StringBuilder sbContent = new StringBuilder();

        DirectoryInfo directory = new DirectoryInfo(path);

        //拿到文件夹下所有文件
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];
            string fullName = file.FullName; //全名 包含路径扩展名

            //相对路径
            string name = fullName.Substring(fullName.IndexOf(arrBuildTarget[buildTargetIndex]) + arrBuildTarget[buildTargetIndex].Length + 1);
            //if (name.Equals(arrBuildTarget[buildTargetIndex], StringComparison.CurrentCultureIgnoreCase)) continue;

            string md5 = EncryptUtil.GetFileMD5(fullName); //文件的MD5
            if (md5 == null) continue;

            string size = Math.Ceiling(file.Length / 1024f).ToString(); //文件大小

            bool isFirstData = true; //是否初始数据
            bool isBreak = false;

            for (int j = 0; j < m_List.Count; j++)
            {
                foreach (string xmlPath in m_List[j].PathList)
                {
                    string tempPath = xmlPath;
                    if (xmlPath.IndexOf(".") != -1)
                    {
                        tempPath = xmlPath.Substring(0, xmlPath.IndexOf("."));
                    }
                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        isFirstData = m_List[j].IsFirstData;
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak) break;
            }

            if (name.IndexOf("DataTable") != -1)
            {
                isFirstData = true;
            }

            string strLine = string.Format("{0} {1} {2} {3}", name, md5, size, isFirstData ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("创建版本文件成功");
    }

    private void OnClearAssetbundleName()
    {
        //string folderPath = Application.dataPath + "/DownloadLesson";
        string folderPath = Application.dataPath + "/";

        string[] arrFolder = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);
        for (int i = 0; i < arrFolder.Length; i++)
        {
            EditorUtility.DisplayProgressBar("OnClearAssetbundleName", "正在OnClearAssetbundleName中...", 1f * i / arrFolder.Length);
            FileInfo file = new FileInfo(arrFolder[i]);
            if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
            {
                //Debug.Log("filePath=" + filePath);
                int index = arrFolder[i].IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
                if (index > -1)
                {
                    //路径
                    string newPath = arrFolder[i].Substring(index);
                    Debug.Log("newPath=" + newPath);

                    AssetImporter import = AssetImporter.GetAtPath(newPath);
                    if (!string.IsNullOrEmpty(import.assetBundleName) || !string.IsNullOrEmpty(import.assetBundleVariant))
                    {
                        import.SetAssetBundleNameAndVariant(null, null);
                        import.SaveAndReimport();
                    }
                }
                else
                {
                    Debug.Log("arrFolder[i] =" + arrFolder[i]);
                }
            }
        }
        EditorUtility.ClearProgressBar();

        string[] dirArr = Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories);
        for (int i = 0; i < dirArr.Length; i++)
        {
            EditorUtility.DisplayProgressBar("OnClearAssetbundleName", "正在清理含有bundle name的文件夹中...", 1f * i / dirArr.Length);
            int index = dirArr[i].IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
            if (index > -1)
            {
                //路径
                string newPath = dirArr[i].Substring(index);
                Debug.Log("newPath=" + newPath);

                AssetImporter import = AssetImporter.GetAtPath(newPath);
                if (!string.IsNullOrEmpty(import.assetBundleName) || !string.IsNullOrEmpty(import.assetBundleVariant))
                {
                    import.SetAssetBundleNameAndVariant(null, null);
                    import.SaveAndReimport();
                }
            }
            else
            {
                Debug.Log("dirArr[i] =" + dirArr[i]);
            }
        }
        EditorUtility.ClearProgressBar();

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("清除DownloadLession下面的AsstbundleName完毕");
    }
}
