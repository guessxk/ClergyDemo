using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneExtend;
public class MainmenuView : MonoBehaviour {

    private AsyncOperation mAsyncOperation;


     void Awake() {
        Debug.Log("MainMenuView awake");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void OnClickButton00() {
        //SceneManager.sceneLoaded += sceneLoad;
        //SceneManager.LoadScene("MainMapScene");

        SceneManagerExpend.Instance.LoadScene(MainMapScene.Instance);
    
    }

    public void OnClickButton01() {
        Application.Quit();
    }

    //private void sceneLoad(Scene scence, LoadSceneMode mod) {
    //    MapSceneManager.Create();
    //    SceneManager.sceneLoaded -= sceneLoad;
    //}
}
