using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class PoolObject
    {
        //具体对象
        public Object Object;

        // 对象名字
        public string Name;

        // 最后一次使用的时间
        public System.DateTime LastUseTime;

        public PoolObject(string name, Object obj)
        {
            Name = name;
            Object = obj;
            LastUseTime = System.DateTime.Now;
        }
    }
}