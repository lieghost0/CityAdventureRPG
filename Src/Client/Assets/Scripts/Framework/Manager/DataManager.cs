using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class DataManager : MonoBehaviour
    {
        public Dictionary<int, EnemyDefine> Enemys = new Dictionary<int, EnemyDefine>();
        public Dictionary<int, MapDefine> Maps = new Dictionary<int, MapDefine>();
        public Dictionary<int, SpawnRuleDefine> SpawnRules = new Dictionary<int, SpawnRuleDefine>();

        private void Start()
        {
            Load();
        }

        public void Load()
        {
            string json = FileUtil.ReadFileText(PathUtil.DataPath + "EnemyDefine.txt");
            this.Enemys = JsonUtility.FromJson<Dictionary<int, EnemyDefine>>(json);

            json = FileUtil.ReadFileText(PathUtil.DataPath + "MapDefine.txt");
            this.Maps = JsonUtility.FromJson<Dictionary<int, MapDefine>>(json);

            json = FileUtil.ReadFileText(PathUtil.DataPath + "SpawnRuleDefine.txt");
            this.SpawnRules = JsonUtility.FromJson<Dictionary<int, SpawnRuleDefine>>(json);
        }

        public IEnumerator LoadAsync()
        {
            string json = FileUtil.ReadFileText(PathUtil.DataPath + "EnemyDefine.txt");
            this.Enemys = JsonUtility.FromJson<Dictionary<int, EnemyDefine>>(json);
            yield return null;

            json = FileUtil.ReadFileText(PathUtil.DataPath + "MapDefine.txt");
            this.Maps = JsonUtility.FromJson<Dictionary<int, MapDefine>>(json);
            yield return null;

            json = FileUtil.ReadFileText(PathUtil.DataPath + "SpawnRuleDefine.txt");
            this.SpawnRules = JsonUtility.FromJson<Dictionary<int, SpawnRuleDefine>>(json);
            yield return null;
        }
    }
}