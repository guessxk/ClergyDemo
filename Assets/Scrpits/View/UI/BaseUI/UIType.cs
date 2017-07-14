using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSpace {
    public class UIType {


        public string Name { get; private set; } //UI预制体名称

        public UIType(string name) {
            Name = name;
        }

        public override string ToString() {
            return string.Format(" name : {1}", Name);
        }

        //UI类型
        //public static readonly UIType MainMenu = new UIType("View/MainMenuView");
        //public static readonly UIType OptionMenu = new UIType("View/OptionMenuView");
        //public static readonly UIType NextMenu = new UIType("View/NextMenuView");
        //public static readonly UIType HighScore = new UIType("View/HighScoreView");

        public static readonly UIType ActiveMenu = new UIType("ActiveMenuView");
        public static readonly UIType LeadPanel = new UIType("LeadPanelView");
        public static readonly UIType RelPanel = new UIType("RelPanelView");
        public static readonly UIType CountryPanel = new UIType("CountryPanelView");
        public static readonly UIType OptionPanel = new UIType("OptionPanelView");
        public static readonly UIType PopInterFacePanel = new UIType("PopInterFacePanelView");
        public static readonly UIType CityInfoSign = new UIType("CityInfoSignView");
        public static readonly UIType CityIn = new UIType("CityInView");


    }
}
