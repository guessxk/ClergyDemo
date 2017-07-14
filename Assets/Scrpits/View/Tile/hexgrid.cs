using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSpace.ToolsInfo;
using GameSpace;
using GameSpace.GameCore;
using System.IO;

public class hexgrid : MonoBehaviour {

   
    public TiledMapInfo tiledmapinfo;

    public List<GameObject> Tiledlist; //地图预制体集合
    public hexcell[,] cells;  //地图对象集合
    private int width;
    private int heigth;



    private Canvas gridCanvas;
    private Text cellLabelPrefab;


       
    // Use this for initialization
    void Awake() {
        InitMapTile();       
        tiledmapinfo = Singleton<TiledMapInfo>.Instance;
        width = tiledmapinfo.width;
        heigth = tiledmapinfo.height;
      
        cells = new hexcell[width,heigth];

        //创建基础层板块
        CreateTileMap(tiledmapinfo,cells);


    }
    void Start () {
        OnMapInitedEvent(); //发送地图初始化完成事件
    }

    // Update is called once per frame
    void Update () {
		
	}

    //private void CreateCell(int x,int y, float positionx,float positiony, float positionz, hexcell.MAP_TYPE type, hexcell[,] cell) {
    //    Vector3 position;
      

       

      
        

       

    //}//指定位置创建地图板块
    private void CreateTileMap( TiledMapInfo mapinfo, hexcell[,] Initcells) {
        
        for (int i = 0; i < mapinfo.baselayertiled.Count; i++) {
            Vector3 position;
            int x = mapinfo.baselayertiled[i].x;
            int y = mapinfo.baselayertiled[i].y;
            float positionx = mapinfo.baselayertiled[i].positionX;
            float positiony = mapinfo.baselayertiled[i].positionY;
            float positionz = mapinfo.baselayertiled[i].positionZ;
            hexcell.MAP_TYPE type = (hexcell.MAP_TYPE)mapinfo.baselayertiled[i].type;
            position.x = positionx;
            position.y = positiony;
            position.z = positionz;

            hexcell hex_cell = InstantiateItem(position, type).GetComponent<hexcell>();
            hex_cell.transform.SetParent(this.transform, false);
            Initcells[x, y] = hex_cell;
            if(mapinfo.baselayertiled[i].iscity == true) {
                Initcells[x, y].city_id = mapinfo.baselayertiled[i].id;
                Initcells[x, y].obj_id = mapinfo.baselayertiled[i].obj_id;
                //Debug.Log("mapid=[" + mapinfo.baselayertiled[i].id + "] obj_id=["+mapinfo.baselayertiled[i].obj_id+"]");
            }
            //CreateCell(x, y, positionx, positiony, positionz, type, Initcells);
        }
    }//指定位置创建地图板块

    private GameObject InstantiateItem(Vector3 position,hexcell.MAP_TYPE type) {//根据坐标和类型复制预制体
        List<GameObject> CondList = Tiledlist.FindAll(delegate (GameObject temp) { return temp.GetComponent<hexcell>().type == type; });
        int index = Random.Range(0, CondList.Count);
        GameObject obj = Instantiate<GameObject>(CondList[index],position,Quaternion.identity);
        return obj;
    } //根据坐标和类型复制预制体

    private void InitMapTile( ) {  /*初始化所有地图块预制体，放入LIST中*/
        Tiledlist = new List<GameObject>();
        string path = Application.streamingAssetsPath + @"/AssetBundles/maptile";
        Debug.Log("path=[" + path + "]");
        GameObject[] obj = ResourceManger.Instance.GetAllRes<GameObject>(path);
        //GameObject[] obj = Resources.LoadAll<GameObject>(@"Prefabs\MapTiled\MapTile128X192");
        foreach( GameObject singleobj in obj) {
            Tiledlist.Add(singleobj);
        }

    }  /*初始化所有地图块预制体，放入LIST中*/


    private void OnMapInitedEvent() {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("mapX1", cells[0, 0].transform.position.x.ToString());
        dict.Add("mapY1", cells[0, 0].transform.position.y.ToString());
        dict.Add("mapX2", cells[width - 1, heigth - 1].transform.position.x.ToString());
        dict.Add("mapY2", cells[width - 1, heigth - 1].transform.position.y.ToString());

        NotifyEvent evt = new NotifyEvent(NotifyType.MSG_SYS_MAPINITED,dict, this);
        MainMapScene.Instance.MapSceneMsgCenter.postNotification(evt);
    

    }//地图初始化完成事件

}
