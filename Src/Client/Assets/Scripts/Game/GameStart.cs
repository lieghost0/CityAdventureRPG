using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using XLua;

public class GameStart : MonoBehaviour
{
    public GameMode GameMode;
    public bool OpenLog;
    private Action startAction;
    void Start()
    {
        Manager.Event.Subscribe((int)GameEvent.GameInit, GameInit);
        Manager.Event.Subscribe((int)GameEvent.StartLua, StartLua);

        AppConst.GameMode = GameMode;
        AppConst.OpenLog = OpenLog;

        if (AppConst.GameMode == GameMode.UpdateMode)
            this.gameObject.AddComponent<HotUpdate>();
        else
            Manager.Event.Fire((int)GameEvent.GameInit);
    }

    void GameInit(object args)
    {
        if (AppConst.GameMode != GameMode.EditorMode)
            Manager.Resource.ParseVersionFile();
        Manager.Lua.Init();
    }

    void StartLua(object args)
    {
        Manager.Lua.StartLua("main");

        startAction = Manager.Lua.LuaEnv.Global.Get<Action>("StartGame");
        startAction?.Invoke();

        Manager.Pool.CreateGameObjectPool(AppConfig.UIPool, 600);
        Manager.Pool.CreateGameObjectPool(AppConfig.EnemyPool, 600);
        Manager.Pool.CreateAssetPool(AppConfig.AssetBundlePool, 600);
    }

    private void OnApplicationQuit()
    {
        startAction = null;
        Manager.Event.UnSubscribe((int)GameEvent.GameInit, GameInit);
        Manager.Event.UnSubscribe((int)GameEvent.StartLua, StartLua);
    }
}
