using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System;

namespace GameSpace.GameCore {
    public class lc_ObjManager {
        private const int MAXRATNUM = 100; //转化最大值
        public const uint DEFREL = 500; //缺省宗教编号


        public  lc_ObjGather objgather; //游戏实例
    

        public lc_ObjManager() {

        }

        public lc_ObjManager(lc_ObjGather objgather) {
            this.objgather = objgather;
            InitGameObj();
        }

        public   void setObjgather(lc_ObjGather objgather) {
            if (objgather == null) {
                this.objgather = objgather;
                InitGameObj();
            }
        }

        private void InitGameObj() {
            lc_Object.setBeginNum(1000);  //设置对象起始编号
            //basechange.setBeginNum(1000); //设置事件编号
            objgather.gameGlobal.setBeginRound(1); //设置开始回合
        }



        /*新增对象*/
        public T AddObject<T>(string name) where T : lc_Object {
            return objgather.AddObject<T>(name);
        }


        /*新增对象*/
        //public uint AddSeer(string name, int age, int life, uint rel_id) {
        //    return objgather.AddSeer(name, age, life, rel_id);
        //} /*新增预言家*/
        //public uint AddPerson(string name, int age, int life, uint country_id, uint pro_id) {
        //    return objgather.AddPerson(name, age, life, country_id, pro_id);
        //} /*新增名人*/
        //public uint AddRel(string name) {
        //    uint obj_id = objgather.AddRel(name);
        //    //objgather.eventBuilder.AddRelObject(objgather.GetRel(obj_id));
        //    return obj_id;

        //} /*新增宗教*/
        //public uint AddCity(string name, int map_id, int city_peonum, uint country_id) {
        //    return AddCity(name, map_id, city_peonum, country_id);

        //} /*新增城市*/
        //public uint AddCountry(string name) {
        //    return AddCountry(name);
        //} /*新增国家*/
        //public uint AddCanon(string name, int theorypt_num, VarChg[] invarchg) {
        //    return objgather.AddCanon(name, theorypt_num, invarchg);
        //} /*新增教规*/
        //public uint AddBuilding(string name, int cost, VarChg[] invarchg) {
        //    return objgather.AddBuilding(name, cost, invarchg);
        //} /*新增建筑*/

        public void setPlayerId(uint peo_id) {
            objgather.gameGlobal.player_id = peo_id;
        } //设置玩家ID
        public void setPlayerRelId(uint rel_id) {
            objgather.gameGlobal.player_relid = rel_id;
        } //设置玩家宗教


        
        //查询对象
        public T GetObject<T>(uint obj_id) where T : lc_Object {
            return objgather.GetObject<T>(obj_id);
        }
        //查询用户自定义对象
        public T GetObjectByUserId<T>(int user_id) where T : lc_Object {
            return objgather.GetObjectByUserId<T>(user_id);
        }

        //public uint GetCountryidFromMapid(int map_id) {
        //    int typeint = lc_ObjGather.GetObjNameMap("lc_Country");
        //    for(int i = 0; i < objgather.obj_list[typeint].Count; i++) {
        //        lc_Country country =(lc_Country)objgather.obj_list[typeint][i];
        //        if(  country.m_mapid == map_id) {
        //            return country.obj_id;
        //        }
        //    }

        //    return 0;
        //}

        //查询关系
        public T GetRelShip<T>(uint relship_id) where T : STU_RELATION {
            return objgather.GetRelShip<T>(relship_id);
        }




         /*设置对象关系*/
        public uint SetPeoCity(uint peo_id, uint city_id) {
            int typeint = lc_ObjGather.GetRelNameMap("STU_PEO_CITY");
            int index = objgather.relship_list[typeint].FindIndex(delegate (STU_RELATION temp) {
                STU_PEO_CITY substu = (STU_PEO_CITY)temp;
                if (substu.peo_id == peo_id) return true;
                else return false;
            });
            if (index == -1) {
                STU_PEO_CITY stu = new STU_PEO_CITY(peo_id, city_id); 
                objgather.relship_list[typeint].Add(stu);
                return stu.relship_id;
            }
            else {
                STU_PEO_CITY substu = (STU_PEO_CITY)objgather.relship_list[typeint][index];
                substu.city_id = city_id;
                return substu.relship_id;
            }
        }
        public int SetPeoRel(uint peo_id, uint rel_id, int rel_num) {
            int typeint = lc_ObjGather.GetRelNameMap("STU_PEO_REL");
            int index = objgather.relship_list[typeint].FindIndex(delegate (STU_RELATION temp) {
                STU_PEO_REL substu = (STU_PEO_REL)temp;
                if (substu.peo_id == peo_id) return true;
                else return false;
            });
            if (index == -1) {
                List<SUBSTU_REL_RAT> list_rel_rat = new List<SUBSTU_REL_RAT>();
                list_rel_rat.Add(new SUBSTU_REL_RAT(rel_id, rel_num));
                STU_PEO_REL stu = new STU_PEO_REL(peo_id, rel_id, list_rel_rat);
                objgather.relship_list[typeint].Add(stu);
                return (int)stu.relship_id;  /*1-新增人物*/
            }
            else {
                STU_PEO_REL substu = (STU_PEO_REL)objgather.relship_list[typeint][index];
                int subindex = substu.rel_rat.FindIndex(delegate (SUBSTU_REL_RAT temp) { return temp.rel_id == rel_id; });
                if (index == -1) {
                    substu.rel_rat.Add(new SUBSTU_REL_RAT(rel_id, rel_num));
                    return (int)substu.relship_id; /*新增宗教*/
                }
                else {
                    substu.rel_rat[subindex].rel_num += rel_num;
                    if (substu.rel_rat[subindex].rel_num >= MAXRATNUM) {
                        substu.rel_id = substu.rel_rat[subindex].rel_id;
                        return (int)substu.relship_id;/*该人物信仰改变*/
                    }
                    else {
                        return (int)substu.relship_id; /*增加宗教偏好*/
                    }

                }
            }
        }
        public uint SetCityRelNum(uint city_id, int city_peonum) {
            int typeint = lc_ObjGather.GetRelNameMap("STU_CITY_INF");
            int index = objgather.relship_list[typeint].FindIndex(delegate (STU_RELATION temp) {
                STU_CITY_INF substu = (STU_CITY_INF)temp;
                if (substu.city_id == city_id) return true;
                else return false;
            });
            if (index == -1) {
                List<SUBSTU_REL_RAT> list_rel_rat = new List<SUBSTU_REL_RAT>();
                list_rel_rat.Add(new SUBSTU_REL_RAT(DEFREL, city_peonum));
                STU_CITY_INF stu = new STU_CITY_INF(city_id, city_peonum, list_rel_rat);
                objgather.relship_list[typeint].Add(stu);
                return stu.relship_id;
            }
            else { //调整每个子模块数量
                STU_CITY_INF substu = (STU_CITY_INF)objgather.relship_list[typeint][index];
                int old_num = substu.city_peonum;
                int chg_sumnum = old_num - city_peonum;
                if (chg_sumnum < 0) {
                    int defindex = substu.rel_rat.FindIndex(delegate (SUBSTU_REL_RAT temp) { return temp.rel_id == DEFREL; });
                    int old_rel_num = substu.rel_rat[defindex].rel_num;
                    substu.rel_rat[defindex].rel_num = old_rel_num + System.Math.Abs(chg_sumnum);
                }
                else if (chg_sumnum > 0) {
                    int sumnum = substu.rel_rat.Sum(delegate (SUBSTU_REL_RAT temp) { return temp.rel_num; });
                    for (int i = 0; i < substu.rel_rat.Count(); i++) {
                        int old_rel_num = substu.rel_rat[i].rel_num;
                        
                        substu.rel_rat[i].rel_num = old_rel_num - (int)(chg_sumnum * ((float)old_rel_num / sumnum));
                    }
                }
                else { }

                substu.city_peonum = city_peonum;
                return substu.relship_id;
            }
        }
        public uint SetCityRel(uint city_id, uint rel_id, int rel_num) {
            int typeint = lc_ObjGather.GetRelNameMap("STU_CITY_INF");
            int index = objgather.relship_list[typeint].FindIndex(delegate (STU_RELATION temp) {
                STU_CITY_INF substu = (STU_CITY_INF)temp;
                if (substu.city_id == city_id) return true;
                else return false;
            });
            if (index != -1) {
                STU_CITY_INF stu = (STU_CITY_INF)objgather.relship_list[typeint][index];
                int subindex = stu.rel_rat.FindIndex(delegate (SUBSTU_REL_RAT temp) { return temp.rel_id == rel_id; });
                if (subindex == -1) {
                    SUBSTU_REL_RAT substu = new SUBSTU_REL_RAT(rel_id, rel_num);
                    stu.rel_rat.Add(substu);
                    return stu.relship_id;
                }
                else {
                    stu.rel_rat[subindex].rel_num += rel_num;
                    return stu.relship_id;
                }
            }
            return 0;
        }
        public uint SetCityBel(uint city_id, uint country_id) {
            int typeint = lc_ObjGather.GetRelNameMap("STU_CITY_COUNTRY");
            int index = objgather.relship_list[typeint].FindIndex(delegate (STU_RELATION temp) {
                STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)temp;
                if (substu.city_id == city_id) return true;
                else return false;
            });
            if (index == -1) {
                STU_CITY_COUNTRY stu = new STU_CITY_COUNTRY(city_id, country_id);
                objgather.relship_list[typeint].Add(stu);
                return stu.relship_id;
            }
            else {
                STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)objgather.relship_list[typeint][index];
                substu.country_id = country_id;
                return substu.relship_id;
            }
        }

        /*设置对象数值*/



        /*变量修改操作*/
        //public uint AddVarChange(lc_VarChange.objType sourceType, uint source_id, int setTime, int RunType, int TypeParam,
        //                      lc_VarChange.objType eventTag, uint tag_id, matchVarChg[] invarchg) {
        //    return objgather.AddVarChange(sourceType, source_id, setTime, RunType, TypeParam, eventTag, tag_id, invarchg);

        //} //新增变量修改

        //public void DelVarChange(uint event_id) {
        //    objgather.DelVarChange(event_id);
        //} //删除变量修改
        //public int ExecVarChange(int nowtime) {
        //    return objgather.ExecVarChange(nowtime);
        //} //执行变量修改


        //事件
        //public void AddEvent(uint event_id, string event_name, int judge_type) {
        //    objgather.AddEvent(event_id, event_name, judge_type);
        //}
        //public void AddTrigger(uint event_id, lc_Object.GAMEOBJTYPE obj_type, int scope, string varname, int cond_type, int varvalue) {
        //    objgather.AddTrigger(event_id, obj_type, scope, varname, cond_type, varvalue);
        //}
        //public void AddTrigger(uint event_id, uint triggerEvent_id) {
        //    objgather.AddTrigger(event_id, triggerEvent_id);
        //}
        //public void showEvent() {
        //    objgather.ShowEvent();
        //}


        //对象附加规则操作
        public int RelAddCanon(uint rel_id, uint canon_id) { /*新增教规*/
            lc_Religion rel = GetObject<lc_Religion>(rel_id);
            lc_Canon canon = GetObject<lc_Canon>(canon_id); 
            if (rel != null && canon != null) {
                if (rel.m_theorypt < canon.m_cost) return 0;  //理论点数不足
                else {
                    rel.m_theorypt -= canon.m_cost;
                    rel.RelAddCanon(canon);
                    return 1;
                }

                //matchVarChg[] varchgarray = new matchVarChg[canon.chglist.Count];
                //for( int i = 0; i < varchgarray.Length; i++) {
                //    varchgarray[i] = new matchVarChg(canon.chglist[i].instruction, canon.chglist[i].varname, canon.chglist[i].chgnum, canon.chglist[i].instruction, canon.chglist[i].varname, -canon.chglist[i].chgnum);
                //}
                //uint event_id =objgather.AddVarChange(lc_VarChange.objType.CANON,canon_id,NowRound(),2,0,lc_VarChange.objType.RELIGION,rel_id,varchgarray);
                //rel.relAddCanon(canon_id, event_id);
                //objgather.ExecVarChange(1); /*立即轮询一次事件*/              
                //return 1;
            }
            else return -1; /*逻辑错误*/
        } /*宗教新增规则*/
        public int RelDelCanon(uint rel_id, uint canon_id) { /*删除教规*/
            lc_Religion rel = GetObject<lc_Religion>(rel_id);
            lc_Canon canon = GetObject<lc_Canon>(rel_id); 
            if (rel != null && canon != null) {
                rel.RelDelCanon(canon);
                return 1;

                //int index  = rel.listCanonEvent.FindIndex(delegate (CanonEvent temp) { return temp.canon_id == canon_id; });
                //if (index == -1) return -1; /*逻辑错误*/
                //else {
                //    objgather.DelVarChange(rel.listCanonEvent[index].event_id);
                //    rel.listCanonEvent.RemoveAt(index);
                //    return 1;
                //}                
            }
            else return -1; /*逻辑错误*/
        } /*宗教删除规则*/
        public int CityAddBuilding(uint city_id, uint peo_id, uint building_id) {
            uint rel_belong;
            uint obj_id;
            int index = lc_ObjGather.GetRelNameMap("STU_CITY_BUILDING");
            lc_City city = GetObject<lc_City>(city_id);
            lc_Building building = GetObject<lc_Building>(building_id);
            lc_Seer seer = GetObject<lc_Seer>(peo_id);
            if(peo_id !=0) {
                if (seer.m_wealth < building.m_cost) return 0;
                seer.m_wealth -= building.m_cost;
               
            }

            if (building.m_belong) {
                STU_PEO_REL stu_peo_rel = GetRelShip<STU_PEO_REL>(peo_id);
                rel_belong = stu_peo_rel.rel_id;
            }
            else {
                rel_belong = 0;
            }
                
            STU_CITY_BUILDING stu_city_build = new STU_CITY_BUILDING(city_id, building.obj_id, rel_belong);
            objgather.relship_list[index].Add(stu_city_build);

            foreach ( var chgnum in building.chglist) {
                int typeindex = lc_ObjGather.GetObjNameMap(chgnum.objtype);
                switch (chgnum.objtype) {
                     case "BUILDING":
                         obj_id = building.obj_id;
                         break;
                     case "RELIGION":
                         obj_id = stu_city_build.rel_belong_id;
                         break;
                     default:
                         return -1;
                }

                lc_Object obj = objgather.obj_list[typeindex].Find(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
                 obj.ChangeVar<int>(chgnum.varname, chgnum.chgnum);
             }

             return 1;
            

           
        } /*城市增加建筑*/
        public int CityDelBuilding(uint city_id, uint building_id) {
            uint obj_id;
            int typeint = lc_ObjGather.GetRelNameMap("STU_CITY_BUILDING");
            lc_City city = GetObject<lc_City>(city_id);
            lc_Building building = GetObject<lc_Building>(building_id);
            if (city != null && building != null) {
                List<STU_CITY_BUILDING> stulist = objgather.GetAllRelShip<STU_CITY_BUILDING>(city_id);
                int index = stulist.FindIndex(delegate (STU_CITY_BUILDING temp) { return temp.building_id == building_id; });
                if (index == -1) return -1;
                else {
                    foreach (var chgnum in building.chglist) {
                        int typeindex = lc_ObjGather.GetObjNameMap(chgnum.objtype);
                        switch (chgnum.objtype) {
                            case "BUILDING":
                                obj_id = building.obj_id;
                                break;
                            case "RELIGION":
                                obj_id = stulist[index].rel_belong_id;
                                break;
                            default:
                                return -1;
                        }
                        lc_Object obj = objgather.obj_list[typeindex].Find(delegate (lc_Object temp) { return temp.obj_id == obj_id; });
                        obj.ChangeVar<int>(chgnum.varname, -chgnum.chgnum);
                        stulist.RemoveAt(index);
                    }

                    return 1;
                }
            }
            else return -1;/*逻辑错误*/
            
        }/*城市删除建筑*/


        /*当前回合数*/
        public int NowRound() {
            return objgather.gameGlobal.round;
        }
        /*下一回合*/
        public void NextRound() {
            objgather.gameGlobal.NextRound();
        }

        //玩家行为
        public int active_Preach(uint peo_id) {
            lc_Seer seer = GetObject<lc_Seer>(peo_id);
            uint rel_id = GetRelShip<STU_PEO_REL>(peo_id).rel_id;
            lc_Religion rel = GetObject<lc_Religion>(rel_id);
            uint city_id = GetRelShip<STU_PEO_CITY>(peo_id).city_id;
            lc_City city = GetObject<lc_City>(city_id);
            uint country_id = GetRelShip<STU_CITY_COUNTRY>(city_id).country_id;
            lc_Country country = GetObject<lc_Country>(country_id);
            int city_rel_index = GetRelShip<STU_CITY_INF>(city_id).rel_rat.FindIndex(delegate (SUBSTU_REL_RAT temp) { return temp.rel_id == DEFREL; });
            int city_rel_num = GetRelShip<STU_CITY_INF>(city_id).rel_rat[city_rel_index].rel_num;
            //效果 = 口才*4000+(宗教吸引力-王权)*2000-组织度*1000
            int attr_num = seer.m_eloquence * 4000 + (rel.m_attraction - country.m_crown) * 2000 - rel.m_organization * 1000;
            //减少对应无信仰人数,增加对应宗教人数
            if (attr_num > city_rel_num) attr_num = city_rel_num;
            SetCityRel(city.obj_id, DEFREL, -attr_num);
            SetCityRel(city.obj_id, rel.obj_id, attr_num);

            return 1;

        } //传教
        public int active_Move(uint peo_id, uint tag_city_id) {
            lc_Seer seer = GetObject<lc_Seer>(peo_id);
            uint cur_city_id = GetRelShip<STU_PEO_CITY>(peo_id).city_id;
            if (cur_city_id == tag_city_id) return -1;

            SetPeoCity(peo_id, tag_city_id);

            return 1;
        } //移动
        public int active_Visit(uint seer_id, uint person_id) {
            lc_Seer seer = GetObject<lc_Seer>(seer_id);
            uint rel_id = GetRelShip<STU_PEO_REL>(seer_id).rel_id;
            lc_Religion rel = GetObject<lc_Religion>(rel_id);
            lc_Personality person = GetObject<lc_Personality>(person_id);

            int chgnum = seer.m_eloquence * 2 - person.m_risist;
            if (chgnum <= 0) return 5; /*转变程度小于0*/
            if (GetRelShip<STU_PEO_REL>(person_id).rel_id == DEFREL) {
                int ret = SetPeoRel(person.obj_id, rel.obj_id, chgnum);
                if (ret == 3) return 3; /*增加该人物转变*/
                else if (ret == 4) return 4; /*成功转变该人物*/
                else return -1; /*逻辑错误*/
            }
            else {
                return 2; /*该人物已信仰其他宗教*/
            }
        } //拜访
        public int active_Build(uint peo_id, uint building_id) {
            lc_Seer seer = GetObject<lc_Seer>(peo_id);
            uint city_id = GetRelShip<STU_PEO_CITY>(peo_id).city_id;
            lc_City city = GetObject<lc_City>(city_id);
            int ret = CityAddBuilding(city.obj_id, seer.obj_id, building_id);
            return ret;
        } //修建建筑
        public int active_Meditation(uint peo_id) {
            lc_Seer seer = GetObject<lc_Seer>(peo_id);
            uint rel_id = GetRelShip<STU_PEO_REL>(peo_id).rel_id;
            lc_Religion rel = GetObject<lc_Religion>(rel_id);

            int chgnum = seer.m_intelligence;
            rel.m_theorypt += chgnum;
            return 1;

        } //冥思


    }
}
