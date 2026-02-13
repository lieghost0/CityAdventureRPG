using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entity
{
    public class BaseEntity
    {
        public int id;
        public string name;

        public IEntityController Controller;

        public BaseEntity(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}