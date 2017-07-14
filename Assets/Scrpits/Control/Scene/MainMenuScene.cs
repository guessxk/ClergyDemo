using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneExtend;
using GameSpace;
using GameSpace.GameCore;
using UnityEngine.SceneManagement;


public class MainMenuScene: BaseScene {
    public  NotifacitionCenter MapSceneMsgCenter;

    private static MainMenuScene _instance;


    public static MainMenuScene Instance
    {
        get {
            if(_instance == null) {
                _instance = new MainMenuScene();
            }

            return _instance;
        }
    }

    public MainMenuScene() : base(SceneType.MainMenuScene, SceneLoadType.COMMON, SceneLoadMode.SINGLE) {

    }

    public override void OnSceneLoaded() {
        Debug.Log("MainMenuscene load !");
        //创建场景消息管理中心
        MapSceneMsgCenter = new NotifacitionCenter();

        //创建空游戏实例
        Singleton<lc_ObjGather>.Create();
   
    }

    public override void OnSceneUnLoaded() {
        Debug.Log("MaminMenuscene unload ");
    }

}
