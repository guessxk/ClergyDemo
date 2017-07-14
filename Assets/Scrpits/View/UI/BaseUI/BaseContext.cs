using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSpace{
    public class BaseContext {

        public UIType ViewType { get; private set; } //UI 类型，代表一种UI

        public BaseContext(UIType viewType) {
            ViewType = viewType;
        }


    }  //UI 信息，保存UI里输入输出数据
}
