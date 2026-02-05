using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using XLuaTest;

public class SpawnManager : MonoBehaviour
{
    string enemyUIPath;

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
                SpawnEnemy(enemyIndex, enemyTransforms[i]);
            }
        }
    }

    private void SpawnEnemy(int enemyId, Transform parent)
    {
        GameObject go = null;
        Object obj = Manager.Pool.Spawn(AppConfig.EnemyPool, enemyUIPath);
        if(obj != null)
        {
            go = obj as GameObject;
            go.transform.SetParent(parent, false);

            UIEnemy uiEnemy = go.GetComponent<UIEnemy>();
            uiEnemy.Init(enemyId, enemyUIPath);
            return;
        }
        
        Manager.Resource.LoadUIPrefab(AppConfig.EnemyAssetName, (Object obj) =>
        {
            go = Instantiate(obj) as GameObject;
            go.transform.SetParent(parent, false);

            UIEnemy uiEnemy = go.GetComponent<UIEnemy>();
            uiEnemy.Init(enemyId, enemyUIPath);
        });
    }
}
