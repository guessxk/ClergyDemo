using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameSpace.GameCore {
    public class lc_ObjViewer {
        public lc_ObjGather objgather;

        public lc_ObjViewer() {

        }
        public lc_ObjViewer(lc_ObjGather objgather) {
            this.objgather = objgather;
        }

        public  void setObjgather(lc_ObjGather objgather) {
            if (objgather == null) {
                this.objgather = objgather;
            }
        }


        /*查询对象*/
        public T ViewObject<T>(uint obj_id) where T : lc_Object {
            int typeint =lc_ObjGather.GetObjNameMap(typeof(T).Name);       
            int index = objgather.obj_list[typeint].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
            if (index == -1) return null;            
            else return (T)Activator.CreateInstance(typeof(T), objgather.obj_list[typeint][index]);
        }

        /*根据自定义编号查询对象*/
        public T ViewObjectByUserId<T>(int user_id) where T : lc_Object {
            return objgather.GetObjectByUserId<T>(user_id);
        }
        /*查询玩家角色*/
        public uint ViewPlayerId() {
            return objgather.gameGlobal.player_id;
        }
        /*查询玩家教派*/
        public uint ViewPlayerRel() {
            return objgather.gameGlobal.player_relid;
        }

        /*查询对象*/
        //public lc_People ViewPeo(uint obj_id) {
        //    int index = objgather.obj_list[(int)LISTTYPE.PEOLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else {
        //        if (objgather.obj_list[(int)LISTTYPE.PEOLIST][index].obj_type == lc_Object.GAMEOBJTYPE.SEER) {
        //            return new lc_Seer((lc_Seer)objgather.obj_list[(int)LISTTYPE.PEOLIST][index]);
        //        }
        //        else {
        //            return new lc_Personality((lc_Personality)objgather.obj_list[(int)LISTTYPE.PEOLIST][index]);
        //        }
        //    }
        //}
        //public lc_Religion ViewRel(uint obj_id) {
        //    int index = objgather.obj_list[(int)LISTTYPE.RELLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return new lc_Religion((lc_Religion)objgather.obj_list[(int)LISTTYPE.RELLIST][index]);
        //}
        //public lc_City ViewCity(uint obj_id) {
        //    int index = objgather.obj_list[(int)LISTTYPE.CITYLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return new lc_City((lc_City)objgather.obj_list[(int)LISTTYPE.CITYLIST][index]);
        //}
        //public lc_Country ViewCountry(uint obj_id) {
        //    int index = objgather.obj_list[(int)LISTTYPE.COUNTRYLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return new lc_Country((lc_Country)objgather.obj_list[(int)LISTTYPE.COUNTRYLIST][index]);
        //}
        //public lc_Canon ViewCanon(uint obj_id) {
        //    int index = objgather.obj_list[(int)LISTTYPE.CANONLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return new lc_Canon((lc_Canon)objgather.obj_list[(int)LISTTYPE.CANONLIST][index]);
        //}
        //public lc_Building ViewBuilding(uint obj_id) {
        //    int index = objgather.obj_list[(int)LISTTYPE.BUILDINGLIST].FindIndex(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
        //    if (index == -1) return null;
        //    else return new lc_Building((lc_Building)objgather.obj_list[(int)LISTTYPE.BUILDINGLIST][index]);
        //}


        /*查询所有对象*/
        public T[] viewAllObject<T>() where T:lc_Object {
            int typeint = lc_ObjGather.GetObjNameMap(typeof(T).Name);
            if (objgather.obj_list[typeint].Count <= 0) return null;
            T[] objlist = new T[objgather.obj_list[typeint].Count];
            for( int i = 0; i < objgather.obj_list[typeint].Count; i++) {
                objlist[i] = (T)Activator.CreateInstance(typeof(T), objgather.obj_list[typeint][i]);
            }

            return objlist;
        }


        /*查询关系*/
        public T viewRelShep<T>(uint relship_id) where T:STU_RELATION {
            int typeint = lc_ObjGather.GetRelNameMap(typeof(T).Name);
            int index = objgather.relship_list[typeint].FindIndex(delegate (STU_RELATION temp) { return temp.relship_id == relship_id; });
            if (index == -1) return null;
            else return (T)Activator.CreateInstance(typeof(T), objgather.relship_list[typeint][index]);
        }

        
        public List<T> viewAllRelShep<T>(uint relship_id) where T : STU_RELATION {
            return objgather.GetAllRelShip<T>(relship_id);
        }

        /*查询所有对象*/
        ////public lc_Seer[] viewAllSeer() {
        ////    int seerCount = 0;
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.PEOLIST].Count; i++) {
        ////        if (objgather.obj_list[(int)LISTTYPE.PEOLIST][i].obj_type == lc_Object.GAMEOBJTYPE.SEER) {
        ////            seerCount++;
        ////        }
        ////    }
        ////    if (objgather.obj_list[(int)LISTTYPE.PEOLIST].Count == 0 || seerCount == 0) return null;
        ////    lc_Seer[] seerlist = new lc_Seer[seerCount];
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.PEOLIST].Count; i++) {
        ////        if (objgather.obj_list[(int)LISTTYPE.PEOLIST][i].obj_type == lc_Object.GAMEOBJTYPE.SEER) {
        ////            seerlist[i] = new lc_Seer((lc_Seer)objgather.obj_list[(int)LISTTYPE.PEOLIST][i]);
        ////        }
        ////    }

        ////    return seerlist;
        ////}
        ////public lc_Personality[] viewAllperson() {
        ////    int personCount = 0;
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.PEOLIST].Count; i++) {
        ////        if (objgather.obj_list[(int)LISTTYPE.PEOLIST][i].obj_type == lc_Object.GAMEOBJTYPE.PERSONALITY) {
        ////            personCount++;
        ////        }
        ////    }
        ////    if (objgather.obj_list[(int)LISTTYPE.PEOLIST].Count == 0 || personCount == 0) return null;
        ////    lc_Personality[] personlist = new lc_Personality[personCount];
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.PEOLIST].Count; i++) {
        ////        if (objgather.obj_list[(int)LISTTYPE.PEOLIST][i].obj_type == lc_Object.GAMEOBJTYPE.PERSONALITY) {
        ////            personlist[i] = new lc_Personality((lc_Personality)objgather.obj_list[(int)LISTTYPE.PEOLIST][i]);
        ////        }
        ////    }

        ////    return personlist;

        ////}
        ////public lc_Religion[] viewAllRel() {
        ////    if (objgather.obj_list[(int)LISTTYPE.RELLIST].Count == 0) return null;
        ////    lc_Religion[] rellist = new lc_Religion[objgather.obj_list[(int)LISTTYPE.RELLIST].Count];
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.RELLIST].Count; i++) {
        ////        rellist[i] = new lc_Religion((lc_Religion)objgather.obj_list[(int)LISTTYPE.RELLIST][i]);
        ////    }

        ////    return rellist;
        ////}
        ////public lc_City[] viewAllCity() {
        ////    if (objgather.obj_list[(int)LISTTYPE.CITYLIST].Count == 0) return null;
        ////    lc_City[] citylist = new lc_City[objgather.obj_list[(int)LISTTYPE.CITYLIST].Count];
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.CITYLIST].Count; i++) {
        ////        citylist[i] = new lc_City((lc_City)objgather.obj_list[(int)LISTTYPE.CITYLIST][i]);
        ////    }
        ////    return citylist;
        ////}
        ////public lc_Country[] viewAllCountry() {
        ////    if (objgather.obj_list[(int)LISTTYPE.COUNTRYLIST].Count == 0) return null;
        ////    lc_Country[] countrylist = new lc_Country[objgather.obj_list[(int)LISTTYPE.COUNTRYLIST].Count];
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.COUNTRYLIST].Count; i++) {
        ////        countrylist[i] = new lc_Country((lc_Country)objgather.obj_list[(int)LISTTYPE.COUNTRYLIST][i]);
        ////    }

        ////    return countrylist;
        ////}
        ////public lc_Canon[] viewAllCanon() {
        ////    if (objgather.obj_list[(int)LISTTYPE.CANONLIST].Count == 0) return null;
        ////    lc_Canon[] canonlist = new lc_Canon[objgather.obj_list[(int)LISTTYPE.CANONLIST].Count];
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.CANONLIST].Count; i++) {
        ////        canonlist[i] = new lc_Canon((lc_Canon)objgather.obj_list[(int)LISTTYPE.CANONLIST][i]);
        ////    }
        ////    return canonlist;
        ////}
        ////public lc_Building[] viewAllBuilding() {
        ////    if (objgather.obj_list[(int)LISTTYPE.BUILDINGLIST].Count == 0) return null;
        ////    lc_Building[] buildinglist = new lc_Building[objgather.obj_list[(int)LISTTYPE.BUILDINGLIST].Count];
        ////    for (int i = 0; i < objgather.obj_list[(int)LISTTYPE.BUILDINGLIST].Count; i++) {
        ////        buildinglist[i] = new lc_Building((lc_Building)objgather.obj_list[(int)LISTTYPE.BUILDINGLIST][i]);
        ////    }
        ////    return buildinglist;

        ////}

        ////查询对象关系
        //public STU_PEO_CITY ViewPeoCity(uint peo_id) {
        //    return objgather.gameRel.ViewPeoCity(peo_id);
        //}
        //public STU_PEO_REL ViewPeoRel(uint peo_id) {
        //    return objgather.gameRel.ViewPeoRel(peo_id);
        //}
        //public STU_CITY_INF ViewCityInf(uint city_id) {
        //    return objgather.gameRel.ViewCityInfo(city_id);
        //}
        //public STU_CITY_COUNTRY ViewCityCountry(uint city_id) {
        //    return objgather.gameRel.ViewCityCountry(city_id);
        //}

        //查询教派总人数


        public int ViewRelCount(uint rel_id) {
            return objgather.GetRelCount(rel_id);

        }//查询教派总人数

        public List<uint> ViewCountryCity(uint country_id) {
            return objgather.GetCountryCity(country_id);
        }

    }
}

