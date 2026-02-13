using System.Collections;
using System.Collections.Generic;
using Define;
using Entity;
using Framework;
using UI;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        string enemyUIPath;

        private int enemyEntityId = 1;

        private void Start()
        {
            enemyUIPath = PathUtil.GetUIPrefabPath(AppConfig.EnemyAssetName);
        }

        public void Spawn(int mapId, Transform[] enemyTransforms)
        {
            if (Manager.Data.SpawnRules.TryGetValue(mapId, out SpawnRuleDefine spawnRule))
            {
                int enemyCount = Random.Range(1, 6);
                for (int i = 0; i < enemyCount; i++)
                {
                    int enemyIndex = Random.Range(0, spawnRule.EnemyIDs.Length);
                    SpawnEnemy(spawnRule.EnemyIDs[enemyIndex], enemyTransforms[i]);
                }
            }
        }

        private void SpawnEnemy(int enemyId, Transform parent)
        {
            GameObject go = null;
            Object obj = Manager.Pool.Spawn(AppConfig.EnemyPool, enemyUIPath);
            if (obj != null)
            {
                go = obj as GameObject;
                go.transform.SetParent(parent, false);

                UIEnemy uiEnemy = go.GetComponent<UIEnemy>();
                uiEnemy.Init(enemyEntityId, enemyId, enemyUIPath);
                enemyEntityId++;
                EntityController ec = go.GetComponent<EntityController>();
                uiEnemy.Enemy.Controller = ec;
                Manager.Battle.battle.enemys.Add(enemyEntityId, uiEnemy.Enemy);
                return;
            }

            Manager.Resource.LoadUIPrefab(AppConfig.EnemyAssetName, (Object obj) =>
            {
                go = Instantiate(obj) as GameObject;
                go.transform.SetParent(parent, false);

                UIEnemy uiEnemy = go.GetComponent<UIEnemy>();
                uiEnemy.Init(enemyEntityId, enemyId, enemyUIPath);
                EntityController ec = go.AddComponent<EntityController>();
                uiEnemy.Enemy.Controller = ec;
                Manager.Battle.battle.enemys.Add(enemyEntityId, uiEnemy.Enemy);
                enemyEntityId++;
            });
        }
    }
}