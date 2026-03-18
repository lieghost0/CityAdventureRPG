using System.Collections;
using System.Collections.Generic;
using Define;
using Entity;
using Framework;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIEnemy : MonoBehaviour
    {
        public Slider HP;
        public Text TextHp;
        public Text TextMaxHp;
        public Text EnemyName;
        public Image Image;

        public Enemy Enemy;
        public EnemyDefine EnemyDefine;

        public string AssetName;

        public void Init(int entityId, int enemyId, string AssetName)
        {
            this.AssetName = AssetName;
            if (Manager.Data.Enemys.TryGetValue(enemyId, out EnemyDefine))
            {
                Enemy = new Enemy(entityId, EnemyDefine);
                HP.maxValue = EnemyDefine.MaxHP;
                //HP.value = Enemy.Attributes.HP;
                HP.value = 10;
                TextMaxHp.text = string.Format("/{0:0}", EnemyDefine.MaxHP);
                //TextHp.text = string.Format("/{0:0}", Enemy.Attributes.HP.ToString());
                TextHp.text = string.Format("{0:0}", 10.ToString());
                EnemyName.text = Enemy.name;
                if (!string.IsNullOrEmpty(EnemyDefine.EnemySprite))
                {
                    Manager.Resource.LoadSprite(EnemyDefine.EnemySprite, (Object obj) =>
                    {
                        Image.overrideSprite = obj as Sprite;
                    });
                }
            }
        }

        private void Update()
        {
            if (this.TextHp != null) TextHp.text = string.Format("{0:0}", 10.ToString());
        }

        public void Death()
        {
            Manager.Pool.UnSpawn(AppConfig.EnemyPool, AssetName, gameObject);
        }
    }
}