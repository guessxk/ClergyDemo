using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneExtend;
using GameSpace;
using GameSpace.GameCore;
using GameSpace.ToolsInfo;
using UnityEngine.SceneManagement;
using System.IO;


public class MainMapScene : BaseScene {
    public lc_ObjGather objgather;
    public lc_ObjManager objmanager;
    public lc_ObjViewer objviewer;
    
    public NotifacitionCenter MapSceneMsgCenter;
    public int testint;


    private static MainMapScene _instance;

    public static MainMapScene Instance
    {
        get {
            if (_instance == null) {
                _instance = new MainMapScene();
            }

            return _instance;
        }
    }

    public MainMapScene():base(SceneType.MainMapScene, SceneLoadType.COMMON, SceneLoadMode.SINGLE) {
        Debug.Log("[" + SceneType.MainMapScene.sceneName+"]");

    }

    public override void OnSceneLoaded() {
        Debug.Log("MainMapScene load activescene=["+SceneManager.GetActiveScene().name+"]");
        //创建场景消息管理中心
        MapSceneMsgCenter = new NotifacitionCenter();
        //创建UI管理器管理UI
        Singleton<UIManager>.Create();
        Singleton<ContextManager>.Create();

        //string mappath = Application.dataPath + "/StreamingAssets/AssetBundles/maptile";
        //string mapuipath = Application.dataPath + "/StreamingAssets/AssetBundles/mapsceneui";
        //string rolepath = Application.dataPath + "/StreamingAssets/AssetBundles/role";

        ResourceManger.Instance.LoadRes(Application.streamingAssetsPath + @"/AssetBundles/AssetBundles");
        ResourceManger.Instance.LoadRes(Application.streamingAssetsPath+@"/AssetBundles/maptile");
        ResourceManger.Instance.LoadRes(Application.streamingAssetsPath + @"/AssetBundles/mapsceneui");
        ResourceManger.Instance.LoadRes(Application.streamingAssetsPath + @"/AssetBundles/role");



        //AssetBundle mapBundle = AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/AssetBundles/maptile");
        //AssetBundle mapUiBundle= AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/AssetBundles/mapsceneui");
        //AssetBundle roleBundle=AssetBundle.LoadFromFile(Application.dataPath + "/StreamingAssets/AssetBundles/role");

        

        //创建地图实例
        Singleton<TiledMapInfo>.Create();
        objgather =  Singleton<lc_ObjGather>.Instance;
        objmanager = new lc_ObjManager(objgather);
        objviewer = new lc_ObjViewer(objgather);
      

        //加载游戏地图及游戏脚本文件
        string mapfilepath = Application.dataPath + @"/StreamingAssets/Map/MapInfo.tmx";
        string gamefilepath = Application.dataPath + @"/StreamingAssets/Game/GameData.xml";
        XmlLoad.MapTileXmlLoad(mapfilepath, Singleton<TiledMapInfo>.Instance);
        XmlLoad.GameXmlLoad(gamefilepath, objgather, Singleton<TiledMapInfo>.Instance);

        //UI初始化
        Singleton<ContextManager>.Instance.Push(new ActiveMenuContext());
        Singleton<ContextManager>.Instance.Push(new OptionPanelContext());
        Singleton<ContextManager>.Instance.Push(new PopInterFacePanelContext());
        Singleton<ContextManager>.Instance.NoStackPush(new CityInfoSignContext());

        //创建地图生成器生成地图
        GameObject go = new GameObject("MapMaker");
        go.AddComponent<hexgrid>();

        CreateGameTest();
    }

    public override void OnSceneUnLoaded() {
        Debug.Log("MainMapScene unload");
        //Singleton<ContextManager>.Instance.PopAll();
        MapSceneMsgCenter.removeAllObservers();
        Singleton<TiledMapInfo>.Destroy();
        Singleton<UIManager>.Destroy();
        Singleton<ContextManager>.Destroy();

        ResourceManger.Instance.UnLoadRes(Application.streamingAssetsPath + @"/AssetBundles/AssetBundles");
        ResourceManger.Instance.UnLoadRes(Application.streamingAssetsPath + @"/AssetBundles/maptile");
        ResourceManger.Instance.UnLoadRes(Application.streamingAssetsPath + @"/AssetBundles/mapsceneui");
        ResourceManger.Instance.UnLoadRes(Application.streamingAssetsPath + @"/AssetBundles/role");


    }


    private  void CreateGameTest() {
        //创建GAME数据
        lc_Religion rel = objmanager.AddObject<lc_Religion>("万法教");
        lc_Seer player = objmanager.AddObject<lc_Seer>("主角1");

        lc_Building market= objmanager.AddObject<lc_Building>("market");
        market.user_id = 1;
        market.m_cost = 0;
        lc_Building castle = objmanager.AddObject<lc_Building>("castle");
        castle.user_id = 2;
        castle.m_cost = 0;

        lc_City city = objmanager.GetObjectByUserId<lc_City>(2);

        objmanager.CityAddBuilding(city.obj_id, 0, market.obj_id);
        objmanager.CityAddBuilding(city.obj_id, 0, castle.obj_id);



        player.m_age = 17;
        player.m_life = 55;
        player.m_eloquence = 50;
        player.m_intelligence = 50;
        player.m_leadership = 50;
        player.m_willpower = 50;
        player.m_fame = 10;
        player.m_wealth = 100;
        
       
         
        rel.m_relType = 1;
        rel.m_attraction = 50;
        rel.m_piety = 50;
        rel.m_organization = 50;
        rel.m_theorypt = 2;
       objmanager.setPlayerRelId(rel.obj_id);
        objmanager.setPlayerId(player.obj_id);

    }
}
