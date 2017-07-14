using System.Collections;
using System.Collections.Generic;


namespace GameSpace.ToolsInfo
{
 
    public class AllTiledMap
    {
        public List<singleTiledMap> allmap;

        public AllTiledMap() {
            allmap = new List<singleTiledMap>();
        }

        public void addTiled( int id,string name, int type) {
            singleTiledMap single = new singleTiledMap();
            single.id = id;
            single.name = name;
            single.type = type;
            allmap.Add(single);
        }

        public int findType( int id) {
            int index = allmap.FindIndex(delegate ( singleTiledMap temp) { return temp.id == id; });
            return allmap[index].type;
        }
    } //全部地图块集合

    public class singleTiledMap {
        public int id;
        public string name;
        public int type;
    }
    public class InfoTiledMap {
        public int id;
        public int type;
        public int x;
        public int y;
        public float positionX;
        public float positionY;
        public float positionZ;
        public string name;
        public bool iscity;
        public int  countryid;
        public int citynum;
        public uint obj_id;
    }

    public class TiledMapInfo
    {
        public int width;
        public int height;
        public List<InfoTiledMap> baselayertiled = new List<InfoTiledMap>();  //基础层

    }  //自定义地图信息
}
