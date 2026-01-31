using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Framework
{
    public class PathUtil
    {
        // 根目录 /Assets
        public static readonly string AssetsPath = Application.dataPath;

        // 需要打Bundle的目录
        public static readonly string BuildResourcesPath = AssetsPath + "/BuildResources";

        // 只读目录
        public static readonly string ReadPath = Application.streamingAssetsPath;

        // Bundle包的输出目录
        public static readonly string BundleOutPath = ReadPath;

        // 可读写目录
        public static readonly string ReadWritePath = Application.persistentDataPath;

        // lua路径
        public static readonly string LuaPath = "Assets/BuildResources/LuaScripts";

        // UI路径
        public static readonly string UIPath = "Assets/BuildResources/UI";

        // Audio路径
        public static readonly string AudioPath = "Assets/BuildResources/Audio";

        // Effect路径
        public static readonly string EffectPath = "Assets/BuildResources/Effect";

        // Model路径
        public static readonly string ModelPath = "Assets/BuildResources/Model";

        // Sprite路径
        public static readonly string SpritePath = "Assets/BuildResources/Sprites";

        // bundle资源路径
        public static string BundleResourcePath
        {
            get
            {
                if (AppConst.GameMode == GameMode.UpdateMode)
                {
                    return ReadWritePath;
                }
                return ReadPath;
            }
        }

        /// <summary>
        /// 获取Unity的相对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetUnityPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            return path.Substring(path.IndexOf("Assets"));
        }

        /// <summary>
        /// 获取标准路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetStandardPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            return path.Trim().Replace("\\", "/");
        }

        /// <summary>
        /// 获取Lua资源
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetLuaPath(string name)
        {
            return string.Format("Assets/BuildResources/LuaScripts/{0}.lua.txt", name);
        }

        /// <summary>
        /// 获取UI预制体资源
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetUIPrefabPath(string name)
        {
            return string.Format(Path.Combine(UIPath, "Prefabs", "{0}.prefab"), name);
        }

        /// <summary>
        /// 获取音乐资源(需后缀名)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetMusicPath(string name)
        {
            return string.Format(Path.Combine(AudioPath, "Music", "{0}"), name);
        }

        /// <summary>
        /// 获取音效资源(需后缀名)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSoundPath(string name)
        {
            return string.Format(Path.Combine(AudioPath, "Sound", "{0}"), name);
        }

        /// <summary>
        /// 获取特效预制体资源
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetEffectPrefabPath(string name)
        {
            return string.Format(Path.Combine(EffectPath, "Prefabs", "{0}.prefab"), name);
        }

        /// <summary>
        /// 获取模型预制体资源
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetModelPrefabPath(string name)
        {
            return string.Format(Path.Combine(ModelPath, "Prefabs", "{0}.prefab"), name);
        }

        /// <summary>
        /// 获取精灵资源(需后缀名)
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetSpritePath(string name)
        {
            return string.Format(Path.Combine(SpritePath, "{0}"), name);
        }
    }
}
