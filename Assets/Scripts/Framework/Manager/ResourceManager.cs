using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace Framework
{
    public class ResourceManager : MonoBehaviour
    {
        internal class BundleInfo
        {
            public string AssetsName;
            public string BundleName;
            public List<string> Dependences;
        }

        internal class BundleData
        {
            public AssetBundle Bundle;

            //引用计数
            public int Count;

            public BundleData(AssetBundle ab)
            {
                Bundle = ab;
                Count = 1;
            }
        }

        // 存放Bundle信息的集合
        private Dictionary<string, BundleInfo> m_BundleInfos = new Dictionary<string, BundleInfo>();
        // 存放Bundle资源的集合
        private Dictionary<string, BundleData> m_AssetBundles = new Dictionary<string, BundleData>();

        public void ParseVersionFile()
        {
            // 版本文件的路径
            string url = Path.Combine(PathUtil.ReadWritePath, AppConst.FileListName);
            string[] data = File.ReadAllLines(url);

            // 解析文件信息
            for (int i = AppConst.FileListStartRow; i < data.Length; i++)
            {
                BundleInfo bundleInfo = new BundleInfo();
                string[] info = data[i].Split('|');
                bundleInfo.AssetsName = info[0];
                bundleInfo.BundleName = info[1];

                bundleInfo.Dependences = new List<string>(info.Length - AppConst.FileListDependenceStartCol);
                for (int j = AppConst.FileListDependenceStartCol; j < info.Length; j++)
                {
                    bundleInfo.Dependences.Add(info[j]);
                }
                m_BundleInfos.Add(bundleInfo.AssetsName, bundleInfo);

                if (info[0].IndexOf("LuaScripts") > 0)
                    Manager.Lua.LuaNames.Add(info[0]);

            }
        }

        IEnumerator LoadBundleAsync(string assetName, Action<UObject> action = null)
        {
            string bundleName = m_BundleInfos[assetName].BundleName;
            string bundlePath = Path.Combine(PathUtil.BundleResourcePath, bundleName);
            List<string> dependence = m_BundleInfos[assetName].Dependences;

            BundleData bundle = GetBundle(bundleName);
            if (bundle == null)
            {
                UObject obj = Manager.Pool.Spawn(AppConfig.AssetBundlePool, bundleName);
                if (obj != null)
                {
                    AssetBundle ab = obj as AssetBundle;
                    bundle = new BundleData(ab);
                }
                else
                {
                    AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundlePath);
                    yield return request;
                    bundle = new BundleData(request.assetBundle);
                }
                m_AssetBundles.Add(bundleName, bundle);
            }

            if (dependence != null && dependence.Count > 0)
            {
                for (int i = 0; i < dependence.Count; i++)
                {
                    yield return LoadBundleAsync(dependence[i]);
                }
            }

            if (assetName.EndsWith(".unity"))
            {
                action?.Invoke(null);
                yield break;
            }

            if (action == null)
            {
                yield break;
            }

            AssetBundleRequest bundleRequest = bundle.Bundle.LoadAssetAsync(assetName);
            yield return bundleRequest;

            action?.Invoke(bundleRequest?.asset);
        }

        private BundleData GetBundle(string bundleName)
        {
            if (m_AssetBundles.TryGetValue(bundleName, out BundleData bundle))
            {
                bundle.Count++;
                return bundle;
            }
            return null;
        }

        // 减去bundle和依赖的引用计数
        public void MinusBundleCount(string assetName)
        {
            string bundleName = m_BundleInfos[assetName].BundleName;

            MinusOneBundleCount(bundleName);
        }

        // 减去一个bundle的引用计数
        private void MinusOneBundleCount(string bundleName)
        {
            if (m_AssetBundles.TryGetValue(bundleName, out BundleData bundle))
            {
                if (bundle.Count > 0)
                {
                    bundle.Count--;
                }
                if (bundle.Count <= 0)
                {
                    Manager.Pool.UnSpawn(AppConfig.AssetBundlePool, bundleName, bundle.Bundle);
                    m_AssetBundles.Remove(bundleName);
                }
            }
        }

#if UNITY_EDITOR
        void EditorLoadAsset(string assetName, Action<UObject> action = null)
        {
            UObject obj = UnityEditor.AssetDatabase.LoadAssetAtPath(assetName, typeof(UObject));
            action?.Invoke(obj);
        }
#endif

        private void LoadAsset(string assetName, Action<UObject> action)
        {
#if UNITY_EDITOR
            if (AppConst.GameMode == GameMode.EditorMode)
                EditorLoadAsset(assetName, action);
            else
#endif
                StartCoroutine(LoadBundleAsync(assetName, action));
        }

        public void LoadUIPrefab(string assetName, Action<UObject> action = null)
        {
            LoadAsset(PathUtil.GetUIPrefabPath(assetName), action);
        }

        public void LoadLua(string assetName, Action<UObject> action = null)
        {
            LoadAsset(assetName, action);
        }

        internal void UnloadBundle(UObject obj)
        {
            AssetBundle ab = obj as AssetBundle;
            ab.Unload(true);
        }
    }
}