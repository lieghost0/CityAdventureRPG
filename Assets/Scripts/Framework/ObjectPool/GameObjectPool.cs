using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class GameObjectPool : PoolBase
    {
        public override Object Spawn(string name)
        {
            Object obj = base.Spawn(name);
            if (obj == null)
                return null;

            GameObject go = obj as GameObject;
            go.SetActive(false);
            return obj;
        }

        public override void UnSpawn(string name, Object obj)
        {
            GameObject go = obj as GameObject;
            go.SetActive(false);
            go.transform.SetParent(transform, false);
            base.UnSpawn(name, obj);
        }

        public override void Release()
        {
            foreach (PoolObject item in m_Objects)
            {
                // 单位变换比较，需将单位秒*10000000
                if (System.DateTime.Now.Ticks - m_LastReleaseTime > m_ReleaseTime * 10000000)
                {
                    Destroy(item.Object);
                    Manager.Resource.MinusBundleCount(item.Name);
                    m_Objects.Remove(item);
                    Release();
                    return;
                }
            }
        }
    }
}