using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneType {

    public string sceneName;

    public SceneType(string name) {
        sceneName = name;
    }

    public static readonly SceneType MainMenuScene = new SceneType("MainMenuScene");
    public static readonly SceneType MainMapScene = new SceneType("MainMapScene");

}
