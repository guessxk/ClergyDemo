using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSpace;
using GameSpace.GameCore;
using UnityEngine.UI;


namespace GameSpace {


    public class CityInContext : BaseContext {

        public int map_id;
        public CityInContext(int map_id) : base(UIType.CityIn) {
            this.map_id = map_id;
        }
    }
    public class CityInView : BaseView {

        public Transform tr_buildingPanel;

        public override void OnInit() {
            tr_buildingPanel = gameObject.transform.Find("CityPic/buildingPanel").transform;
            foreach(Transform tr_child in tr_buildingPanel) {
                tr_child.gameObject.SetActive(false);
            }
            Debug.Log("ONINIT");
        }

        public override void OnEnter(BaseContext context) {           
            base.OnEnter(context);
            CityInContext cityContext = (CityInContext)context;
            lc_City city= MainMapScene.Instance.objviewer.ViewObjectByUserId<lc_City>(cityContext.map_id);
            List<STU_CITY_BUILDING> stulist = MainMapScene.Instance.objviewer.viewAllRelShep<STU_CITY_BUILDING>(city.obj_id);
            Debug.Log("city " + city.m_name + " has [" + stulist.Count + "] building");
            for(int i = 0; i < stulist.Count; i++) {
                lc_Building building = MainMapScene.Instance.objviewer.ViewObject<lc_Building>(stulist[i].building_id);
                Debug.Log("BUILDING NAME=[" + building.m_name + "]");
            }
            Debug.Log(cityContext.map_id);
            ShowData(city);
        }

        public override void OnExit(BaseContext context) {
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context) {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context) {
            base.OnResume(context);
        }

        private void ShowData(lc_City city) {
            Transform transform = this.gameObject.transform;

            transform.Find("StaticMenu/NAME_VALUE").GetComponent<Text>().text = city.m_name;
            transform.Find("StaticMenu/POP_VALUE").GetComponent<Text>().text = city.m_pop.ToString();
            transform.Find("StaticMenu/EMS_VALUE").GetComponent<Text>().text = city.m_ems.ToString();
        }

      

        public void OnCloseCallBack() {
            Singleton<ContextManager>.Instance.Pop();
        }
    }
}
