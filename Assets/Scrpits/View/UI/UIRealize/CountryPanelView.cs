using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSpace.GameCore;


namespace GameSpace {
    public class CountryPanelContext : BaseContext {

        public lc_Country[] countrys;
       
        public CountryPanelContext(lc_Country[] countrys) : base(UIType.CountryPanel) {
            this.countrys = countrys;
        }
    }


    public class CountryPanelView : BaseView {
        public GameObject ItemObject;
        public  Transform content;

     

        public override void OnEnter(BaseContext context) {
            RemoveItem();
            CountryPanelContext countrypanelcontext = (CountryPanelContext)context;
            AddItem(countrypanelcontext.countrys);
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context) {
            RemoveItem();
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context) {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context) {
            base.OnResume(context);
        }


        private void RemoveItem() {
            for(int i=0;i< content.childCount; i++) {
                Destroy(content.GetChild(i).gameObject);
            }
            //while (content.childCount > 0) {
            //    GameObject toRemove = content.GetChild(0).gameObject;
            //    Destroy(toRemove);
            //}
        }

        private void AddItem(lc_Country[] countrys) {
            for (int i = 0; i < countrys.Length; i++) {
                GameObject Item = Instantiate<GameObject>(ItemObject) as GameObject;
                Item.transform.SetParent(content,false);
                Item.SetActive(true);
                CountryPanelItem ItemInst = Item.GetComponent<CountryPanelItem>();
                ItemInst.ValueInit(countrys[i]);
            }
        }

        public void OnCloseCallBack() {
            Singleton<ContextManager>.Instance.Pop();
        }

    }


}