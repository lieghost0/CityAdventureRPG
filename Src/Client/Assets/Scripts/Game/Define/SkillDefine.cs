using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Unity.VisualScripting;

namespace Define
{
    public class SkillDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string Icon { get; set; }
        public string Sound { get; set; }
        public string CastEffect { get; set; }
        public string HitEffect { get; set; }

        public SkillType SkillType { get; set; }
        public string CastTarget { get; set; }
        public int TargetCount { get; set; }
        public string ChooseType { get; set; }
        public float CastTime { get; set; }
        public float CD { get; set; }
        public int MPCost { get; set; }
        public List<int> Buff { get; set; }
        public float BasicDamage { get; set; }
        public List<float> DamageFators { get; set; }
        public List<int> FatorAttributeTypes { get; set; }
        public List<int> FatorDamageTypes { get; set; }
    }
}