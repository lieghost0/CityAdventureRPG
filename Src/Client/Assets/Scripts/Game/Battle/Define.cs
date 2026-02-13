using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public enum AttributeType
    {
        None = -1,
        /// <summary>
        /// 最大生命
        /// </summary>
        MaxHP = 0,
        /// <summary>
        /// 最大法力
        /// </summary>
        MaxMP = 1,
        /// <summary>
        /// 体质
        /// </summary>
        VIT = 2,
        /// <summary>
        /// 力量
        /// </summary>
        STR = 3,
        /// <summary>
        /// 智力
        /// </summary>
        INT = 4,
        /// <summary>
        /// 敏捷
        /// </summary>
        DEX = 5,
        /// <summary>
        /// 物理攻击
        /// </summary>
        AD = 6,
        /// <summary>
        /// 魔法攻击
        /// </summary>
        AP = 7,
        /// <summary>
        /// 物理防御
        /// </summary>
        DEF = 8,
        /// <summary>
        /// 魔法防御
        /// </summary>
        MDEF = 9,
        /// <summary>
        /// 攻击速度（行动速度）
        /// </summary>
        SPD = 10,

        Max
    }

    public enum DamageType
    {
        None = 0,
        AD,
        AP,
        Real,
        Heal
    }

    public enum SkillType
    {
        All = -1, 
        /// <summary>
        /// 普通攻击
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 主动技能
        /// </summary>
        Skill = 2,
        /// <summary>
        /// 被动技能
        /// </summary>
        Passive = 4
    }

    public enum SkillStatus
    {
        None = 0,
        // 释放中
        Casting = 1,
        Running = 2
    }

    public enum SkillResult
    {
        OK = 0,
        OutOfMp,
        CoolDown,
        Casting
    }
}