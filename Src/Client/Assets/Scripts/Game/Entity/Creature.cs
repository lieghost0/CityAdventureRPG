using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class Creature : BaseEntity
{
    public float hp;
    public Creature(int id, string name, float hp) : base(id, name)
    {
        this.hp = hp;
    }

    public virtual void OnUpdate()
    {

    }
}
