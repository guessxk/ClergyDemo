using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneExtend;
using GameSpace;
using UnityEngine.SceneManagement;
public class GameRoot{

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod() {
        Debug.Log("Before first scene loaded");
      
        SceneManagerExpend.Instance.LoadScene(MainMenuScene.Instance);
    }

}
