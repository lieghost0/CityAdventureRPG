using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Entity;
using UnityEngine;

namespace AI
{
    public class AIBase
    {
        public Creature Target;
        public Enemy Owner;
        Skill normalSkill;

        public AIBase(Enemy owner)
        {
            Owner = owner;
            normalSkill = Owner.SkillMgr.NormalSkill;
        }

        internal void DoAction()
        {
            if (!TryCastSkill())
            {
                if (!TryCastNormalSkill())
                {
                    return;
                }
            }
        }

        private bool TryCastSkill()
        {
            Skill skill = Owner.FindSkill(SkillType.Skill);
            if (skill != null)
            {
                Owner.CastSkill(skill.Define.ID);
                return true;
            }
            return false;
        }

        private bool TryCastNormalSkill()
        {
            if (normalSkill != null)
            {
                Owner.CastSkill(normalSkill.Define.ID);
                return true;
            }
            return false;
        }
    }
}