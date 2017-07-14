using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSpace.GameCore;


namespace GameSpace {

    public class RelPanelContext : BaseContext {

        public string name;
        public int rel_type;
        public int rel_count;
        public int attract;
        public int piety;
        public int org;
        public int theo;

        public RelPanelContext(lc_Religion rel) : base(UIType.RelPanel) {
            name = rel.m_name;
            rel_type = rel.m_relType;
            this.rel_count = rel.m_count;
            attract = rel.m_attraction;
            piety = rel.m_piety;
            org = rel.m_organization;
            theo = rel.m_theorypt;
        }

    }

    public class RelPanelView : BaseView {

        public void ShowData(RelPanelContext context) {
            Transform transform = this.gameObject.transform;
            
            transform.Find("NAME_VALUE").GetComponent<Text>().text = context.name;
            transform.Find("TYPE_VALUE").GetComponent<Text>().text = context.rel_type.ToString();
            transform.Find("COUNT_VALUE").GetComponent<Text>().text = context.rel_count.ToString();
            transform.Find("ATTRACT_VALUE").GetComponent<Text>().text = context.attract.ToString();
            transform.Find("PIETY_VALUE").GetComponent<Text>().text = context.piety.ToString();
            transform.Find("ORG_VALUE").GetComponent<Text>().text = context.org.ToString();
            transform.Find("THEO_VALUE").GetComponent<Text>().text = context.theo.ToString();
        }
        public override void OnEnter(BaseContext context) {
            ShowData((RelPanelContext)context);
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