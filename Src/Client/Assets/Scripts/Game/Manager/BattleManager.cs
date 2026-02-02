using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    Transform[] enemyTransforms;

    private enum BattleType
    {
        None,
        Resting,
        Searching,
        Battleing
    }
    BattleType battleType = BattleType.None;

    float restTime = 5;
    float currentRestTime = 0;

    int mapId = 0;

    bool isSpawning = false;

    public void StartBattle()
    {
        if(enemyTransforms == null || enemyTransforms.Length == 0)
        {
            Transform[] child = GameObject.Find("Enemys").GetComponentsInChildren<Transform>(false);
            enemyTransforms = new Transform[child.Length - 1];
            for (int i = 1; i < child.Length; i++)
            {
                enemyTransforms[i - 1] = child[i];
            }
        }
        battleType = BattleType.Resting;
    }

    private void Update()
    {
        if(battleType == BattleType.Resting)
        {
            currentRestTime += Time.deltaTime;
            if (currentRestTime >= restTime)
            {
                battleType = BattleType.Searching;
            }
        }
        if (battleType == BattleType.Searching && !isSpawning)
        {
            currentRestTime = 0;
            isSpawning = true;
            // 开始刷怪
            Manager.Spawn.Spawn(mapId, enemyTransforms);
            battleType = BattleType.Battleing;
            isSpawning = false;
        }
    }
}
