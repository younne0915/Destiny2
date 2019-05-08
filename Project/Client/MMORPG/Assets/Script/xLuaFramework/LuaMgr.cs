using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaMgr : SingletonInstance<LuaMgr>
{
    public static LuaEnv luaEnv;

    protected override void OnAwake()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString(string.Format("package.path = '{0}/?.lua'", Application.dataPath));
    }

    public void DoString(string str)
    {
        luaEnv.DoString(str);
    }
}
