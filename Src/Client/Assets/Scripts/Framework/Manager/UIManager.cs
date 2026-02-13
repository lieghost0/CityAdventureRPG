using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace Framework
{
    public class UIManager : MonoBehaviour
    {
        // 层级
        Dictionary<int, Transform> m_Layers = new Dictionary<int, Transform>();

        // UI
        Dictionary<string, UILogic> m_UILogics = new Dictionary<string, UILogic>();

        public Transform UIParent;

        private void Awake()
        {
            UIParent = GameObject.FindGameObjectWithTag("GamePanel")?.transform;
            Transform[] layers = UIParent.GetComponentsInChildren<Transform>(false);
            
            for (int i = 1; i < layers.Length; i++)
            {
                m_Layers.Add(i, layers[i]);
            }
        }

        public void SetUILayer(GameObject obj, int layer)
        {
            obj.transform.SetParent(m_Layers[layer], false);
        }

        public Transform GetUILayer(int layer)
        {
            if (!m_Layers.ContainsKey(layer))
                LogUtil.Error("layer is not exists");
            return m_Layers[layer];
        }

        public GameObject OpenUI(string uiName, int layer, string luaName)
        {
            GameObject ui = null;
            
            string uiPath = PathUtil.GetUIPrefabPath(uiName);
            Object uiObj = Manager.Pool.Spawn(AppConfig.UIPool, uiPath);
            Transform parent = GetUILayer(layer);
            if (uiObj != null)
            {
                ui = uiObj as GameObject;
                ui.transform.SetParent(parent, false);

                UILogic uiLogic = ui.GetComponent<UILogic>();
                uiLogic.OnOpen();
                AddUILogic(uiName, uiLogic);
                return ui;
            }

            Manager.Resource.LoadUIPrefab(uiName, (Object obj) =>
            {
                ui = Instantiate(obj) as GameObject;

                ui.transform.SetParent(parent, false);

                UILogic uiLogic = ui.AddComponent<UILogic>();
                uiLogic.AssetName = uiPath;
                uiLogic.Init(luaName);
                uiLogic.OnOpen();
                m_UILogics[uiName] = uiLogic;
                AddUILogic(uiName, uiLogic);
            });

            return ui;
        }

        public void CloseUI(string uiName)
        {
            if (m_UILogics.TryGetValue(uiName, out UILogic uILogic))
            {
                uILogic.OnClose();
                m_UILogics.Remove(uiName);
            }
        }

        private void AddUILogic(string uiName, UILogic uiLogic)
        {
            if (!m_UILogics.ContainsKey(uiName))
            {
                m_UILogics.Add(uiName, uiLogic);
            }
            else
            {
                m_UILogics[uiName] = uiLogic;
            }
        }

        public void CreateUserUI(Transform parent)
        {
            GameObject go = null;

            Manager.Resource.LoadUIPrefab("UIPlayer", (Object obj) =>
            {
                go = Instantiate(obj) as GameObject;
                go.transform.SetParent(parent, false);

                UIPlayer uiPlayer = go.AddComponent<UIPlayer>();
                //uiPlayer.Init(enemyId, enemyUIPath);
                EntityController ec = go.AddComponent<EntityController>();
                User.Instance.player.Controller = ec;
            });
        }
    }
}