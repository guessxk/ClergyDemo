using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSpace {
    public class UIManager {
        public Dictionary<UIType, GameObject> _UIDict = new Dictionary<UIType, GameObject>(); //UI列表，保存当前所有UI的OBJECT

        private Transform _canvas; //画布

        private UIManager() {
            _canvas = GameObject.Find("Canvas").transform;
            foreach (Transform item in _canvas) {
                GameObject.Destroy(item.gameObject);
            }
        }

        public GameObject GetSingleUI(UIType uiType) {
            if (_UIDict.ContainsKey(uiType) == false || _UIDict[uiType] == null) {
                Debug.Log(uiType.Name);
                GameObject go = GameObject.Instantiate(ResourceManger.Instance.GetRes<GameObject>(Application.streamingAssetsPath + @"/AssetBundles/mapsceneui",uiType.Name)) as GameObject;
                go.transform.SetParent(_canvas, false);
                go.name = uiType.Name;
                //_UIDict.AddOrReplace(uiType, go);
                _UIDict[uiType] = go;
                go.GetComponent<BaseView>().OnInit();
                return go;
            }
            return _UIDict[uiType];
        }//根据UI类型获取对应UI的gameobject

        public void DestroySingleUI(UIType uiType) {
            if (!_UIDict.ContainsKey(uiType)) {
                return;
            }

            if (_UIDict[uiType] == null) {
                _UIDict.Remove(uiType);
                return;
            }

            GameObject.Destroy(_UIDict[uiType]);
            _UIDict.Remove(uiType);
        }//根据UI类型销毁对应UIgameobject

    }
}