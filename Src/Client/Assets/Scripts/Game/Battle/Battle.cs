using System.Collections;
using System.Collections.Generic;
using Entity;
using Models;
using UnityEngine;

namespace Battle
{
    public class Battle
    {
        public Dictionary<int, Enemy> enemys;

        public Battle()
        {
            enemys = new Dictionary<int, Enemy>();
        }

        public void OnUpdate()
        {
            //User.Instance.player.OnUpdate();
            foreach(int enemyId in enemys.Keys)
            {
                enemys[enemyId].OnUpdate();
            }
        }

        public Creature GetEnemyTarget(int enemyId)
        {
            if(enemys.TryGetValue(enemyId, out Enemy target))
            {
                return target;
            }
            return null;
        }

        public Creature GetPlayerTarget()
        {
            return User.Instance.player;
        }

        public void EndBattle()
        {
            enemys.Clear();
        }
    }
}