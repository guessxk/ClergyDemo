using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum NotifyType {
    MSG_SYS_MAPSCENEINITED,
    MSG_SYS_MAPINITED,  //地图初始化完成

    MSG_UI_TILECLICKED,//地图板块点击
    MSG_UI_TILEMOUSEIN,//地图板块鼠标进入
    MSG_UI_TILEMOUSEOUT, //地图板块鼠标移出
    MSG_GAME_PALYERMOVE,
}// 消息的类型  

public class NotifyEvent {
    protected Dictionary<string, string> arguments;  //参数  
    protected NotifyType type;  //事件类型  
    protected System.Object sender;    //发送者  

    // bean函数  
    public NotifyType Type
    {
        get { return type; }
        set { type = value; }
    }  //类型
    public Dictionary<string, string> Params
    {
        get { return arguments; }
        set { arguments = value; }
    } //参数
    public System.Object Sender
    {
        get { return sender; }
        set { sender = value; }
    } //发送者

    // 常用函数  
    public override string ToString() {
        return type + " [ " + ((sender == null) ? "null" : sender.ToString()) + " ] ";
    }

    public NotifyEvent Clone() {
        return new NotifyEvent(type, arguments, sender);
    }

    
    public NotifyEvent(NotifyType type, System.Object sender) {
        Type = type;
        Sender = sender;
        if (arguments == null) {
            arguments = new Dictionary<string, string>();
        }
    }// 构造函数  

    public NotifyEvent(NotifyType type, Dictionary<string, string> args, System.Object sender) {
        Type = type;
        arguments = args;
        Sender = sender;
        if (arguments == null) {
            arguments = new Dictionary<string, string>();
        }
    }// 构造函数  
}// 消息事件类，使用中传递的信息  

// 消息监听者，这是一个delegate，也就是一个函数，当事件触发时，对应注册的delegate就会触发  
public delegate void EventListenerDelegate(NotifyEvent evt);


public class NotifacitionCenter {
     
    //private static NotifacitionCenter instance;// 单例 
    //private NotifacitionCenter() { }
    //public static NotifacitionCenter getInstance() {
    //    if (instance == null) {
    //        instance = new NotifacitionCenter();
    //    }
    //    return instance;
    //}//静态函数

    public NotifacitionCenter() {
        notifications = new Dictionary<NotifyType, EventListenerDelegate>();
        

    }//构造函数



    // 成员变量  
    private Dictionary<NotifyType, EventListenerDelegate> notifications;  // 所有的消息  


     
    public void registerObserver(NotifyType type, EventListenerDelegate listener) {
        if (listener == null) {
            Debug.LogError("registerObserver: listener cannot be null");
            return;
        }

        // 将新来的监听者加入调用链，这样只要调用Combine返回的监听者就会调用所有的监听者  
        Debug.Log("NotifacitionCenter: add observer" + type);

        EventListenerDelegate myListener = null;
        notifications.TryGetValue(type, out myListener);
        notifications[type] = (EventListenerDelegate)Delegate.Combine(myListener, listener);
    }// 注册监视 

    // 移除监视  
    public void removeObserver(NotifyType type, EventListenerDelegate listener) {
        if (listener == null) {
            Debug.LogError("removeObserver: listener cannot be null");
            return;
        }

        // 与添加的思路相同，只是这里是移除操作  
        Debug.Log("NotifacitionCenter: remove observer" + type);
        notifications[type] = (EventListenerDelegate)Delegate.Remove(notifications[type], listener);
    }

    public void removeAllObservers() {
        notifications.Clear();
        Debug.Log("NotifacitionCenter remove All observer");
    }

    // 消息触发  
    public void postNotification(NotifyEvent evt) {
        EventListenerDelegate listenerDelegate;
        if (notifications.TryGetValue(evt.Type, out listenerDelegate)) {
            try {
                // 执行调用所有的监听者  
                listenerDelegate(evt);             
            }
            catch (System.Exception e) {
                throw new Exception(string.Concat(new string[] { "Error dispatching event", evt.Type.ToString(), ": ", e.Message, " ", e.StackTrace }), e);
            }
        }
    }

}// 消息中心  