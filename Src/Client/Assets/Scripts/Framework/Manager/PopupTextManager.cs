using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Battle;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using XLua;

namespace Framework
{
    public class PopupTextManager : MonoBehaviour
    {
        public void ShowDamagePopupText(DamageType damageType, float damage, bool isCrit, Transform transform)
        {
            GameObject ui = null;

            string uiPath = PathUtil.GetUIPrefabPath(AppConfig.DamagePopupUIName);
            Object uiObj = Manager.Pool.Spawn(AppConfig.UIPool, uiPath);
            Transform parent = transform;
            if (uiObj != null)
            {
                ui = uiObj as GameObject;
                ui.transform.SetParent(parent, false);

                UIPopupText popupText = ui.GetComponent<UIPopupText>();
                popupText.InitPopup(damageType, damage, isCrit, uiPath);
                ui.SetActive(true);
                return;
            }

            Manager.Resource.LoadUIPrefab(AppConfig.DamagePopupUIName, (Object obj) =>
            {
                ui = Instantiate(obj) as GameObject;
                ui.transform.SetParent(parent, false);

                UIPopupText popupText = ui.GetComponent<UIPopupText>();
                popupText.InitPopup(damageType, damage, isCrit, uiPath);
            });
        }
    }
}