using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneExtend {
    public class SceneManagerExpend {

        private AsyncOperation mAsyncoperation;

        private static SceneManagerExpend _instance=new SceneManagerExpend();

        private BaseScene loadScene;
        private BaseScene curScene;


        public SceneManagerExpend() {
   
        }

        public static SceneManagerExpend Instance
        {
            get {
                if( _instance == null) {
                    _instance = new SceneManagerExpend();
                }

                return _instance;
            }
        }

        public void LoadScene(BaseScene basescene) {
            Debug.Log("load scene=[" + basescene.SceneName + "]");
            loadScene = basescene;
            SceneManager.sceneLoaded += LoadCommon; 
            if(curScene != null) {
                SceneManager.sceneUnloaded += UnLoadCommon;
            }
            SceneManager.LoadSceneAsync(basescene.SceneName);

        }

        public void UnLoadScene(BaseScene basescene) {
            SceneManager.sceneUnloaded += UnLoadCommon;
            SceneManager.UnloadSceneAsync(basescene.SceneName);
            
        }

        private void LoadCommon(Scene scence, LoadSceneMode mod) {          
            if (loadScene != null) {
                Debug.Log("loadscene name=[" + loadScene.SceneName + "]");
                loadScene.OnSceneLoaded();
                curScene = loadScene;
                SceneManager.sceneLoaded -= LoadCommon;
            }
        }

        private void UnLoadCommon(Scene scene) {            
            if (curScene != null) {
                Debug.Log("unloadscene name=[" + curScene.SceneName + "]");
                curScene.OnSceneUnLoaded();
                SceneManager.sceneUnloaded -= UnLoadCommon;
                Debug.Log("111111");
            }

        }
    }
}
