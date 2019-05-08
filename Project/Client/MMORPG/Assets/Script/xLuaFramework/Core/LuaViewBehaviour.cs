using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaViewBehaviour : MonoBehaviour {

    [CSharpCallLua]
    public delegate void delLuaAwake(GameObject obj);
    private delLuaAwake luaAwake;

    [CSharpCallLua]
    public delegate void delLuaStart();
    private delLuaStart luaStart;

    [CSharpCallLua]
    public delegate void delLuaUpdate();
    private delLuaUpdate luaUpdate;

    [CSharpCallLua]
    public delegate void delLuaOnDestroy();
    private delLuaOnDestroy luaOnDestroy;

    private LuaTable scriptEnv;
    private LuaEnv luaEnv;

    public string Tag;

    void Awake()
    {
        luaEnv = LuaMgr.luaEnv; //此处要从LuaMgr上获取 全局只有一个

        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        string prefabName = name;
        if (prefabName.Contains("(Clone)"))
        {
            prefabName = prefabName.Split(new string[] { "(Clone)" }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        prefabName = prefabName.Replace("pan_", "");

        luaAwake = scriptEnv.GetInPath<delLuaAwake>(prefabName + ".awake");
        luaStart = scriptEnv.GetInPath<delLuaStart>(prefabName + ".start");
        luaUpdate = scriptEnv.GetInPath<delLuaUpdate>(prefabName + ".update");
        luaOnDestroy = scriptEnv.GetInPath<delLuaOnDestroy>(prefabName + ".ondestroy");

        scriptEnv.Set("self", this);
        if (luaAwake != null)
        {
            luaAwake(gameObject);
        }
    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    void Destroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        luaAwake = null;
    }
}
