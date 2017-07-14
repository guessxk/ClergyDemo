using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSpace;
using GameSpace.GameCore;
using UnityEngine.UI;


namespace GameSpace {


    public class CityInfoSignContext : BaseContext {
        public CityInfoSignContext() : base(UIType.CityInfoSign) {
        }


    }


    public class CityInfoSignView : BaseView {

        private lc_ObjManager objmanager;
        public override void OnEnter(BaseContext context) {
            base.OnEnter(context);
            //lc_ObjGather objgather = Singleton<lc_ObjGather>.Instance;
            //if (objgather != null) {
            //    tmpmanager = new lc_ObjManager(objgather);
            //}
            objmanager = MainMapScene.Instance.objmanager;
            MainMapScene.Instance.MapSceneMsgCenter.registerObserver(NotifyType.MSG_UI_TILEMOUSEIN, OnMouseIn);
            MainMapScene.Instance.MapSceneMsgCenter.registerObserver(NotifyType.MSG_UI_TILEMOUSEOUT, OnMouseOut);
            this.gameObject.SetActive(false);

        }

        public override void OnExit(BaseContext context) {
            base.OnExit(context);
            MainMapScene.Instance.MapSceneMsgCenter.removeObserver(NotifyType.MSG_UI_TILEMOUSEIN, OnMouseIn);
            MainMapScene.Instance.MapSceneMsgCenter.removeObserver(NotifyType.MSG_UI_TILEMOUSEIN, OnMouseOut);
        }

        public override void OnPause(BaseContext context) {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context) {
            base.OnResume(context);
        }

        private void OnMouseIn(NotifyEvent evt) {
            //Debug.Log("Mouse IN");
            string strobjid;
            uint obj_id = 0;

            this.gameObject.SetActive(true);
            evt.Params.TryGetValue("OBJID", out strobjid);
            obj_id = (uint)int.Parse(strobjid);
            lc_City city = objmanager.GetObject<lc_City>(obj_id);
            STU_CITY_INF city_inf = objmanager.GetRelShip<STU_CITY_INF>(obj_id);
            city.m_pop = city_inf.city_peonum;
            ShowData(city);
        }

        private void OnMouseOut(NotifyEvent evt) {
            //Debug.Log("Mouse Out");
            this.gameObject.SetActive(false);
        }

        private void ShowData(lc_City city) {
            Transform transform = this.gameObject.transform;

            transform.Find("NAME_VALUE").GetComponent<Text>().text = city.m_name;
            transform.Find("POP_VALUE").GetComponent<Text>().text = city.m_pop.ToString();
            transform.Find("EMS_VALUE").GetComponent<Text>().text = city.m_ems.ToString();

        }
    }



   
}
