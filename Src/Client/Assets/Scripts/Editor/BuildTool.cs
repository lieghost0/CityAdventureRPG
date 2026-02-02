using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Framework;
using UnityEditor;
using UnityEngine;

public class BuildTool : Editor
{
    [MenuItem("Tools/Build Windows Bundle")]
    static void BundleWindowsBuild()
    {
        Build(BuildTarget.StandaloneWindows, "Windows打包完成");
    }

    [MenuItem("Tools/Build Android Bundle")]
    static void BundleAndroidBuild()
    {
        Build(BuildTarget.Android, "Android打包完成");
    }

    [MenuItem("Tools/Build IOS Bundle")]
    static void BundleIOSBuild()
    {
        Build(BuildTarget.iOS, "IOS打包完成");
    }

    static void Build(BuildTarget target, string msg)
    {
        // AB包构建列表
        List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();

        // 文件信息列表
        List<string> bundleInfos = new List<string>();
        bundleInfos.Add("version:" + AppConst.version);
        // 获取需要打包的资源目录下所有文件下的所有文件，这里获取到的是绝对路径的文件名
        string[] files = Directory.GetFiles(PathUtil.BuildResourcesPath, "*", SearchOption.AllDirectories);
        List<string> logs = new List<string>();
        for(int i = 0; i < files.Length; i++)
        {
            // 忽略meta文件
            if (files[i].EndsWith(".meta"))
                continue;
            AssetBundleBuild assetBundle = new AssetBundleBuild();

            // 替换反斜杠，获得Unity中使用的标准路径
            string fileName = PathUtil.GetStandardPath(files[i]);
            logs.Add(fileName);

            // 获取相对路径
            string assetName = PathUtil.GetUnityPath(fileName);
            assetBundle.assetNames = new string[] { assetName };
            // 获取具体资源名
            string bundleName = fileName.Replace(PathUtil.BuildResourcesPath + "/", "").ToLower();
            assetBundle.assetBundleName = bundleName + ".ab";
            assetBundleBuilds.Add(assetBundle);

            // 添加文件和依赖信息
            List<string> dependenceInfo = GetDependence(assetName);
            string md5 = MD5Util.GetMD5Code(fileName);
            string bundleInfo = assetName + "|" + bundleName + ".ab" + "|" + md5;

            if(dependenceInfo.Count > 0)
                bundleInfo = bundleInfo + "|" + string.Join("|", dependenceInfo);

            bundleInfos.Add(bundleInfo);
        }

        if(Directory.Exists(PathUtil.BundleOutPath))
            Directory.Delete(PathUtil.BundleOutPath, true);
        Directory.CreateDirectory(PathUtil.BundleOutPath);

        // 构建AB包
        BuildPipeline.BuildAssetBundles(PathUtil.BundleOutPath, assetBundleBuilds.ToArray(), BuildAssetBundleOptions.None, target);

        File.WriteAllLines(PathUtil.BundleOutPath + "/" + AppConst.FileListName, bundleInfos);
        foreach (var log in logs)
        {
            Debug.Log(log);
        }

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("提示", msg, "确定");
    }

    static List<String> GetDependence(string curFile)
    {
        List<string> dependence = new List<string>();
        // 查询资源依赖
        string[] files = AssetDatabase.GetDependencies(curFile);
        // 过滤掉C#文件和自己
        dependence = files.Where(file => !file.EndsWith(".cs") && !file.Equals(curFile)).ToList();
        return dependence;
    }
}
