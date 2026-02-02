using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using XLua.Cast;

namespace Framework
{
    [Flags]
    public enum EnemyType
    {
        None = 0,
        Small = 1 << 0,
        Medium = 1 << 1,
        Large = 1 << 2,
        Gient = 1 << 3,
        Insect = 1 << 4
    }
    public class EnemyDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public EnemyType[] EnemyTypes { get; set; }
        public string EnemySprite { get; set; }
        public string Description { get; set; }
        public string AI { get; set; }
        public string LootDrop { get; set; }
        public int InitLevel { get; set; }
        public float MaxHP { get; set; }
        public float MaxMP { get; set; }
        public float VIT { get; set; }
        public float STR { get; set; }
        public float INT { get; set; }
        public float DEX { get; set; }
        public float AD { get; set; }
        public float AP { get; set; }
        public float DEF { get; set; }
        public float MDEF { get; set; }
        public float SPD { get; set; }

    }
}