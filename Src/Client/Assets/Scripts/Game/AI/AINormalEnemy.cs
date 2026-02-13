using System.Collections;
using System.Collections.Generic;
using Entity;
using UnityEngine;

namespace AI
{
    public class AINormalEnemy : AIBase
    {
        public const string ID = "AINormalEnemy";

        public AINormalEnemy(Enemy enemy) : base(enemy)
        {

        }
    }
}