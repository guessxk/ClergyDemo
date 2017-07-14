using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

namespace GameSpace.GameCore {
    public enum RELLISTYPE { TP_PEO_CITY, TP_PEO_REL, TP_CITY_INF, TP_CITY_COUNTRY,TP_CITY_BUILDING,TP_REL_CANON };

    public abstract class STU_RELATION {

        public RELLISTYPE relation_type;
        public uint relship_id;

  

        public STU_RELATION(RELLISTYPE type) {
            relation_type = type;
        }

        public STU_RELATION(STU_RELATION temp) {
            this.relship_id = temp.relship_id;
            this.relation_type = temp.relation_type;
        }
    }

    public class SUBSTU_REL_RAT {

        public uint rel_id;
        public int rel_num;
        public SUBSTU_REL_RAT(uint rel_id, int rel_num) {
            this.rel_id = rel_id;
            this.rel_num = rel_num;
        }

        public SUBSTU_REL_RAT(SUBSTU_REL_RAT temp) {
            this.rel_id = temp.rel_id;
            this.rel_num = temp.rel_num;
        }
    }

    public class STU_PEO_CITY : STU_RELATION {  /*人物所在位置*/
        public uint peo_id;
        public uint city_id;

        public STU_PEO_CITY(uint peo_id, uint city_id) : base(RELLISTYPE.TP_PEO_CITY) {
            this.relship_id = peo_id;
            this.peo_id = peo_id;
            this.city_id = city_id;
        }

        public STU_PEO_CITY(STU_PEO_CITY temp) : base(temp) {
            this.peo_id = temp.peo_id;
            this.city_id = temp.city_id;
        }
    }   /*人物所在地*/

    public class STU_PEO_REL : STU_RELATION { /*人物宗教关系*/
        public uint peo_id;
        public uint rel_id;
        public List<SUBSTU_REL_RAT> rel_rat;


        public STU_PEO_REL(uint peo_id, uint rel_id, List<SUBSTU_REL_RAT> rel_rat) : base(RELLISTYPE.TP_PEO_REL) {
            this.peo_id = peo_id;
            this.rel_id = rel_id;
            this.rel_rat = rel_rat;
            this.relship_id = peo_id;
        }

        public STU_PEO_REL(STU_PEO_REL temp) : base(temp) {
            this.peo_id = temp.peo_id;
            this.rel_id = temp.rel_id;
            this.rel_rat = new List<SUBSTU_REL_RAT>(temp.rel_rat);

        }
    }  /*人物信仰*/

    public class STU_CITY_INF : STU_RELATION { /*城市宗教关系*/
        public uint city_id;
        public int city_peonum; /*城市人口*/
        public List<SUBSTU_REL_RAT> rel_rat;


        public STU_CITY_INF(uint city_id, int city_peonum, List<SUBSTU_REL_RAT> rel_rat) : base(RELLISTYPE.TP_CITY_INF) {
            this.city_id = city_id;
            this.city_peonum = city_peonum;
            this.rel_rat = rel_rat;
            this.relship_id = city_id;
        }
        public STU_CITY_INF(STU_CITY_INF temp) : base(temp) {
            this.city_id = temp.city_id;
            this.city_peonum = temp.city_peonum;
            this.rel_rat = new List<SUBSTU_REL_RAT>(temp.rel_rat);
        }

        public int GetRelNum(uint rel_id) {
            int index = rel_rat.FindIndex(delegate (SUBSTU_REL_RAT substu) { return substu.rel_id == rel_id; });
            if (index == -1) return 0;
            else return rel_rat[index].rel_num;
        }
    } /*城市信仰*/

    public class STU_CITY_COUNTRY : STU_RELATION {
        public uint city_id;
        public uint country_id;

        public STU_CITY_COUNTRY(uint city_id, uint country_id) : base(RELLISTYPE.TP_CITY_COUNTRY) {
            this.city_id = city_id;
            this.country_id = country_id;
            this.relship_id = city_id;
        }

        public STU_CITY_COUNTRY(STU_CITY_COUNTRY temp) : base(temp) {
            this.city_id = temp.city_id;
            this.country_id = temp.country_id;
        }
    } /*国家城市归属*/

    public class STU_CITY_BUILDING:STU_RELATION {
        public uint city_id;   
        public uint building_id;
        public uint rel_belong_id;

        public STU_CITY_BUILDING(uint city_id,uint building_id,uint rel_belong_id):base(RELLISTYPE.TP_CITY_BUILDING) {
            this.city_id = city_id;
            this.building_id = building_id;
            this.rel_belong_id = rel_belong_id;
            this.relship_id = city_id;
        }

        public STU_CITY_BUILDING(STU_CITY_BUILDING temp) : base(temp) {
            this.city_id = temp.city_id;
            this.building_id = temp.building_id;
            this.rel_belong_id = temp.rel_belong_id;
        }
    }  /*城市建筑*/

    public class STU_REL_CANON:STU_RELATION {
        public uint rel_id;
        public uint canon_id;

        public STU_REL_CANON(uint rel_id,uint canon_id) : base(RELLISTYPE.TP_REL_CANON) {
            this.rel_id = rel_id;
            this.canon_id = canon_id;
            this.relship_id = rel_id;
        }

        public STU_REL_CANON(STU_REL_CANON temp) : base(temp) {
            this.rel_id = temp.rel_id;
            this.canon_id = temp.canon_id;
        }
    }/*宗教教规*/




    //public class lc_ObjRel {
    //    private const int MAXRATNUM = 100;
    //    public const uint DEFREL = 500;
    //    public const int MAXRELLISTNUM = 4;

    //    public lc_ObjRel() {
    //        objrel_list = new List<STU_RELATION>[MAXRELLISTNUM];
    //        for (int i = 0; i < MAXRELLISTNUM; i++) {
    //            objrel_list[i] = new List<STU_RELATION>();
    //        }

    //        //stu_peo_city = new List<STU_PEO_CITY>();
    //        //stu_peo_rel = new List<STU_PEO_REL>();
    //        //stu_city_rel = new List<STU_CITY_INF>();
    //        //stu_city_country = new List<STU_CITY_COUNTRY>();

    //    }

    //    private List<STU_RELATION>[] objrel_list;

    //    //private List<STU_PEO_CITY> stu_peo_city;
    //    //private List<STU_PEO_REL> stu_peo_rel;
    //    //private List<STU_CITY_INF> stu_city_rel;
    //    //private List<STU_CITY_COUNTRY> stu_city_country;


    //    public void SetPeoCity(uint peo_id, uint city_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_PEO_CITY].FindIndex(delegate (STU_RELATION temp) {
    //            STU_PEO_CITY substu = (STU_PEO_CITY)temp;
    //            if (substu.peo_id == peo_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) {
    //            objrel_list[(int)RELLISTYPE.TP_PEO_CITY].Add(new STU_PEO_CITY(peo_id, city_id));
    //        }
    //        else {
    //            STU_PEO_CITY substu = (STU_PEO_CITY)objrel_list[(int)RELLISTYPE.TP_PEO_CITY][index];
    //            substu.city_id = city_id;
    //        }
    //    } /*设置人物所在地*/
    //    public int SetPeoRel(uint peo_id, uint rel_id, int rel_num) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_PEO_REL].FindIndex(delegate (STU_RELATION temp) {
    //            STU_PEO_REL substu = (STU_PEO_REL)temp;
    //            if (substu.peo_id == peo_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) {
    //            List<SUBSTU_REL_RAT> list_rel_rat = new List<SUBSTU_REL_RAT>();
    //            list_rel_rat.Add(new SUBSTU_REL_RAT(rel_id, rel_num));
    //            objrel_list[(int)RELLISTYPE.TP_PEO_REL].Add(new STU_PEO_REL(peo_id, rel_id, list_rel_rat));
    //            return 1;  /*1-新增人物*/
    //        }
    //        else {
    //            STU_PEO_REL substu = (STU_PEO_REL)objrel_list[(int)RELLISTYPE.TP_PEO_REL][index];
    //            int subindex = substu.rel_rat.FindIndex(delegate (SUBSTU_REL_RAT temp) { return temp.rel_id == rel_id; });
    //            if (index == -1) {
    //                substu.rel_rat.Add(new SUBSTU_REL_RAT(rel_id, rel_num));
    //                return 2; /*新增宗教*/
    //            }
    //            else {
    //                substu.rel_rat[subindex].rel_num += rel_num;
    //                if (substu.rel_rat[subindex].rel_num >= MAXRATNUM) {
    //                    substu.rel_id = substu.rel_rat[subindex].rel_id;
    //                    return 4;/*该人物信仰改变*/
    //                }
    //                else {
    //                    return 3; /*增加宗教偏好*/
    //                }

    //            }
    //        }
    //    } /*设置人物信仰*/
    //    public void SetCityPeoNum(uint city_id, int city_peonum) {  /*设置城市人数*/
    //        int index = objrel_list[(int)RELLISTYPE.TP_CITY_INF].FindIndex(delegate (STU_RELATION temp) {
    //            STU_CITY_INF substu = (STU_CITY_INF)temp;
    //            if (substu.city_id == city_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) {
    //            List<SUBSTU_REL_RAT> list_rel_rat = new List<SUBSTU_REL_RAT>();
    //            list_rel_rat.Add(new SUBSTU_REL_RAT(DEFREL, city_peonum));
    //            objrel_list[(int)RELLISTYPE.TP_CITY_INF].Add(new STU_CITY_INF(city_id, city_peonum, list_rel_rat));
    //        }
    //        else { //调整每个子模块数量
    //            STU_CITY_INF substu = (STU_CITY_INF)objrel_list[(int)RELLISTYPE.TP_CITY_INF][index];
    //            int old_num = substu.city_peonum;
    //            int chg_sumnum = old_num - city_peonum;
    //            if (chg_sumnum < 0) {
    //                int defindex = substu.rel_rat.FindIndex(delegate (SUBSTU_REL_RAT temp) { return temp.rel_id == DEFREL; });
    //                int old_rel_num = substu.rel_rat[defindex].rel_num;
    //                substu.rel_rat[defindex].rel_num = old_rel_num + System.Math.Abs(chg_sumnum);
    //            }
    //            else if (chg_sumnum > 0) {
    //                int sumnum = substu.rel_rat.Sum(delegate (SUBSTU_REL_RAT temp) { return temp.rel_num; });
    //                for (int i = 0; i < substu.rel_rat.Count(); i++) {
    //                    int old_rel_num = substu.rel_rat[i].rel_num;
    //                    //int rat = (int)((float)old_rel_num / sumnum;
    //                    //Console.WriteLine("old_rel_num=" + old_rel_num);
    //                    //Console.WriteLine("sumnum=" + sumnum);
    //                    //Console.WriteLine("rat=" + rat);
    //                    //Console.WriteLine("DEBUG=" + (old_rel_num / sumnum).ToString());
    //                    substu.rel_rat[i].rel_num = old_rel_num - (int)(chg_sumnum * ((float)old_rel_num / sumnum));
    //                }
    //            }
    //            else { }

    //            substu.city_peonum = city_peonum;
    //        }
    //    }  /*设置城市人数*/
    //    public void SetCityRel(uint city_id, uint rel_id, int rel_num) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_CITY_INF].FindIndex(delegate (STU_RELATION temp) {
    //            STU_CITY_INF substu = (STU_CITY_INF)temp;
    //            if (substu.city_id == city_id) return true;
    //            else return false;
    //        });
    //        if (index != -1) {
    //            STU_CITY_INF substu = (STU_CITY_INF)objrel_list[(int)RELLISTYPE.TP_CITY_INF][index];
    //            int subindex = substu.rel_rat.FindIndex(delegate (SUBSTU_REL_RAT temp) { return temp.rel_id == rel_id; });
    //            if (subindex == -1) {
    //                substu.rel_rat.Add(new SUBSTU_REL_RAT(rel_id, rel_num));
    //            }
    //            else {
    //                substu.rel_rat[subindex].rel_num += rel_num;
    //            }
    //        }


    //    } /*设置城市信仰*/
    //    public void setCiytBel(uint city_id, uint country_id) {  /*设置城市归属*/
    //        int index = objrel_list[(int)RELLISTYPE.TP_CITY_COUNTRY].FindIndex(delegate (STU_RELATION temp) {
    //            STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)temp;
    //            if (substu.city_id == city_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) {
    //            objrel_list[(int)RELLISTYPE.TP_CITY_COUNTRY].Add(new STU_CITY_COUNTRY(city_id, country_id));
    //        }
    //        else {
    //            STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)objrel_list[(int)RELLISTYPE.TP_CITY_COUNTRY][index];
    //            substu.country_id = country_id;
    //        }
    //    } /*设置城市归属*/

    //    public uint GetPeoCity(uint peo_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_PEO_CITY].FindIndex(delegate (STU_RELATION temp) {
    //            STU_PEO_CITY substu = (STU_PEO_CITY)temp;
    //            if (substu.peo_id == peo_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return 0;
    //        else {
    //            STU_PEO_CITY substu = (STU_PEO_CITY)objrel_list[(int)RELLISTYPE.TP_PEO_CITY][index];
    //            return substu.city_id;
    //        }
    //    } /*获取人物所在地*/
    //    public STU_PEO_REL GetPeoRel(uint peo_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_PEO_REL].FindIndex(delegate (STU_RELATION temp) {
    //            STU_PEO_REL substu = (STU_PEO_REL)temp;
    //            if (substu.peo_id == peo_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return null;
    //        else {
    //            STU_PEO_REL substu = (STU_PEO_REL)objrel_list[(int)RELLISTYPE.TP_PEO_REL][index];
    //            return substu;
    //        }
    //    } /*获取人物信仰*/
    //    public STU_CITY_INF GetCityRel(uint city_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_CITY_INF].FindIndex(delegate (STU_RELATION temp) {
    //            STU_CITY_INF substu = (STU_CITY_INF)temp;
    //            if (substu.city_id == city_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return null;
    //        else {
    //            STU_CITY_INF substu = (STU_CITY_INF)objrel_list[(int)RELLISTYPE.TP_CITY_INF][index];
    //            return substu;
    //        }
    //    } /*获取城市信仰*/
    //    public STU_CITY_COUNTRY GetCityBel(uint city_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_CITY_COUNTRY].FindIndex(delegate (STU_RELATION temp) {
    //            STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)temp;
    //            if (substu.city_id == city_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return null;
    //        else {
    //            STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)objrel_list[(int)RELLISTYPE.TP_CITY_COUNTRY][index];
    //            return substu;
    //        }
    //    } /*获取城市归属*/

    //    public STU_PEO_CITY ViewPeoCity(uint peo_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_PEO_CITY].FindIndex(delegate (STU_RELATION temp) {
    //            STU_PEO_CITY substu = (STU_PEO_CITY)temp;
    //            if (substu.peo_id == peo_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return null;
    //        else {
    //            STU_PEO_CITY substu = (STU_PEO_CITY)objrel_list[(int)RELLISTYPE.TP_PEO_CITY][index];
    //            return new STU_PEO_CITY(substu);
    //        }
    //    } /*查询人物所在地*/
    //    public STU_PEO_REL ViewPeoRel(uint peo_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_PEO_REL].FindIndex(delegate (STU_RELATION temp) {
    //            STU_PEO_REL substu = (STU_PEO_REL)temp;
    //            if (substu.peo_id == peo_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return null;
    //        else {
    //            STU_PEO_REL substu = (STU_PEO_REL)objrel_list[(int)RELLISTYPE.TP_PEO_REL][index];
    //            return new STU_PEO_REL(substu);
    //        }
    //    } /*查询人物信仰*/
    //    public STU_CITY_INF ViewCityInfo(uint city_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_CITY_INF].FindIndex(delegate (STU_RELATION temp) {
    //            STU_CITY_INF substu = (STU_CITY_INF)temp;
    //            if (substu.city_id == city_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return null;
    //        else {
    //            STU_CITY_INF substu = (STU_CITY_INF)objrel_list[(int)RELLISTYPE.TP_CITY_INF][index];
    //            return new STU_CITY_INF(substu);
    //        }
    //    } /*查询城市信仰*/
    //    public STU_CITY_COUNTRY ViewCityCountry(uint city_id) {
    //        int index = objrel_list[(int)RELLISTYPE.TP_CITY_COUNTRY].FindIndex(delegate (STU_RELATION temp) {
    //            STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)temp;
    //            if (substu.city_id == city_id) return true;
    //            else return false;
    //        });
    //        if (index == -1) return null;
    //        else {
    //            STU_CITY_COUNTRY substu = (STU_CITY_COUNTRY)objrel_list[(int)RELLISTYPE.TP_CITY_COUNTRY][index];
    //            return new STU_CITY_COUNTRY(substu);
    //        }
    //    } /*查询城市归属*/

    //}
}
