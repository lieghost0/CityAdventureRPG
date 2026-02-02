using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace Framework
{
    public class LogUtil
    {
        [LuaCallCSharp]
        public static void Info(string msg)
        {
            if (!AppConst.OpenLog)
                return;
            Debug.Log(msg);
        }

        [LuaCallCSharp]
        public static void Warning(string msg)
        {
            if (!AppConst.OpenLog)
                return;
            Debug.LogWarning(msg);
        }

        [LuaCallCSharp]
        public static void Error(string msg)
        {
            if (!AppConst.OpenLog)
                return;
            Debug.LogError(msg);
        }

        public static void Exception(Exception e)
        {
            if (!AppConst.OpenLog)
                return;
            Debug.LogException(e);
        }
    }
}