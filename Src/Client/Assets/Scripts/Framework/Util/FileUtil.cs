using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Framework
{
    public class FileUtil
    {
        /// <summary>
        /// 检测文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExists(string path)
        {
            FileInfo file = new FileInfo(path);
            return file.Exists;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        public static void WriteFile(string path, byte[] data)
        {
            // 获取标准路径
            path = PathUtil.GetStandardPath(path);
            // 文件夹的路径
            string dir = path.Substring(0, path.LastIndexOf('/'));
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            FileInfo file = new FileInfo(path);
            if(file.Exists)
                file.Delete();
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                }
            }
            catch (IOException e)
            {
                LogUtil.Exception(e);
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string ReadFileText(string path)
        {
            if (!File.Exists(path))
                return "";
            return File.ReadAllText(path);
        }
    }
}
