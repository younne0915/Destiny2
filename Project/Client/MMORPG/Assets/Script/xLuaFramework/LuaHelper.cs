using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class LuaHelper : Singleton<LuaHelper>
{
    public UISceneCtrl uiSceneCtrl
    {
        get { return UISceneCtrl.Instance; }
    }

    public UIViewUtil uiViewUtil
    {
        get { return UIViewUtil.Instance; }
    }

    public LoaderMgr loaderMgr
    {
        get { return LoaderMgr.Instance; }
    }

    public GameDataTableToLua GetData(string name)
    {
        GameDataTableToLua data = new GameDataTableToLua();
#if DISABLE_ASSETBUNDLE
        string path = string.Format("{0}/Download/DataTable/{1}", Application.dataPath, name);
#else
        string path = string.Format("{0}/Download/DataTable/{1}", Application.persistentDataPath, name);
#endif
        using (GameDataTableParser parser = new GameDataTableParser(path))
        {
            data.Row = parser.Row;
            data.Column = parser.Column;

            data.Data = new string[parser.Row][];

            for (int i = 0; i < parser.Row; i++)
            {
                string[] arr = new string[parser.Column];
                for (int j = 0; j < parser.Column; j++)
                {
                    arr[j] = parser.GameData[i, j];
                }
                data.Data[i] = arr;
            }
        }
        return data;
    }

    public MMO_MemoryStream CreateMemoryStream()
    {
        return new MMO_MemoryStream();
    }

    public MMO_MemoryStream CreateMemoryStream(byte[] buffer)
    {
        return new MMO_MemoryStream(buffer);
    }

    public void SendProto(byte[] buffer)
    {
        NetWorkSocket.Instance.SendMsg(buffer);
    }

    public void AddListener(ushort protoCode, SocketDispatcher.OnActionHandler listener)
    {
        SocketDispatcher.Instance.AddEventHandler(protoCode, listener);
    }

    public void RemoveEventHandler(ushort protoCode, SocketDispatcher.OnActionHandler listener)
    {
        SocketDispatcher.Instance.RemoveEventHandler(protoCode, listener);
    }
}
