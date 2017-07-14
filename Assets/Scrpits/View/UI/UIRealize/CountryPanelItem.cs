using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSpace.GameCore;

namespace GameSpace{
public class CountryPanelItem : MonoBehaviour {


    public void ValueInit(lc_Country country) {
        Transform transform = this.gameObject.transform;

        transform.Find("NAME_VALUE").GetComponent<Text>().text = country.m_name;
        transform.Find("POP_VALUE").GetComponent<Text>().text = country.m_pop.ToString();
        transform.Find("MILITARY_VALUE").GetComponent<Text>().text = country.m_military.ToString();
        transform.Find("CROWN_VALUE").GetComponent<Text>().text = country.m_crown.ToString();
        transform.Find("EMS_VALUE").GetComponent<Text>().text = country.m_ems.ToString();
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
}
