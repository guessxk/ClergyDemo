using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace GameSpace {
    public class PopInterFacePanelContext : BaseContext {
        public PopInterFacePanelContext() : base(UIType.PopInterFacePanel) {
        }

        
    }

    public class PopInterFacePanelView : BaseView {

        private int city_id;   

        public override void OnEnter(BaseContext context) {
            base.OnEnter(context);
            this.gameObject.SetActive(false);
            MainMapScene.Instance.MapSceneMsgCenter.registerObserver(NotifyType.MSG_UI_TILECLICKED, OnTileClicked);

        }

        public override void OnExit(BaseContext context) {
            base.OnExit(context);
            MainMapScene.Instance.MapSceneMsgCenter.removeObserver(NotifyType.MSG_UI_TILECLICKED, OnTileClicked);

        }

        public override void OnPause(BaseContext context) {
            base.OnPause(context);
            this.gameObject.SetActive(false);
        }

        public override void OnResume(BaseContext context) {
            base.OnResume(context);
        }

        public void OnButton00Call() {
            if (city_id > 0) {
                Singleton<ContextManager>.Instance.Push(new CityInContext(city_id));
            }
        }

        public void OnButton01Call() {

        }

        private void OnMapInited(NotifyEvent evt) {

        }

        private void OnTileClicked(NotifyEvent evt) {
            
            int cityid = 0;
            string strcityid;
            evt.Params.TryGetValue("CITYID", out strcityid);
           
            cityid = int.Parse(strcityid);
            //Debug.Log("ONtileClicked cityid=[" + cityid + "]");
            if (cityid >= 0) {
                this.gameObject.SetActive(true);
                this.city_id = cityid;
            }
            else {
                this.gameObject.SetActive(false);
            }

        }


      
    }

  

}
