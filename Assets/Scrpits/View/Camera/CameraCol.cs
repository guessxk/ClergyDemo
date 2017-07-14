using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CameraCol : MonoBehaviour {

    // Use this for initialization
    //控制视野缩放的速率
    public float view_value = 100f;
    //控制摄像机移动的速率
    public float move_speed = 0.25f;
    //拖动开关
    private bool DragAble = true;
    public GameObject PopView;

    public GraphicRaycaster graphicraycaster;


    private float screenWidth; //屏幕宽度
    private float screenHeight; //屏幕高度

    private Vector3 offset;
    private Vector3 Woffset;
    private Vector3 lastPos;
    private Vector3 newPos;
    private Vector3 WlastPos;
    private Vector3 WnewPos;


    private Rect mapRect;
    private Rect curRect;
    private Camera curcamear;
    void Awake() {
        Debug.Log("camera awake");
       
    }
    void Start() {
        Debug.Log("camera start");
        curcamear = this.GetComponent<Camera>();
        //增加对地图初始化事件监控
        MainMapScene.Instance.MapSceneMsgCenter.registerObserver(NotifyType.MSG_SYS_MAPINITED, OnMapInited);
    }

    // Update is called once per frame
    void Update() {



    }

    void LateUpdate() {        
        CamearDrag();//鼠标拖动
    }

    void OnDestroy() {
        //删除地图初始化事件监控
        //MapSceneManager.Instance.MapSceneMsgCenter.removeObserver(NotifyType.MSG_SYS_MAPINITED, OnMapInited);
    }



    private void OnMapInited(NotifyEvent evt) {
        string x1, x2, y1, y2;

        evt.Params.TryGetValue("mapX1", out x1);
        evt.Params.TryGetValue("mapX2", out x2);
        evt.Params.TryGetValue("mapY1", out y1);
        evt.Params.TryGetValue("mapY2", out y2);

        //镜头置中
        mapRect = new Rect(new Vector2(float.Parse(x1), float.Parse(y1)), new Vector2(float.Parse(x2), float.Parse(y2)));
        curcamear.transform.position = new Vector3(mapRect.center.x, mapRect.center.y, -100);
       
        screenHeight = curcamear.orthographicSize * 2;
        screenWidth = screenHeight * curcamear.aspect;

    } //地图初始化完成事件

    private void CamearDrag() {
        //需判断是否在UI上，在UI则无法拖动
        if (DragAble == true/* && !EventSystem.current.IsPointerOverGameObject()*/) {
            Vector3 camPos = curcamear.transform.position;
            if (Input.GetMouseButton(0) && !CheckGuiRaycastObjects()) {
                Woffset.x = Input.GetAxis("Mouse X") * move_speed * Time.deltaTime;
                Woffset.y = Input.GetAxis("Mouse Y") * move_speed * Time.deltaTime;
                Woffset.z = 0;

                //计算拖动后摄像机位置，控制范围在地图边线内
                curcamear.transform.position = new Vector3(Mathf.Clamp(camPos.x - Woffset.x, mapRect.x+screenWidth/2, mapRect.x + mapRect.width-screenWidth/2), Mathf.Clamp(camPos.y - Woffset.y, mapRect.y + mapRect.height+screenHeight/2, mapRect.y-screenHeight/2), camPos.z);
            }

        }

    }//鼠标拖动镜头

    private bool CheckGuiRaycastObjects()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;

 

        List<RaycastResult> list = new List<RaycastResult>();
        graphicraycaster.Raycast(eventData, list);
        //Debug.Log(list.Count);
        return list.Count > 0;
    }//使用射线检查当前鼠标是否在UI raycaster上
}
