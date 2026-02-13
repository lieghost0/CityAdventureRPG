using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Define;
using Framework;
using UnityEngine;

namespace Entity
{
    public class Enemy : Creature
    {
        public EnemyDefine define;

        public AIAgent AI;

        public Enemy(int id, EnemyDefine define) : base(id, define.Name)
        {
            this.define = define;
            base.InitSkills(define.SkillIds);
            this.AI = new AIAgent(this);
        }

        public override void DoAction()
        {
            this.AI.AI.DoAction();
        }

        internal Skill FindSkill(SkillType skillType)
        {
            Skill canCast = null;
            foreach (Skill skill in this.SkillMgr.Skills.Values)
            {
                if (skill.Define.SkillType != skillType) continue;
                if (skill.CanCast() == SkillResult.OK)
                {
                    canCast = skill;
                    break;
                }
            }
            return canCast;
        }
    }
}