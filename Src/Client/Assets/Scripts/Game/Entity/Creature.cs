using System;
using System.Collections;
using System.Collections.Generic;
using Battle;
using Framework;
using JetBrains.Annotations;
using Managers;
using UnityEngine;

namespace Entity
{
    public class Creature : BaseEntity
    {
        public float actionBar;
        public float attackSpeed = 100;

        public Attributes Attributes;
        public SkillManager SkillMgr;

        public Creature(int id, string name) : base(id, name)
        {
            this.Attributes = new Attributes();
            actionBar = 0;
        }

        public virtual void InitSkills(int[] skillIds)
        {
            SkillMgr = new SkillManager(this, skillIds);
        }

        public virtual void OnUpdate()
        {
            UpdateActionBar();
            OnActionReady();
            this.SkillMgr.Update();
        }

        public virtual void UpdateActionBar()
        {
            if (!Manager.Battle.isPause)
                actionBar += attackSpeed * 0.1f * Time.deltaTime;
        }

        private void OnActionReady()
        {
            if (actionBar >= Manager.Battle.maxActionBar)
            {
                // 发布通知进行行动
                Manager.Battle.isPause = true;
                // 行动
                DoAction();
                // 行动结束恢复并重置行动条
                actionBar = 0;
                Manager.Battle.isPause = false;
            }
        }

        public virtual void DoAction()
        {
            // 执行普通攻击或者技能

        }

        internal void CastSkill(int skillId)
        {
            Skill skill = this.SkillMgr.GetSkill(skillId);
            if(skill != null)
            {
                skill.Cast();
            }
        }

        internal void DoDamage(SkillHitInfo hit)
        {
            //this.Attributes.HP -= hit.damage;
            Manager.Popup.ShowDamagePopupText(hit.damageType, -hit.damage, hit.isCrit, this.Controller.GetTransform());
            //if(this.Attributes.HP <= 0)
            //{
            //    Death();
            //}
        }

        public virtual void Death()
        {
            
        }
    }
}