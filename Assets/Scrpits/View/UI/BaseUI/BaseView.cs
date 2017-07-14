using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSpace {
    public abstract class BaseView : MonoBehaviour {

        public virtual void OnInit() {

        }

        public virtual void OnClear() {

        }

        public virtual void OnEnter(BaseContext context) {
            this.gameObject.SetActive(true);
        }//UI载入

        public virtual void OnExit(BaseContext context) {
            this.gameObject.SetActive(false);
        }//UI退出

        public virtual void OnPause(BaseContext context) {

        }//UI暂停

        public virtual void OnResume(BaseContext context) {

        }//UI重新加载

        public void DestroySelf() {
            Destroy(gameObject);
        }//UI销毁
    }
}