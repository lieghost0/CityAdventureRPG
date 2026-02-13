using System.Collections;
using System.Collections.Generic;
using Battle;
using UnityEngine;

public class SkillHitInfo : MonoBehaviour
{
    public int skillId;
    public int casterId;
    public int targetId;
    public DamageType damageType;
    public float damage;
    public bool isCrit;

    public void Init(int skillId, int casterId, int targetId, DamageType damageType, float damage, bool isCrit)
    {
        this.skillId = skillId;
        this.casterId = casterId;
        this.targetId = targetId;
        this.damageType = damageType;
        this.damage = damage;
        this.isCrit = isCrit;
    }
}
