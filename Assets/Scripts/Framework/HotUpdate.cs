using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using static Framework.HotUpdate;

namespace Framework
{
    public class HotUpdate : MonoBehaviour
    {
        byte[] m_ReadPathFileListData;
        byte[] m_ServerFileListData;
        internal class FileListMap
        {
            public string version;
            public List<DownFileInfo> downFileInfos;
        }
        internal class DownFileInfo
        {
            public string url;
            public string fileName;
            public string md5;
            public DownloadHandler fileData;
            public bool needDownload = true;
        }
        // 下载文件数量
        int m_DownloadCount;

        /// <summary>
        /// 下载单个文件
        /// </summary>
        /// <param name="info"></param>
        /// <param name="complete"></param>
        /// <returns></returns>
        IEnumerator DownloadFile(DownFileInfo info, Action<DownFileInfo> complete)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(info.url);
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                LogUtil.Error(string.Format("下载文件出错：result{0} url:{1} error:{2}", webRequest.result, info.url, webRequest.error));
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
            info.fileData = webRequest.downloadHandler;
            complete?.Invoke(info);
            webRequest.Dispose();
        }

        /// <summary>
        /// 批量下载文件
        /// </summary>
        /// <param name="infos"></param>
        /// <param name="complete"></param>
        /// <param name="DownloadAllComplete"></param>
        /// <returns></returns>
        IEnumerator DownloadFiles(List<DownFileInfo> infos, Action<DownFileInfo> complete, Action DownloadAllComplete)
        {
            foreach (DownFileInfo info in infos)
            {
                yield return DownloadFile(info, complete);
            }
            DownloadAllComplete?.Invoke();
        }

        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="fileData"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private FileListMap GetFileList(string fileData, string path)
        {
            string content = fileData.Trim().Replace("\r", "");
            string[] files = content.Split('\n');
            FileListMap fileListMap = new FileListMap();
            if (files.Length > 0)
            {
                string[] v = files[0].Split(":");
                if (v.Length >= 2)
                    fileListMap.version = files[0].Split(":")[1];

                List<DownFileInfo> downFileInfos = new List<DownFileInfo>(files.Length);
                for (int i = AppConst.FileListStartRow; i < files.Length; i++)
                {
                    string[] info = files[i].Split("|");
                    DownFileInfo fileInfo = new DownFileInfo();
                    fileInfo.fileName = info[1];
                    fileInfo.md5 = info[2];
                    fileInfo.url = Path.Combine(path, info[1]);
                    downFileInfos.Add(fileInfo);
                }
                fileListMap.downFileInfos = downFileInfos;
            }
            return fileListMap;
        }

        GameObject gameLoadingObj;
        GameLoadingUI gameLoadingUI;
        private void Start()
        {
            GameObject go = Resources.Load<GameObject>("GameLoadingUI");
            gameLoadingObj = Instantiate(go, this.transform);
            gameLoadingUI = gameLoadingObj.GetComponent<GameLoadingUI>();
            // 检查是否初次安装
            if (IsFirstInstall())
            {
                // 释放资源
                ReleaseResources();
            }
            else
            {
                // 检查更新
                CheckUpdate();
            }
        }

        /// <summary>
        /// 检查是否初次安装
        /// </summary>
        /// <returns></returns>
        private bool IsFirstInstall()
        {
            // 判断只读目录是否存在版本文件
            bool isExistsReadPath = FileUtil.IsExists(Path.Combine(PathUtil.ReadPath, AppConst.FileListName));
            // 判断可读写目录是否存在版本文件
            bool isExistsReadWritePath = FileUtil.IsExists(Path.Combine(PathUtil.ReadWritePath, AppConst.FileListName));

            return isExistsReadPath && !isExistsReadWritePath;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        private void ReleaseResources()
        {
            m_DownloadCount = 0;
            string url = Path.Combine(PathUtil.ReadPath, AppConst.FileListName);
            DownFileInfo info = new DownFileInfo();
            info.url = url;
            StartCoroutine(DownloadFile(info, OnDownloadReadPathFileListComplete));
        }

        /// <summary>
        /// 获取只读包中的资源，也就是从下载的安装包中进行下载
        /// </summary>
        /// <param name="file"></param>
        private void OnDownloadReadPathFileListComplete(DownFileInfo file)
        {
            m_ReadPathFileListData = file.fileData.data;
            FileListMap fileListMap = GetFileList(file.fileData.text, PathUtil.ReadPath);
            StartCoroutine(DownloadFiles(fileListMap.downFileInfos, OnReleaseFileComplete, OnReleaseAllFileComplete));
            gameLoadingUI.InitProcess(fileListMap.downFileInfos.Count, "正在释放资源，不消耗流量......");
        }

        /// <summary>
        /// 释放单个文件完成时处理
        /// </summary>
        /// <param name="fileInfo"></param>
        private void OnReleaseFileComplete(DownFileInfo fileInfo)
        {
            string writeFile = Path.Combine(PathUtil.ReadWritePath, fileInfo.fileName);
            FileUtil.WriteFile(writeFile, fileInfo.fileData.data);
            m_DownloadCount++;
            // --释放文件完成，更新加载页面进度
            gameLoadingUI.UpdateProcess(m_DownloadCount);
        }

        /// <summary>
        /// 释放所有文件完成时处理（最后将版本文件写入可读写目录）
        /// </summary>
        private void OnReleaseAllFileComplete()
        {
            FileUtil.WriteFile(Path.Combine(PathUtil.ReadWritePath, AppConst.FileListName), m_ReadPathFileListData);
            CheckUpdate();
        }

        private void CheckUpdate()
        {
            string url = Path.Combine(AppConst.ResourcesUrl, AppConst.FileListName);
            DownFileInfo info = new DownFileInfo();
            info.url = url;
            StartCoroutine(DownloadFile(info, OnDownloadServerFileListComplete));
        }

        private void OnDownloadServerFileListComplete(DownFileInfo file)
        {
            m_DownloadCount = 0;
            m_ServerFileListData = file.fileData.data;
            FileListMap fileListMap = GetFileList(file.fileData.text, AppConst.ResourcesUrl);
            List<DownFileInfo> downListFiles = new List<DownFileInfo>();

            CheckFileList(fileListMap);
            downListFiles = fileListMap.downFileInfos.Where(file => file.needDownload).ToList();

            if (downListFiles.Count > 0)
            {
                StartCoroutine(DownloadFiles(downListFiles, OnUpdateFileComplete, OnUpdateAllFileComplete));
                gameLoadingUI.InitProcess(downListFiles.Count, "正在下载资源......");
            }
            else
            {
                // 进入游戏
                EnterGame();
            }
        }

        private void CheckFileList(FileListMap serverFileListMap)
        {
            string localFileList = FileUtil.ReadFileText(Path.Combine(PathUtil.ReadWritePath, AppConst.FileListName));
            FileListMap localFileListMap = GetFileList(localFileList, PathUtil.ReadWritePath);
            // 比较版本号
            string[] localVersion = localFileListMap.version.Split('.');
            string[] serverVersion = serverFileListMap.version.Split('.');
            bool isUpdate = false;
            int i = 0;
            while (i < localVersion.Length && i < serverVersion.Length)
            {
                int localV = int.Parse(localVersion[i]);
                int serverV = int.Parse(serverVersion[i]);
                if (localV < serverV)
                {
                    isUpdate = true;
                    break;
                }
                i++;
            }
            if (!isUpdate && i == localVersion.Length && i < serverVersion.Length)
                isUpdate = true;

            if (isUpdate)
            {
                List<DownFileInfo> serverFileInfos = serverFileListMap.downFileInfos;
                Dictionary<string, DownFileInfo> localFileInfoDic = new Dictionary<string, DownFileInfo>(localFileListMap.downFileInfos.Count);
                foreach (DownFileInfo info in localFileListMap.downFileInfos)
                {
                    localFileInfoDic.Add(info.fileName, info);
                }
                foreach (DownFileInfo serverInfo in serverFileInfos)
                {
                    if (localFileInfoDic.TryGetValue(serverInfo.fileName, out DownFileInfo localInfo))
                    {
                        if (string.Equals(localInfo.md5, serverInfo.md5))
                        {
                            serverInfo.needDownload = false;
                        }
                    }
                }
            }
            else
            {
                foreach (DownFileInfo serverInfo in serverFileListMap.downFileInfos)
                {
                    serverInfo.needDownload = false;
                }
            }
        }

        private void OnUpdateFileComplete(DownFileInfo file)
        {
            string writeFile = Path.Combine(PathUtil.ReadWritePath, file.fileName);
            FileUtil.WriteFile(writeFile, file.fileData.data);
            m_DownloadCount++;
            gameLoadingUI.UpdateProcess(m_DownloadCount);
        }

        private void OnUpdateAllFileComplete()
        {
            FileUtil.WriteFile(Path.Combine(PathUtil.ReadWritePath, AppConst.FileListName), m_ServerFileListData);
            // --更新进度条显示，进入游戏
            EnterGame();
            gameLoadingUI.InitProcess(0, "游戏载入中......");
        }

        private void EnterGame()
        {
            Manager.Event.Fire((int)GameEvent.GameInit);
            Destroy(gameLoadingObj);
        }
    }
}