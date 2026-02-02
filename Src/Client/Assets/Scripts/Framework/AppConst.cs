using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public enum GameMode
    {
        EditorMode,
        PackageBundle,
        UpdateMode,
    }

    public enum GameEvent
    {
        GameInit = 10000,
        StartLua
    }

    public class AppConst
    {
        public const string BundleExtension = ".ab";
        public const string FileListName = "filelist.txt";
        public const int FileListStartRow = 1;
        public const int FileListDependenceStartCol = 3;
        public static GameMode GameMode = GameMode.EditorMode;
        public static bool OpenLog = true;
        // 热更资源的地址
        public const string ResourcesUrl = "192.168.1.6/AssetBundles";
        // 版本号
        public const string version = "0.0.1";
    }
}
