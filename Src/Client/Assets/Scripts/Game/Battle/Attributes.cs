using System.Collections;
using System.Collections.Generic;
using Define;
using UnityEngine;

namespace Battle
{
    public class Attributes
    {
        AttributeData Initial = new AttributeData();
        AttributeData Growth = new AttributeData();
        AttributeData Equip = new AttributeData();

        public AttributeData Basic;
        public AttributeData Buff;
        public AttributeData Final;

        int level;

        private int hp;

        public float HP
        {
            get { return hp; }
            set { hp = (int)Mathf.Min(MaxHP, value); }
        }

        private int mp;
        public float MP
        {
            get { return mp; }
            set { mp = (int)Mathf.Min(MaxMP, value); }
        }

        /// <summary>
        /// 最大生命
        /// </summary>
        public float MaxHP { get { return this.Final.MaxHP; } }
        /// <summary>
        /// 最大法力
        /// </summary>
        public float MaxMP { get { return this.Final.MaxMP; } }
        /// <summary>
        /// 体质
        /// </summary>
        public float VIT { get { return this.Final.VIT; } }
        /// <summary>
        /// 力量
        /// </summary>
        public float STR { get { return this.Final.STR; } }
        /// <summary>
        /// 智力
        /// </summary>
        public float INT { get { return this.Final.INT; } }
        /// <summary>
        /// 敏捷
        /// </summary>
        public float DEX { get { return this.Final.DEX; } }
        /// <summary>
        /// 物理攻击
        /// </summary>
        public float AD { get { return this.Final.AD; } }
        /// <summary>
        /// 魔法攻击
        /// </summary>
        public float AP { get { return this.Final.AP; } }
        /// <summary>
        /// 物理防御
        /// </summary>
        public float DEF { get { return this.Final.DEF; } }
        /// <summary>
        /// 魔法防御
        /// </summary>
        public float MDEF { get { return this.Final.MDEF; } }
        /// <summary>
        /// 攻击速度
        /// </summary>
        public float SPD { get { return this.Final.SPD; } }

        public void InitEnemy()
        {
            InitBasicAttribute();
        }

        public void InitPlayer()
        {

        }

        private void InitBasicAttribute()
        {

        }

        private void LoadPlayerInitAttribute(AttributeData attr)
        {

        }

        private void LoadEnemyInitAttribute(AttributeData attr, EnemyDefine define)
        {
            attr.MaxHP = define.MaxHP;
            attr.MaxMP = define.MaxMP;
            attr.VIT = define.VIT;
            attr.STR = define.STR;
            attr.INT = define.INT;
            attr.DEX = define.DEX;
            attr.AD = define.AD;
            attr.AP = define.AP;
            attr.DEF = define.DEF;
            attr.MDEF = define.MDEF;
            attr.SPD = define.SPD;
        }

        private void LoadGrowthAttribute(AttributeData attr)
        {

        }

        private void LoadEquipAttribute(AttributeData attr, List<EquipDefine> equipDefines)
        {

        }
    }
}