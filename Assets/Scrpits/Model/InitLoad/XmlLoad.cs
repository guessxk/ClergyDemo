using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using GameSpace.GameCore;

namespace GameSpace.ToolsInfo {
    public class XmlLoad {

        private const float TiledHalfsize = 1.28f;  //长度的一半
        private const float TiledWidth  = 128;
        private const float TiledHeight =192;
        public XmlLoad() {

        }

        private static string MapAttrLoad(XmlNode subnode,string AttrName) {
            string attrpath = "properties/property[@name='" + AttrName + "']";
            XmlElement attrElement = (XmlElement)subnode.SelectSingleNode(attrpath);
            return attrElement.GetAttribute("value");
        }

        public static void MapTileXmlLoad(string filepath,TiledMapInfo tiledmapinfo) { /*读取XML文件并放入地图结构体中*/
            AllTiledMap alltiledmap = new AllTiledMap();
            if (File.Exists(filepath)) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                XmlNode MainNode = xmlDoc.SelectSingleNode("map"); //读取主节点
                XmlElement MainXmle = (XmlElement)MainNode;


                tiledmapinfo.width = int.Parse(MainXmle.GetAttribute("width"));   //读取长宽
                tiledmapinfo.height = int.Parse(MainXmle.GetAttribute("height"));


                XmlNodeList subnodelist = MainNode.SelectNodes("tileset");
                //加载所有板块列表
                foreach (XmlNode subnode in subnodelist) {                   
                        XmlElement temp = (XmlElement)subnode;
                        int mainid = int.Parse(temp.GetAttribute("firstgid"));
                        string tilename = temp.GetAttribute("name");
                        XmlNodeList tilelist = subnode.SelectNodes("tile"); //获取子板块
                        int listindex = 0;
                        foreach (XmlNode subtile in tilelist) {     //对板块集获取每个板块的ID和TYPE
                            XmlNode subtilenode = subtile.SelectSingleNode("properties/property");
                            XmlElement tempElement = (XmlElement)subtilenode;
                            int subid = listindex++;
                            int type = int.Parse(tempElement.GetAttribute("value"));
                            alltiledmap.addTiled(mainid + subid, tilename, type);
                            //int id = mainid + subid;
                            //Debug.Log("id=" + id + "name=" + tilename + "type=" + type);
                       }

                    
                }

                //加载基础层
                XmlNode dataNode = xmlDoc.SelectSingleNode("map/layer/data");
                string datastr = dataNode.InnerText;
                string[] splitstr = datastr.Split(',');
                for (int i = 0; i < splitstr.Length; i++) {
                    InfoTiledMap tileinfo = new InfoTiledMap();
                    tileinfo.id = int.Parse(splitstr[i]);
                    tileinfo.type = alltiledmap.findType(tileinfo.id);
                    tileinfo.x = i % tiledmapinfo.width;
                    tileinfo.y = i / tiledmapinfo.width;
                    tileinfo.positionX = (tileinfo.x + tileinfo.y * 0.5f - tileinfo.y / 2) * TiledHalfsize;
                    tileinfo.positionY = -tileinfo.y * TiledHalfsize * 0.76f;
                    tileinfo.positionZ = 0 + (-tileinfo.y) * 0.1f;
                    tileinfo.iscity = false;
                    tiledmapinfo.baselayertiled.Add(tileinfo);
                    //Debug.Log("id=[" + tileinfo.id + "] type=[" + tileinfo.type + "] x=[" + tileinfo.x + "] y=[" + tileinfo.y + "]" + "positionX=[" + tileinfo.positionX + "] positionY=[" + tileinfo.positionY + "] positionZ=[" + tileinfo.positionZ + "]");

                }

                //加载城市图块
                XmlNodeList objnodelist = xmlDoc.SelectNodes("map/objectgroup/object");
                foreach (XmlNode subnode in objnodelist) {
                    XmlElement tmpElement = (XmlElement)subnode;

                    //XmlElement mapidpro =(XmlElement)subnode.SelectSingleNode("properties/property[@name='mapid']");
                    //XmlElement citynamepro =(XmlElement)subnode.SelectSingleNode("properties/property[@name='cityname']");
                    //XmlElement citynum = (XmlElement)subnode.SelectSingleNode("properties/property[@name='citynum']");
                    //XmlElement countryid = (XmlElement)subnode.SelectSingleNode("properties/property[@name='countryid']");


                    float realx = float.Parse(tmpElement.GetAttribute("x"));
                    float realy = float.Parse(tmpElement.GetAttribute("y"));

                    InfoTiledMap tileinfo = new InfoTiledMap();
                    tileinfo.id = int.Parse(MapAttrLoad(subnode,"mapid"));
                    tileinfo.name = MapAttrLoad(subnode, "cityname");
                    tileinfo.type = alltiledmap.findType(int.Parse(tmpElement.GetAttribute("gid")));

                    tileinfo.x = Mathf.RoundToInt((realx - TiledWidth) / TiledWidth) + 1;
                    tileinfo.y = Mathf.RoundToInt((realy - TiledHeight) / (TiledHeight/2));

                    tileinfo.positionX = (tileinfo.x + tileinfo.y * 0.5f - tileinfo.y / 2) * TiledHalfsize;
                    tileinfo.positionY = -tileinfo.y * TiledHalfsize * 0.76f;
                    tileinfo.positionZ = 0 + (-tileinfo.y) * 0.1f;
                    tileinfo.iscity = true;
                    tileinfo.citynum =int.Parse(MapAttrLoad(subnode, "citynum"));
                    tileinfo.countryid = int.Parse(MapAttrLoad(subnode, "countryid"));

                    //寻找基础层对应坐标地块，修改为城市地块
                    int index = tiledmapinfo.baselayertiled.FindIndex(delegate (InfoTiledMap temp) { if (temp.x == tileinfo.x && temp.y == tileinfo.y) return true; else return false; });
                    tiledmapinfo.baselayertiled[index] = tileinfo;

                    //Debug.Log("name =["+tileinfo.name+"] realx=[" + realx + "] realy=[" + realy + "] x=[" + tileinfo.x + "] y=[" + tileinfo.y + "]");

                }

            }
            else {
                Debug.Log("Cant find file!");
                return;
            }
        } /*读取地图XML文件并放入地图结构体中*/

        public static void GameXmlLoad(string filepath,lc_ObjGather objgather,TiledMapInfo tiledmapinfo) {
            if (File.Exists(filepath)) {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filepath);
                CountryLoad(xmlDoc, objgather);
                CityLoad(tiledmapinfo, objgather);
            }
        } /*读取游戏XML*/

        private static void CountryLoad(XmlDocument doc, lc_ObjGather objgather) {
            XmlNodeList countrylist = doc.SelectNodes("listType/InitConf/CountryConf/Country");
            foreach(XmlNode country in countrylist) {
                XmlElement tempelement = (XmlElement)country;
                string name = tempelement.GetAttribute("name");
                int mapid =int.Parse(tempelement.GetAttribute("mapid"));
                int military =int.Parse(country.SelectSingleNode("military").InnerText);
                int crown = int.Parse(country.SelectSingleNode("crown").InnerText);
                lc_Country lccountry=objgather.AddObject<lc_Country>(name);
                lccountry.m_military = military;
                lccountry.m_crown = crown;
                lccountry.user_id = mapid;                
            }
        }

        private static void CityLoad(TiledMapInfo tiledmapinfo,lc_ObjGather objgather) {
            lc_ObjManager tmpmanager = new lc_ObjManager(objgather);
            for (int i =0; i< tiledmapinfo.baselayertiled.Count; i++) {
                if(tiledmapinfo.baselayertiled[i].iscity == true) {
                    lc_City city = objgather.AddObject<lc_City>(tiledmapinfo.baselayertiled[i].name);
                    city.user_id = tiledmapinfo.baselayertiled[i].id;
                    tiledmapinfo.baselayertiled[i].obj_id = city.obj_id;
                    tmpmanager.SetCityRelNum(city.obj_id, tiledmapinfo.baselayertiled[i].citynum);
                    lc_Country country = tmpmanager.GetObjectByUserId<lc_Country>(tiledmapinfo.baselayertiled[i].countryid);
                    if (country != null) {
                        //Debug.Log("set city[" + city.m_name + "] belong country[" + country_objid + "]");
                        tmpmanager.SetCityBel(city.obj_id, country.obj_id);
                    }
                    else {
                        Debug.Log("map_id " + tiledmapinfo.baselayertiled[i].countryid + " not mapping city id=["+city.user_id+"]");
                    }
                }
            }

  

            tmpmanager = null;
        }
    }
}
