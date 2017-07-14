using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using SceneExtend;

public class hexcell : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler {

    public  int city_id; //城市编号
    public  uint obj_id; 

    public enum MAP_TYPE{ VOID,PLAIN, WATER, MOUNTAIN,DESERT,FOREST,MARSH,DIRT,HILL,OASIS,FARM,DIRTCASTLE,DIRTVILLAGE,DIRTVILLAGES,PLANSVALLAGE,PLANSVALLAGES, DIRTWALLEDCITY, PLANSCASTLE,BASE }

    public MAP_TYPE type;
	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        Debug.Log("CITYID=[" + city_id + "]");
        dict.Add("CITYID", city_id.ToString());
        dict.Add("OBJID", obj_id.ToString());
        NotifyEvent evt = new NotifyEvent(NotifyType.MSG_UI_TILECLICKED,dict, this);
        MainMapScene.Instance.MapSceneMsgCenter.postNotification(evt);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        //Debug.Log("OnPointerEnter");
        Dictionary<string, string> dict = new Dictionary<string, string>();
        dict.Add("CITYID", city_id.ToString());
        dict.Add("OBJID", obj_id.ToString());
        NotifyEvent evt = new NotifyEvent(NotifyType.MSG_UI_TILEMOUSEIN, dict, this);
        MainMapScene.Instance.MapSceneMsgCenter.postNotification(evt);
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Debug.Log("OnPointerOut");
        NotifyEvent evt = new NotifyEvent(NotifyType.MSG_UI_TILEMOUSEOUT, this);
        MainMapScene.Instance.MapSceneMsgCenter.postNotification(evt);
    }
}
