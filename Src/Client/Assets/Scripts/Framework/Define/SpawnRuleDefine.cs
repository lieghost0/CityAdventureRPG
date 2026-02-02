using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using XLua.Cast;

namespace Framework
{
    public class SpawnRuleDefine
    {
        public int ID { get; set; }
        public int MapID { get; set; }
        public int[] EnemyIDs { get; set; }
    }
}