using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneExtend;

namespace GameSpace {
    public class OptionPanelContext : BaseContext {
        public OptionPanelContext() : base(UIType.OptionPanel) {

        }
    }

    public class OptionPanelView : BaseView {

        private AsyncOperation mAsyncOperation;
        public override void OnEnter(BaseContext context) {
            base.OnEnter(context);
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

        public void OnButton00Call() {
            SceneManagerExpend.Instance.LoadScene(MainMenuScene.Instance);
            //SceneManager.LoadScene("MainMenuScene");
            //MapSceneManager.SceneClear();
        }

    }
}
