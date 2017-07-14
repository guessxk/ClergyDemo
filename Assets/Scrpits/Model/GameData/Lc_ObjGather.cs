using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameSpace.GameCore {


    public enum LISTTYPE { SEERLIST, PERSONLIST, RELLIST, CITYLIST, COUNTRYLIST, CANONLIST, BUILDINGLIST,PERSONALITYLIST }
    
    public enum RELLISTTYPE { PEO_CITY_LIST,PEO_REL_LIST,CITY_INF_LIST,CITY_COUNTRY_LIST}

    public class lc_ObjGather {
        public List<lc_Object>[] obj_list;  //对象列表
        public List<STU_RELATION>[] relship_list; //关系列表
        public const int MAXOBJLISTNUM = 8;
        public const int MAXRELLISTNUM = 6;

        public lc_ObjGlobal gameGlobal;

        //public List<lc_VarChange> varchangelist;

        //public lc_EventBuilder eventBuilder;

        public lc_ObjGather() {
            //对象列表
            obj_list = new List<lc_Object>[MAXOBJLISTNUM];
            for (int i = 0; i < MAXOBJLISTNUM; i++) {
                obj_list[i] = new List<lc_Object>();
            }


            //关系列表
            relship_list = new List<STU_RELATION>[MAXRELLISTNUM];
            for(int i=0;i< MAXRELLISTNUM; i++) {
                relship_list[i] = new List<STU_RELATION>();
            }

            //全局变量
            gameGlobal = new lc_ObjGlobal();

            ////变量修改队列
            //varchangelist = new List<lc_VarChange>();

            ////系统事件队列
            //eventBuilder = new lc_EventBuilder(this);
        }

        public static int  GetObjNameMap(string objname) {
            switch (objname) {
                case "SEER":
                case "lc_Seer":
                    return 0;
                case "PERSONALITY":
                case "lc_Personality":
                    return 1;
                case "RELIGION":
                case "lc_Religion":
                    return 2;
                case "CITY":
                case "lc_City":
                    return 3;
                case "COUNTRY":
                case "lc_Country":
                    return 4;
                case "CANON":
                case "lc_Canon":
                    return 5;
                case "BUILDING":
                case "lc_Building":
                    return 6;
                case "PROFESSIONAL":
                case "lc_Professional":
                    return 7;
                default:
                    Debug.Log("type error");
                    return -1;
            }
        } //查询对象列表编号

        public static int GetRelNameMap(string relname) {
            switch (relname) {
                case "STU_PEO_CITY":
                    return 0;
                case "STU_PEO_REL":
                    return 1;
                case "STU_CITY_INF":
                    return 2;
                case "STU_CITY_COUNTRY":
                    return 3;
                case "STU_CITY_BUILDING":
                    return 4;
                case "STU_REL_CANON":
                    return 5;
                default:
                    Debug.Log("type error");
                    return -1;

            }
        }  //查询关系列表编号

        //查询对象
        public T GetObject<T>(uint obj_id) where T : lc_Object {
            int typeint = lc_ObjGather.GetObjNameMap(typeof(T).Name);
            int index = obj_list[typeint].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
            if (index == -1) return null;
            else return (T)obj_list[typeint][index];
        }

        //自定义编号查询对象
        public T GetObjectByUserId<T>(int user_id) where T:lc_Object {
            int typeint = lc_ObjGather.GetObjNameMap(typeof(T).Name);
            int index = obj_list[typeint].FindIndex(delegate (lc_Object temp) { return temp.user_id == user_id; });
            if (index == -1) return null;
            else return (T)obj_list[typeint][index];
        }

        //新增对象
        public T AddObject<T>(string name) where T : lc_Object {
            int typeint = lc_ObjGather.GetObjNameMap(typeof(T).Name);
            T obj = (T)Activator.CreateInstance(typeof(T), name);
            obj_list[typeint].Add(obj);
            return obj;
        }

        //查询关系
        public T GetRelShip<T>(uint relship_id) where T:STU_RELATION {
            int typeint = lc_ObjGather.GetRelNameMap(typeof(T).Name);
            int index = relship_list[typeint].FindIndex(delegate (STU_RELATION temp) { return temp.relship_id == relship_id; });
            if (index == -1) return null;
            else return (T)relship_list[typeint][index];
        }

        public List<T> GetAllRelShip<T>(uint relship_id) where T : STU_RELATION {            
            int typeint = lc_ObjGather.GetRelNameMap(typeof(T).Name);
            if (relship_list[typeint].Count <= 0) return null;
            List<T> objlist = new List<T>();
            for (int i =0;i< relship_list[typeint].Count; i++) {
                if (relship_list[typeint][i].relship_id == relship_id)
                    objlist.Add((T)relship_list[typeint][i]);
            }

            return objlist;
        }


        //public uint AddSeer(string name, int age, int life, uint rel_id) {
        //    lc_People peo = new lc_Seer(name, age, life);
        //    obj_list[(int)LISTTYPE.PEOLIST].Add(peo);
        //    gameRel.SetPeoRel(peo.obj_id, rel_id, 100);
        //    return peo.obj_id;
        //} /*新增预言家*/
        //public uint AddPerson(string name, int age, int life, uint country_id, uint pro_id) {
        //    lc_Personality peo = new lc_Personality(name, age, life);
        //    peo.belong_cty = country_id; //设置国家
        //    peo.m_profession = pro_id; //设置职业
        //    obj_list[(int)LISTTYPE.PEOLIST].Add(peo);
        //    gameRel.SetPeoRel(peo.obj_id, lc_ObjRel.DEFREL, 0); //设置默认信仰
        //    return peo.obj_id;
        //} /*新增名人*/
        //public uint AddRel(string name) {
        //    lc_Religion rel = new lc_Religion(name);
        //    obj_list[(int)LISTTYPE.RELLIST].Add(rel);
        //    return rel.obj_id;
        //} /*新增宗教*/
        //public uint AddCity(string name, int map_id, int city_peonum, uint country_id) {
        //    lc_City city = new lc_City(name, map_id);
        //    obj_list[(int)LISTTYPE.CITYLIST].Add(city);
        //    //创建默认城市关系
        //    gameRel.SetCityPeoNum(city.obj_id, city_peonum);
        //    gameRel.setCiytBel(city.obj_id, country_id);
        //    return city.obj_id;
        //} /*新增城市*/
        //public uint AddCountry(string name) {
        //    lc_Country country = new lc_Country(name);
        //    obj_list[(int)LISTTYPE.COUNTRYLIST].Add(country);
        //    return country.obj_id;
        //} /*新增国家*/
        //public uint AddCanon(string name, int theorypt_num, VarChg[] invarchg) {
        //    lc_Canon canon = new lc_Canon(name, theorypt_num, invarchg);
        //    obj_list[(int)LISTTYPE.CANONLIST].Add(canon);
        //    return canon.obj_id;
        //} /*新增教规*/
        //public uint AddBuilding(string name, int cost, VarChg[] invarchg) {
        //    lc_Building building = new lc_Building(name, cost, invarchg);
        //    obj_list[(int)LISTTYPE.BUILDINGLIST].Add(building);
        //    return building.obj_id;
        //} /*新增建筑*/





        //查询对象
        //public lc_People GetPeo(uint obj_id) {
        //    int index = obj_list[(int)LISTTYPE.PEOLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return (lc_People)obj_list[(int)LISTTYPE.PEOLIST][index];
        //}
        //public lc_Religion GetRel(uint obj_id) {
        //    int index = obj_list[(int)LISTTYPE.RELLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return (lc_Religion)obj_list[(int)LISTTYPE.RELLIST][index];
        //}
        //public lc_City GetCity(uint obj_id) {
        //    int index = obj_list[(int)LISTTYPE.CITYLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return (lc_City)obj_list[(int)LISTTYPE.CITYLIST][index];
        //}
        //public lc_Country GetCountry(uint obj_id) {
        //    int index = obj_list[(int)LISTTYPE.COUNTRYLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return (lc_Country)obj_list[(int)LISTTYPE.COUNTRYLIST][index];
        //}
        //public lc_Canon GetCanon(uint obj_id) {
        //    int index = obj_list[(int)LISTTYPE.CANONLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return (lc_Canon)obj_list[(int)LISTTYPE.CANONLIST][index];
        //}
        //public lc_Building GetBuilding(uint obj_id) {
        //    int index = obj_list[(int)LISTTYPE.BUILDINGLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return (lc_Building)obj_list[(int)LISTTYPE.BUILDINGLIST][index];
        //}

        //查询教派总人数
        public int GetRelCount(uint rel_id) {
            int count = 0;
            int typeint = lc_ObjGather.GetRelNameMap("STU_CITY_INF");
            for (int i = 0; i < relship_list[typeint].Count; i++) {
                STU_CITY_INF stu = (STU_CITY_INF)relship_list[typeint][i];
                count += stu.GetRelNum(rel_id);
            }
            return count;

        }//查询教派总人数

        public List<uint> GetCountryCity(uint country_id) {
            List<uint> citys = new List<uint>();
            int typeint = lc_ObjGather.GetRelNameMap("STU_CITY_COUNTRY");
            for(int i = 0; i < relship_list[typeint].Count; i++) {
                STU_CITY_COUNTRY stu = (STU_CITY_COUNTRY)relship_list[typeint][i];
                if(stu.country_id == country_id) {
                    citys.Add(stu.city_id); 
                }
            }

            return citys;
        } //查询所有城市

    }
}
