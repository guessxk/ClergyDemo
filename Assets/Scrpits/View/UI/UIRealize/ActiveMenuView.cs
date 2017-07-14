using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSpace.GameCore;
using SceneExtend;
using GameSpace;
using UnityEngine.SceneManagement;

namespace GameSpace {
    public class ActiveMenuContext : BaseContext {
        public ActiveMenuContext() : base(UIType.ActiveMenu) {

        }
    }


    public class ActiveMenuView : BaseView {

        

        public override void OnEnter(BaseContext context) {
            Debug.Log("activemenuview enter ,cur scnen=["+SceneManager.GetActiveScene().name+"]");
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context) {
            Debug.Log("activemenuview exit");

            base.OnExit(context);
        }

        public override void OnPause(BaseContext context) {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context) {
            base.OnResume(context);
        }


        public void OnButton00Call() {
            lc_ObjViewer objviewer = MainMapScene.Instance.objviewer;
            uint playerid = objviewer.ViewPlayerId();
            lc_Seer seer = objviewer.ViewObject<lc_Seer>(playerid);
            if (seer != null) {
                Singleton<ContextManager>.Instance.Push(new LeadPanelContext(seer));
            }
        }

        public void OnButton01Call() {
            lc_ObjViewer objviewer = MainMapScene.Instance.objviewer;
            uint playerRel_id = objviewer.ViewPlayerRel();
            lc_Religion rel = objviewer.ViewObject<lc_Religion>(playerRel_id);
            int rel_count = objviewer.ViewRelCount(playerRel_id);
            rel.m_count = rel_count;
            if (rel != null) {
                Singleton<ContextManager>.Instance.Push(new RelPanelContext(rel));
            }

        }

        public void OnButton02Call() {
   
            lc_ObjViewer objviewer = MainMapScene.Instance.objviewer;
            int pop = 0, ems = 0;
     

            lc_Country[] countrys = objviewer.viewAllObject<lc_Country>();
            Debug.Log("length=[" + countrys.Length + "]");
            for (int i = 0; i < countrys.Length; i++) {
                pop = 0; ems = 0;
                List<uint> citys = objviewer.ViewCountryCity(countrys[i].obj_id);
                Debug.Log("country[" + countrys[i].m_name + "] have [" + citys.Count + "] citys");
                for (int j = 0; j < citys.Count; j++) {                 
                    lc_City city = objviewer.ViewObject<lc_City>(citys[j]);
                    STU_CITY_INF citystu = objviewer.viewRelShep<STU_CITY_INF>(city.obj_id);
                    pop += citystu.city_peonum;
                    ems += city.m_ems;
                    Debug.Log("city["+city.m_name+"] pop=" + city.m_pop);
                }
                Debug.Log("country[" + countrys[i].m_name + "] pop=[" + pop + "]");
                countrys[i].m_pop = pop;
                countrys[i].m_ems = ems;
            }

            if (countrys != null) {
                Singleton<ContextManager>.Instance.Push(new CountryPanelContext(countrys));
            }

        }
    }
}
