using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace Framework
{
    public class MD5Util
    {
        public static string GetMD5Code(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    MD5 md5 = MD5.Create();
                    byte[] hashBytes = md5.ComputeHash(fs);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
            catch (Exception e)
            {
                LogUtil.Exception(e);
                return "";
            }
        }
    }
}
