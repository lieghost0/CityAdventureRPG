using System.Collections;
using System.Collections.Generic;
using Battle;
using Define;
using Entity;
using Framework;
using UnityEngine;

namespace Managers
{
    public class SkillManager
    {
        public Dictionary<int, Skill> Skills = new Dictionary<int, Skill>();

        Creature Owner;

        public Skill NormalSkill;

        public SkillManager(Creature owner, int[] skills) 
        {
            this.Owner = owner;
            for (int i = 0; i < skills.Length; i++)
            {
                if (Manager.Data.Skills.TryGetValue(skills[i], out SkillDefine skillDefine))
                {
                    Skill skill = new Skill(this.Owner, skillDefine);
                    Skills.Add(skills[i], skill);
                    if(skillDefine.SkillType == SkillType.Normal)
                    {
                        NormalSkill = skill;
                    }
                }
            }
        }

        public void Update()
        {
            foreach (Skill skill in Skills.Values)
            {
                skill.Update();
            }
        }

        public Skill GetSkill(int skillId)
        {
            if(Skills.TryGetValue(skillId, out Skill skill))
            {
                return skill;
            }
            return null;
        }
    }
}