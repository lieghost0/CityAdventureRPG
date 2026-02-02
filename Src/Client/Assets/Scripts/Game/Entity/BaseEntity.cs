using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity
{
    public int id;
    public string name;

    public BaseEntity(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
