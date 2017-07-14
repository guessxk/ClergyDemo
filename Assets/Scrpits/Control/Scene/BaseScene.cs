using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SceneExtend {

    public enum SceneLoadType { COMMON,ASYN }

    public enum SceneLoadMode { SINGLE,ADD }

    public abstract class BaseScene {

        public string SceneName { get; set; }

        public SceneLoadType loadtype;
        public SceneLoadMode loadmode;

        public BaseScene(SceneType scenetype,SceneLoadType loadtype,SceneLoadMode loadmode) {
            SceneName = scenetype.sceneName;
            this.loadtype = loadtype;
            this.loadmode = loadmode;
        }

        public virtual void OnSceneLoaded() {

        }

        public virtual void OnSceneUnLoaded() {

        }
    }
}
