using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AttributeData
    {
        public float[] Data = new float[(int)AttributeType.Max];
        public float MaxHP { get { return Data[(int)AttributeType.MaxHP]; } set { Data[(int)AttributeType.MaxHP] = value; } }
        public float MaxMP { get { return Data[(int)AttributeType.MaxMP]; } set { Data[(int)AttributeType.MaxMP] = value; } }
        public float VIT { get { return Data[(int)AttributeType.VIT]; } set { Data[(int)AttributeType.VIT] = value; } }
        public float STR { get { return Data[(int)AttributeType.STR]; } set { Data[(int)AttributeType.STR] = value; } }
        public float INT { get { return Data[(int)AttributeType.INT]; } set { Data[(int)AttributeType.INT] = value; } }
        public float DEX { get { return Data[(int)AttributeType.DEX]; } set { Data[(int)AttributeType.DEX] = value; } }
        public float AD { get { return Data[(int)AttributeType.AD]; } set { Data[(int)AttributeType.AD] = value; } }
        public float AP { get { return Data[(int)AttributeType.AP]; } set { Data[(int)AttributeType.AP] = value; } }
        public float DEF { get { return Data[(int)AttributeType.DEF]; } set { Data[(int)AttributeType.DEF] = value; } }
        public float MDEF { get { return Data[(int)AttributeType.MDEF]; } set { Data[(int)AttributeType.MDEF] = value; } }
        public float SPD { get { return Data[(int)AttributeType.SPD]; } set { Data[(int)AttributeType.SPD] = value; } }

        public void Reset()
        {
            for(int i = 0; i < Data.Length; i++)
            {
                Data[i] = 0;
            }
        }
    }
}