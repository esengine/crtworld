using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class LuaManager : MonoBehaviour
{
    private LuaEnv _luaEnv;

    void Awake()
    {
        _luaEnv = new LuaEnv();
#if UNITY_EDITOR
        _luaEnv.AddLoader(SimulateLuaLoad);
#endif

        // _luaEnv.DoString("require 'framework.init'");
    }

    private byte[] SimulateLuaLoad(ref string filePath) {
        filePath = filePath.Replace(".", "/");
        var path = "Assets/Lua/" + filePath + ".lua.txt";
        if (File.Exists(path)) {
            return Encoding.UTF8.GetBytes(File.ReadAllText(path));
        }

        return null;
    }

    void Update()
    {
        _luaEnv.Tick();
    }

    void OnDestroy()
    {
        _luaEnv.Dispose();
    }
}
