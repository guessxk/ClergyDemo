using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class listDownUi : MonoBehaviour {

    private List<GameObject> Candidatelist;


     void Awake() {
        Candidatelist = new List<GameObject>();
    }

    // Use this for initialization
    void Start () {
         
        Candidatelist.Add(GameObject.Find("Candidate0"));
        Candidatelist.Add(GameObject.Find("Candidate1"));
        Candidatelist.Add(GameObject.Find("Candidate2"));

        for( int i=0;i < Candidatelist.Count; i++) {
            Candidatelist[i].SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Button_OnClick(int id_bt) {
        //Candidate.SetActive(!Candidate.activeSelf);//如果候选项是隐藏的，就弄到显示  
        //if (id_bt >= 0) {
        //    if( activeButtonId != -1) {   //折叠其他下拉框
        //        Candidatelist[activeButtonId].SetActive(false);
        //    }

        //    Candidatelist[id_bt].SetActive(!Candidatelist[id_bt].activeSelf);
        //    Debug.Log(Candidatelist[id_bt].activeSelf);
        //    if (Candidatelist[id_bt].activeSelf)
        //        activeButtonId = id_bt;
        //    else
        //        activeButtonId = -1;
        //}

        for( int i = 0; i < Candidatelist.Count; i++) {
            if (i == id_bt)
                Candidatelist[i].SetActive(!Candidatelist[i].activeSelf);
            else
                Candidatelist[i].SetActive(false);
        } 
    }
}
