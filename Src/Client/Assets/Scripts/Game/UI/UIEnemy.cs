using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemy : MonoBehaviour
{
    public Slider hP;
    public Text textHp;
    public Text textMaxHp;
    public Text enemyName;
    public Image image;

    public Enemy enemy;
    public EnemyDefine enemyDefine;

    public string AssetName;

    public void Init(int enemyId, string AssetName)
    {
        this.AssetName = AssetName;
        if(Manager.Data.Enemys.TryGetValue(enemyId, out enemyDefine))
        {
            enemy = new Enemy(enemyId, enemyDefine.Name, enemyDefine.MaxHP);
            hP.maxValue = enemyDefine.MaxHP;
            hP.value = enemy.hp;
            textMaxHp.text = string.Format("/{0:0}", enemyDefine.MaxHP);
            textHp.text = string.Format("/{0:0}", enemy.hp.ToString());
            enemyName.text = enemy.name;
            if(!string.IsNullOrEmpty(enemyDefine.EnemySprite))
            {
                Manager.Resource.LoadSprite(enemyDefine.EnemySprite, (Object obj) =>
                {
                    image.overrideSprite = obj as Sprite;
                });
            }
        }
    }

    void Death()
    {
        Manager.Pool.UnSpawn(AppConfig.EnemyPool, AssetName, this.gameObject);
    }
}
