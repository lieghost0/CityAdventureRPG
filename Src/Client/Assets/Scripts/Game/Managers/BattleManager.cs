using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using Models;
using UnityEngine;

namespace Managers
{
    public class BattleManager : MonoBehaviour
    {
        public int maxActionBar = 100;
        Transform playerParent;
        Transform enemyParent;
        Transform[] enemyTransforms;

        private enum BattleType
        {
            None,
            Resting,
            Searching,
            Ready,
            Battleing
        }
        BattleType battleType = BattleType.None;

        float restTime = 3;
        float currentRestTime = 0;
        float readyTime = 3;
        float currentReadyTime = 0;

        int mapId = 0;
        bool isSpawning = false;
        public bool isPause = false;

        public Battle.Battle battle;

        public void StartBattle()
        {
            mapId = 1;
            if(this.battle == null)
                this.battle = new Battle.Battle();
            if (enemyTransforms == null || enemyTransforms.Length == 0)
            {
                Transform[] child = GameObject.Find("Enemys").GetComponentsInChildren<Transform>(false);
                enemyTransforms = new Transform[child.Length - 1];
                enemyParent = child[0];
                for (int i = 1; i < child.Length; i++)
                {
                    enemyTransforms[i - 1] = child[i];
                }
            }
            if(playerParent == null)
            {
                playerParent = GameObject.Find("Player").transform;
                User.Instance.Init();
                Manager.UI.CreateUserUI(playerParent);
            }

            battleType = BattleType.Resting;
        }

        private void Update()
        {
            if (battleType == BattleType.Resting)
            {
                currentRestTime += Time.deltaTime;
                if (currentRestTime >= restTime)
                {
                    battleType = BattleType.Searching;
                }
            }
            else if (battleType == BattleType.Searching && !isSpawning)
            {
                currentRestTime = 0;
                isSpawning = true;
                // 开始刷怪
                Manager.Spawn.Spawn(mapId, enemyTransforms);
                battleType = BattleType.Ready;
                isSpawning = false;
                enemyParent.gameObject.SetActive(false);
                isPause = true;
            }
            else if(battleType == BattleType.Ready)
            {
                currentReadyTime += Time.deltaTime;
                if (currentReadyTime >= readyTime)
                {
                    battleType = BattleType.Battleing;
                    enemyParent.gameObject.SetActive(true);
                    isPause = false;
                }
            }
            else if(battleType == BattleType.Battleing)
            {
                this.battle.OnUpdate();
            }
        }
    }
}