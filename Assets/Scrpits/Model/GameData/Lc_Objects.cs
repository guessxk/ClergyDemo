using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Reflection;

namespace GameSpace.GameCore {

    public class VarChg {
        public string instruction; //说明
        public string varname; //对应变量名
        public int chgnum;  //修改值
        public string objtype; //修改对象

        public VarChg(string inst, string varname, int chgnum, string objtype) {
            this.instruction = inst;
            this.varname = varname;
            this.chgnum = chgnum;
            this.objtype = objtype;
        }
    }//变量修改

    public abstract class lc_Object //抽象基类
    {
        public delegate void varChanged(uint obj_id, lc_Object.GAMEOBJTYPE obj_type, string varname, int varvalue);
        public event varChanged eventVarChanged;

        public enum GAMEOBJTYPE { PROFESSIONAL = 1, CANON, RELIGION, BUILDING, CITY, COUNTRY, SEER, PERSONALITY, ROUND,VOID }

        public uint obj_id { get; protected set; } /*编号*/
        public int  user_id { get; set; } /*自定义编号*/
        public string m_name { get; set; }/*名称*/
        public GAMEOBJTYPE obj_type; /*对象类型*/
        public string m_desc { get; set; }/*对象说明*/


        public static void setBeginNum(uint begin_id) {
            idCount = begin_id;
        }

        public static uint getBeginNum() {
            return idCount;
        }

        private static uint idCount;

        public lc_Object(string name, GAMEOBJTYPE obj_type) {
            this.obj_id = idCount++;
            this.m_name = name;
            this.obj_type = obj_type;
        }     //构造函数

        public lc_Object(lc_Object srcTag) {
            this.obj_id = srcTag.obj_id;
            this.m_name = srcTag.m_name;
            this.obj_type = srcTag.obj_type;
            this.m_desc = srcTag.m_desc;
        }//拷贝构造函数

        protected virtual void OnVarChanged(uint obj_id, lc_Object.GAMEOBJTYPE obj_type, string varname, int varvalue) {
            if (eventVarChanged != null) eventVarChanged(obj_id, obj_type, varname, varvalue);
        }//变量改变事件函数

        public void ChangeVar<T>(string varname,T varvalue) where T: struct {
            PropertyInfo proinfo = this.GetType().GetProperty(varname);
            if(proinfo != null) {
                T old_value = (T)proinfo.GetValue(this, null);
                proinfo.SetValue(this, varvalue, null);
            }
        }//改变变量值

    }

   

    public class lc_Professional : lc_Object {
        public lc_Professional(string name) : base(name, GAMEOBJTYPE.PROFESSIONAL) {

        }



    }//职业
    public class lc_Canon : lc_Object {   /*教规*/

        private int M_cost;

        public int m_cost { get { return M_cost; } set { M_cost = value; } } /*消耗点数*/
        public lc_Canon(string name) : base(name, GAMEOBJTYPE.CANON) {

        }
        public lc_Canon(string name, int cost, VarChg[] invarchg) : base(name, GAMEOBJTYPE.CANON) {
            this.chglist = new List<VarChg>();
            this.m_cost = cost;
            foreach (var temp in invarchg) {
                chglist.Add(new VarChg(temp.instruction, temp.varname, temp.chgnum,temp.objtype));
            }
        }//构造函数

        public lc_Canon(lc_Canon srcTag) : base(srcTag) {
            this.m_cost = srcTag.m_cost;
            this.chglist = new List<VarChg>();
            for (int i = 0; i < srcTag.chglist.Count; i++) {
                VarChg newvarchg = new VarChg(srcTag.chglist[i].instruction, srcTag.chglist[i].varname, srcTag.chglist[i].chgnum,srcTag.chglist[i].objtype);
                chglist.Add(newvarchg);

            }
        }//拷贝构造函数

        

        public List<VarChg> chglist; /*变量修改列表*/

        public void ExecEffect(object obj) {
            for (int i = 0; i < chglist.Count; i++) {
                PropertyInfo proinfo = obj.GetType().GetProperty(chglist[i].varname);
                int old_value = (int)proinfo.GetValue(obj, null);
                proinfo.SetValue(obj, old_value + chglist[i].chgnum,null);
            }

        }//执行生效效果
        public void ExecLose(object obj) {
            for (int i = 0; i < chglist.Count; i++) {
                PropertyInfo proinfo = obj.GetType().GetProperty(chglist[i].varname);
                int old_value = (int)proinfo.GetValue(obj, null);
                proinfo.SetValue(obj, old_value - chglist[i].chgnum,null);
            }
        }//执行失效效果

    } //教规

    //public class CanonEvent {
    //    public uint canon_id;
    //    public uint event_id;

    //    public CanonEvent(uint canon_id, uint event_id) {
    //        this.canon_id = canon_id;
    //        this.event_id = event_id;
    //    }
    //}//教规对应实例

    public class lc_Religion : lc_Object  /*宗教*/
    {

        //原始变量
        private int M_attraction;
        private int M_piety;
        private int M_organization;
        private int M_count;

        public lc_Religion(string name) : base(name, GAMEOBJTYPE.RELIGION) {
            //listCanonEvent = new List<CanonEvent>();
            listCanon = new List<uint>();
        }//构造函数

        public lc_Religion(lc_Religion srcTag) : base(srcTag) {
            this.build_date = srcTag.build_date;
            this.m_relType = srcTag.m_relType;
            this.m_attraction = srcTag.m_attraction;
            this.m_piety = srcTag.m_piety;
            this.m_organization = srcTag.m_organization;
            this.m_theorypt = srcTag.m_theorypt;
            this.leader_id = srcTag.leader_id;
            //listCanonEvent = new List<CanonEvent>();
            //for( int i =0; i < srcTag.listCanonEvent.Count; i++) {
            //    CanonEvent newcanonevent = new CanonEvent(srcTag.listCanonEvent[i].canon_id, srcTag.listCanonEvent[i].event_id);
            //    listCanonEvent.Add(newcanonevent);
            //}
            listCanon = new List<uint>(srcTag.listCanon.ToArray());

        }//拷贝构造函数

        //public lc_Religion() : base("无信仰", GAMEOBJTYPE.RELIGION) {
        //    this.obj_id = lc_ObjRel.DEFREL;
        //} //默认构造函数

        public int build_date;

        public int m_relType { get; set; } //宗教类型 1-一神 2-多神

        public int m_count { get; set; } //宗教人数

        public int m_attraction { get { return M_attraction; } set { if (value != M_attraction) { OnVarChanged(obj_id, obj_type, "m_attraction", value); } M_attraction = value; } }/*吸引力*/
        public int m_piety { get { return M_piety; } set { if (value != M_piety) { OnVarChanged(obj_id, obj_type, "m_piety", value); } M_piety = value; } } /*虔诚度*/
        public int m_organization { get { return M_organization; } set { if (value != M_organization) { OnVarChanged(obj_id, obj_type, "m_organization", value); } M_organization = value; } }/*组织度*/

        public int m_theorypt { get; set; } /*理论点数*/

        public uint leader_id; /*宗教领袖*/


        //public List<CanonEvent> listCanonEvent; /*教规*/

        public List<uint> listCanon; /*教规*/

        public int RelAddCanon(lc_Canon canon) {
            int index = listCanon.FindIndex(delegate (uint temp) { return temp == canon.obj_id; });
            if (index != -1) return -1;
            else {
                canon.ExecEffect(this);
                listCanon.Add(canon.obj_id);
                return 1;
            }
        }

        public int RelDelCanon(lc_Canon canon) {
            int index = listCanon.FindIndex(delegate (uint temp) { return temp == canon.obj_id; });
            if (index == -1) return -1;
            else {
                canon.ExecLose(this);
                listCanon.RemoveAt(index);
                return 1;
            }
        }

        //public int relAddCanon(uint canon_id,uint event_id) {
        //    int index = listCanonEvent.FindIndex(delegate (CanonEvent temp) { return temp.canon_id == canon_id; });
        //    if (index != -1) return -1;
        //    else {
        //        listCanonEvent.Add(new CanonEvent(canon_id, event_id));
        //        return 1;
        //    }
        //} //增加教规
        //public int relDelCanon(uint canon_id,uint event_id) {
        //    int index = listCanonEvent.FindIndex(delegate (CanonEvent temp) { if (temp.canon_id == canon_id && temp.event_id == event_id) return true; else return false; });
        //    if (index == -1) return -1;
        //    else {
        //        listCanonEvent.RemoveAt(index);
        //        return 1;
        //    }
        //} //删除教规

    } //宗教

    public class lc_Building : lc_Object { /*建筑*/

        public lc_Building(string name ) : base(name, GAMEOBJTYPE.BUILDING) {
            chglist = new List<VarChg>();
        }

        public lc_Building(lc_Building srcTag) : base(srcTag) {
            this.m_cost = srcTag.m_cost;
            chglist = new List<VarChg>();
            for (int i = 0; i < srcTag.chglist.Count; i++) {
                VarChg newvarchg = new VarChg(srcTag.chglist[i].instruction, srcTag.chglist[i].varname, srcTag.chglist[i].chgnum,srcTag.chglist[i].objtype);
                chglist.Add(newvarchg);
            }
        }//拷贝构造函数

        public int m_cost { get; set; } /*费用*/

        public bool m_belong { get; set; } /*是否归属*/

        public List<VarChg> chglist; /*变量修改列表*/

        //public void ExecEffect(object obj) {
        //    for (int i = 0; i < chglist.Count; i++) {
        //        PropertyInfo proinfo = obj.GetType().GetProperty(chglist[i].varname);
        //        int old_value = (int)proinfo.GetValue(obj, null);
        //        proinfo.SetValue(obj, old_value + chglist[i].chgnum,null);
        //    }
        //}

        //public void ExecFailure(object obj) {
        //    for (int i = 0; i < chglist.Count; i++) {
        //        PropertyInfo proinfo = obj.GetType().GetProperty(chglist[i].varname);
        //        int old_value = (int)proinfo.GetValue(obj, null);
        //        proinfo.SetValue(obj, old_value - chglist[i].chgnum,null);
        //    }
        //}//执行失效效果
    } //建筑

    //public class BuildingEvent {
    //    public uint building_id;
    //    public uint event_id;

    //    public BuildingEvent( uint building_id,uint event_id) {
    //        this.building_id = building_id;
    //        this.event_id = event_id;
    //    }
    //}//建筑对应实例

    public class lc_City : lc_Object { /*城市*/

        //原始变量
        //private int M_mapid;
        private int M_pop;
        private int M_ems;

        public lc_City(string name) : base(name, GAMEOBJTYPE.CITY) { }
        public lc_City(string name, int map_id) : base(name, GAMEOBJTYPE.CITY) {
           
           
        }//构造函数

        public lc_City(lc_City srcTag) : base(srcTag) {
     
            this.m_pop = srcTag.m_pop;
            this.m_ems = srcTag.m_ems;
          
        }//拷贝构造函数


        //public int m_mapid
        //{
        //    get { return M_mapid; }
        //    set { M_mapid = value; }
        //}/*对应MAP_ID*/

        public int m_pop
        {
            get { return M_pop; }
            set { if (value != M_pop) { OnVarChanged(obj_id, obj_type, "m_pop", value); } M_pop = value; }
        } /*人口*/
        public int m_ems
        {
            get { return M_ems; }
            set { if (value != M_ems) { OnVarChanged(obj_id, obj_type, "m_ems", value); } M_ems = value; }
        } /*经济*/



        //public int CityAddBuilding(lc_Building building) {
         
        //}//增加建筑

        //public int CityDelBuilding(lc_Building building) {
          
        //}
    } //城市

    public class lc_Country : lc_Object  /*国家*/
    {

        //原始变量
        private int M_military;
        private int M_crown;
        private int M_pop;
        private int M_ems;
        private int M_mapid;

        public lc_Country(string name) : base(name, GAMEOBJTYPE.COUNTRY) {
            //listCity = new List<uint>();
        }//构造函数

        public lc_Country(lc_Country srcTag) : base(srcTag) {

            this.m_military = srcTag.m_military; //军事力
            this.m_crown = srcTag.m_crown; //王权
        }

        public int m_military
        {
            get { return M_military; }
            set { if (value != M_military) { OnVarChanged(obj_id, obj_type, "m_military", value); } M_military = value; }
        }/*军事*/
        public int m_crown
        {
            get { return M_crown; }
            set { if (value != M_crown) { OnVarChanged(obj_id, obj_type, "m_crown", value); } M_crown = value; }
        } /*王权*/

        public int m_pop { get; set; }
        public int m_ems { get; set; }

        //public int m_mapid { get; set; }

        //public List<uint> listCity; /*城市列表*/




    } //国家

    public class lc_People : lc_Object /*人物*/
    {

        //原始变量
        private int M_age;
        private int M_life;
        private bool M_islife;

        public lc_People(string name) : base(name, GAMEOBJTYPE.VOID) { }

        public lc_People(string name, int age, int life, GAMEOBJTYPE obj_type) : base(name, obj_type) {
            this.m_age = age;
            this.m_life = life;
            this.m_islife = true;
        }//构造函数

        public lc_People(lc_People srcTag) : base(srcTag) {
            this.m_age = srcTag.m_age;
            this.m_life = srcTag.m_life;
            this.m_islife = srcTag.m_islife;
        }//拷贝构造函数



        public int m_bithdate; /*出生日期*/

        //public enum PERSONTYPES { SEER = 1, PERSONALITY = 2 };

        //public PERSONTYPES m_type; /*人物类型*/

        public int m_age
        {
            get { return M_age; }
            set { if (value != M_age) { OnVarChanged(obj_id, obj_type, "m_age", value); } M_age = value; }
        }/*当前年龄*/
        public int m_life
        {
            get { return M_life; }
            set { if (value != M_life) { OnVarChanged(obj_id, obj_type, "m_life", value); } M_life = value; }
        } /*寿命*/
        public bool m_islife
        {
            get { return M_islife; }
            set { if (value != M_islife) { OnVarChanged(obj_id, obj_type, "m_islife", value ? 1 : 0); } M_islife = value; }
        } /*是否存活*/

    } //人物 -基类

    public class lc_Seer : lc_People /*预言家*/
    {


        //原始变量
        private int M_eloquence;
        private int M_leadership;
        private int M_intelligence;
        private int M_willpower;
        private int M_wealth;
        private int M_fame;

        public lc_Seer(string name) : base(name) {
            obj_type = GAMEOBJTYPE.SEER;
        }

        public lc_Seer(string name, int age, int life) : base(name, age, life, GAMEOBJTYPE.SEER) {
            //m_type = PERSONTYPES.SEER;
        }

        public lc_Seer(lc_Seer srcTag) : base(srcTag) {
            //this.m_type = srcTag.m_type;
            this.m_eloquence = srcTag.m_eloquence;
            this.m_leadership = srcTag.m_leadership;
            this.m_intelligence = srcTag.m_intelligence;
            this.m_willpower = srcTag.m_willpower;
            this.m_wealth = srcTag.m_wealth;
            this.m_fame = srcTag.m_fame;
        }

        public int m_eloquence
        {
            get { return M_eloquence; }
            set { if (value != M_eloquence) { OnVarChanged(obj_id, obj_type, "m_eloquence", value); } M_eloquence = value; }
        }/*口才*/
        public int m_leadership
        {
            get { return M_leadership; }
            set { if (value != M_leadership) { OnVarChanged(obj_id, obj_type, "m_leadership", value); } M_leadership = value; }
        }/*领导力*/
        public int m_intelligence
        {
            get { return M_intelligence; }
            set { if (value != M_intelligence) { OnVarChanged(obj_id, obj_type, "m_intelligence", value); } M_intelligence = value; }
        }/*智力*/
        public int m_willpower
        {
            get { return M_willpower; }
            set { if (value != M_willpower) { OnVarChanged(obj_id, obj_type, "m_willpower", value); } M_willpower = value; }
        } /*意志力*/

        public int m_wealth
        {
            get { return M_wealth; }
            set { if (value != M_wealth) { OnVarChanged(obj_id, obj_type, "m_wealth", value); } M_wealth = value; }
        }  /*个人财富*/
        public int m_fame
        {
            get { return M_fame; }
            set { if (value != M_fame) { OnVarChanged(obj_id, obj_type, "m_fame", value); } M_fame = value; }
        }/*名声*/

    } //传教士

    public class lc_Personality : lc_People { /*名人*/


        //原始变量
        private int M_infl;
        private int M_risist;

        public lc_Personality(string name) : base(name) {
            obj_type = GAMEOBJTYPE.PERSONALITY;
        }
        public lc_Personality(string name, int age, int life) : base(name, age, life, GAMEOBJTYPE.PERSONALITY) {
            //m_type = PERSONTYPES.PERSONALITY;
        } //构造函数

        public lc_Personality(lc_Personality srcTag) : base(srcTag) {
            //this.m_type = srcTag.m_type;
            this.belong_cty = srcTag.belong_cty;
            this.m_infl = srcTag.m_infl;
            this.m_profession = srcTag.m_profession;
            this.m_risist = srcTag.m_risist;
        } //拷贝构造函数

        public uint belong_cty { get; set; } /*所属国家*/
        public uint m_profession { get; set; }/*职业*/

        public int m_infl
        {
            get { return M_infl; }
            set { if (value != M_infl) { OnVarChanged(obj_id, obj_type, "m_infl", value); } M_infl = value; }
        }/*政治影响力*/
        public int m_risist
        {
            get { return M_risist; }
            set { if (value != M_risist) { OnVarChanged(obj_id, obj_type, "m_risist", value); } M_risist = value; }
        }/*抗拒度*/
    } //名人
}

