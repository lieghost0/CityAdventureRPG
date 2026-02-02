using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class AssetPool : PoolBase
    {
        public override void Release()
        {
            foreach (PoolObject item in m_Objects)
            {
                // 单位变换比较，需将单位秒*10000000
                if (System.DateTime.Now.Ticks - m_LastReleaseTime > m_ReleaseTime * 10000000)
                {
                    Manager.Resource.UnloadBundle(item.Object);
                    m_Objects.Remove(item);
                    Release();
                    return;
                }
            }
        }
    }
}