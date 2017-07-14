using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace GameSpace {
    public enum ResType { Spirit, Sound, Scriptfile }


    public class AssetItem {
        public string BundlePath;
        public ResType Restype;

        public AssetItem(string pathname, ResType type) {
            BundlePath = pathname;
            Restype = type;
        }
    }

    public class ResourceManger {

        private Dictionary<String, AssetBundle> AssetDict = new Dictionary<String, AssetBundle>();

        private static ResourceManger _instance;

        public static ResourceManger Instance
        {
            get {
                if (_instance == null)
                    _instance = new ResourceManger();

                return _instance;
            }
        }

        public ResourceManger() {

        }

        public void LoadRes(string bundlepath) {
            if (AssetDict.ContainsKey(bundlepath) == true && AssetDict[bundlepath] != null) {
                Debug.LogError("Res[" + bundlepath + "] is exist");
                return;
            }
            AssetBundle bundle = AssetBundle.LoadFromFile(bundlepath);
            if (bundle == null) {
                Debug.LogError("load assetbundlefile [" + bundlepath + "]fail");
            }
            AssetDict[bundlepath] = bundle;
            Debug.Log("load assetbundlefile[" + bundlepath + "] success");
        }

        public T GetRes<T>(string bundlepath, string ResName) where T : UnityEngine.Object {

            if (AssetDict.ContainsKey(bundlepath) && AssetDict[bundlepath] != null) {
                Debug.Log("GetRes [" + bundlepath + "]");
                //Debug.Log(ResName);
                return AssetDict[bundlepath].LoadAsset<T>(ResName);
            }
            else {
                Debug.LogError("load res[" + bundlepath + "] fail");
                return null;
            }
        }

        public T[] GetAllRes<T>(string bundlepath) where T : UnityEngine.Object {
            if (AssetDict.ContainsKey(bundlepath) && AssetDict[bundlepath] != null) {
                Debug.Log("GetRes [" + bundlepath + "] success");
                return AssetDict[bundlepath].LoadAllAssets<T>();
            }
            else {
                Debug.LogError("load res[" + bundlepath + "] fail");
                return null;
            }
        }

        public void UnLoadRes(string bundlepath) {
            if (AssetDict.ContainsKey(bundlepath) == true && AssetDict[bundlepath] != null) {
                AssetDict[bundlepath].Unload(false);
                Debug.Log("unload res[" + bundlepath + "] success");
            }
            else {
                Debug.LogError(" res[" + bundlepath + "] not exist");
            }
        }
    }
}
