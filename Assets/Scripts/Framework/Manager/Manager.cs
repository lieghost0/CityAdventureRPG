using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public class Manager : MonoBehaviour
    {
        private static ResourceManager _resource;
        public static ResourceManager Resource
        {
            get { return _resource; }
        }

        private static PoolManager _pool;
        public static PoolManager Pool
        {
            get { return _pool; }
        }

        private static LuaManager _lua;
        public static LuaManager Lua
        {
            get { return _lua; }
        }

        private static EventManager _event;
        public static EventManager Event
        {
            get { return _event; }
        }

        private static UIManager _ui;
        public static UIManager UI
        {
            get { return _ui; }
        }

        private void Awake()
        {
            _resource = gameObject.AddComponent<ResourceManager>();
            _pool = gameObject.AddComponent<PoolManager>();
            _lua = gameObject.AddComponent<LuaManager>();
            _event = gameObject.AddComponent<EventManager>();
            _ui = gameObject.AddComponent<UIManager>();
        }
    }
}