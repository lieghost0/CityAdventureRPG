using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using XLua.Cast;

namespace Framework
{
    public class MapDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int MapLevel { get; set; }
        public string Background { get; set; }
        public string Description { get; set; }

    }
}