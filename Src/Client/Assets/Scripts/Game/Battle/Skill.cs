using System.Collections;
using System.Collections.Generic;
using Define;
using Entity;
using Framework;
using UnityEngine;

namespace Battle
{
    public class Skill
    {
        public Creature Owner;
        public SkillDefine Define;
        public SkillStatus SkillStatus;

        private float cd;
        public float CD
        {
            get { return cd; }
        }

        public Skill(Creature owner, SkillDefine define)
        {
            Owner = owner;
            Define = define;
        }

        public void Update()
        {
            UpdateCD();
        }

        private void UpdateCD()
        {
            if (this.cd > 0)
            {
                this.cd -= Time.deltaTime;
            }
            if (this.cd < 0)
                this.cd = 0;
        }

        public SkillResult CanCast()
        {
            if(SkillStatus != SkillStatus.None)
            {
                return SkillResult.Casting;
            }
            else if(this.Owner.Attributes.MP < this.Define.MPCost)
            {
                return SkillResult.OutOfMp;
            }
            else if(this.cd > 0)
            {
                return SkillResult.CoolDown;
            }
            return SkillResult.OK;
        }

        public void Cast()
        {
            SkillResult result = CanCast();
            if(result == SkillResult.OK)
            {
                this.cd = this.Define.CD;
                AddBuff();
                DoHit();
            }
        }

        private void AddBuff()
        {

        }

        private void DoHit()
        {
            SkillHitInfo hit = new SkillHitInfo();
            Creature target = Manager.Battle.battle.GetPlayerTarget();
            CalcDamage(hit, target);
            target.DoDamage(hit);

        }

        private SkillHitInfo CalcDamage(SkillHitInfo hit, Creature target)
        {
            float dmg = this.Define.BasicDamage;
            //for(int i = 0; i < this.Define.FatorAttributeTypes.Count; i++)
            //{
            //    dmg += this.Owner.Attributes.Final.Data[this.Define.FatorDamageTypes[i]] + (this.Owner.Attributes.Final.Data[this.Define.FatorAttributeTypes[i]] * this.Define.DamageFators[i]);
            //}
            hit.Init(this.Define.ID, this.Owner.id, target.id, DamageType.AD, dmg, false);

            return hit;
        }
    }
}