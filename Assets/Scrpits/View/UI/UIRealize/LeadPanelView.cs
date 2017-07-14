using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSpace.GameCore;
using UnityEngine.UI;

namespace GameSpace {
    public class LeadPanelContext : BaseContext {

        public string name;
        public int age;
        public int eloquence;
        public int intelligence;
        public int leadership;
        public int willpower;
        public int wealth;
        public int fame;

        

        public LeadPanelContext(lc_Seer seer) : base(UIType.LeadPanel) {
            name = seer.m_name;
            age = seer.m_age;
            eloquence = seer.m_eloquence;
            intelligence = seer.m_intelligence;
            leadership = seer.m_leadership;
            willpower = seer.m_willpower;
            wealth = seer.m_wealth;
            fame = seer.m_fame;
        }


    }

    public class LeadPanelView : BaseView {

        public void ShowData(LeadPanelContext context) {
            Transform transform = this.gameObject.transform;

            transform.Find("NAME_VALUE").GetComponent<Text>().text = context.name;
            transform.Find("AGE_VALUE").GetComponent<Text>().text = context.age.ToString();
            transform.Find("ATTR1_VALUE").GetComponent<Text>().text = context.eloquence.ToString();
            transform.Find("ATTR2_VALUE").GetComponent<Text>().text = context.intelligence.ToString();
            transform.Find("ATTR3_VALUE").GetComponent<Text>().text = context.leadership.ToString();
            transform.Find("ATTR4_VALUE").GetComponent<Text>().text = context.willpower.ToString();
            transform.Find("ATTR5_VALUE").GetComponent<Text>().text = context.wealth.ToString();

        }
        public override void OnEnter(BaseContext context) {
            ShowData((LeadPanelContext)context);
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context) {
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context) {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context) {
            base.OnResume(context);
        }


        public void OnCloseCallBack() {
            Singleton<ContextManager>.Instance.Pop();
        }
    }
}
