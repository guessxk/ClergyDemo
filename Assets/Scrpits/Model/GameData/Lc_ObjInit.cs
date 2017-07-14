//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.Xml;
//using System.IO;

//namespace GameSpace.GameCore {
//    public class lc_ObjInit {
//        public lc_ObjGather objgather; //游戏实例

//        public lc_ObjInit(lc_ObjGather objgather) {
//            this.objgather = objgather;
//        }

//        public void LoadXmlInit(string filepath) {
//            XmlDocument xmlDoc = new XmlDocument();
//            if (File.Exists(filepath)) {
//                xmlDoc.Load(filepath);
//                LoadCanon(xmlDoc);
//                LoadBuilding(xmlDoc);
//            }

//        } //读取XML文件

//        private void LoadCanon(XmlDocument xmlDoc) {
//            XmlNode CanonNode = xmlDoc.SelectSingleNode("listType").SelectSingleNode("InitConf").SelectSingleNode("CanonConf");
//            XmlNodeList CanonNodeList = CanonNode.SelectNodes("Canon");
//            foreach (var childCanonNode in CanonNodeList) {
//                string name = ((XmlElement)childCanonNode).GetAttribute("name");
//                int cost = int.Parse(((XmlElement)childCanonNode).GetAttribute("cost"));
//                XmlNodeList varChgList = ((XmlNode)childCanonNode).SelectNodes("CanonVarChg");
//                VarChg[] varChgArray = new VarChg[varChgList.Count];
//                for (int i = 0; i < varChgList.Count; i++) {
//                    string inst = varChgList[i].SelectSingleNode("Inst").InnerText;
//                    string varname = varChgList[i].SelectSingleNode("VarName").InnerText;
//                    int chgnum = int.Parse(varChgList[i].SelectSingleNode("ChgNum").InnerText);
//                    varChgArray[i] = new VarChg(inst, varname, chgnum);
//                }
//                lc_Canon canon = objgather.AddObject<lc_Canon>(name);
//                canon.m_cost = cost;
//                foreach (var temp in varChgArray) {
//                    canon.chglist.Add(new VarChg(temp.instruction, temp.varname, temp.chgnum));
//                }

//                //objgather.AddCanon(name, cost, varChgArray);

//            }
//        }

//        private void LoadBuilding(XmlDocument xmlDoc) {
//            XmlNode BuildingNode = xmlDoc.SelectSingleNode("listType").SelectSingleNode("InitConf").SelectSingleNode("BuildingConf");
//            XmlNodeList BuildingNodelist = BuildingNode.SelectNodes("Building");
//            foreach (var childBuildingNode in BuildingNodelist) {
//                string name = ((XmlElement)childBuildingNode).GetAttribute("name");
//                int cost = int.Parse(((XmlElement)childBuildingNode).GetAttribute("cost"));
//                XmlNodeList varChgList = ((XmlNode)childBuildingNode).SelectNodes("BuildingVarChg");
//                VarChg[] varChgArray = new VarChg[varChgList.Count];
//                for (int i = 0; i < varChgList.Count; i++) {
//                    string inst = varChgList[i].SelectSingleNode("Inst").InnerText;
//                    string varname = varChgList[i].SelectSingleNode("VarName").InnerText;
//                    int chgnum = int.Parse(varChgList[i].SelectSingleNode("ChgNum").InnerText);
//                    varChgArray[i] = new VarChg(inst, varname, chgnum);
//                }
//                lc_Building building = objgather.AddObject<lc_Building>(name);
//                building.m_cost = cost;
//                foreach (var temp in varChgArray) {
//                    building.chglist.Add(new VarChg(temp.instruction, temp.varname, temp.chgnum));
//                }

//                //objgather.AddBuilding(name, cost, varChgArray);

//            }
//        }

//    }
//}
