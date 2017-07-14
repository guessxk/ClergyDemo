using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSpace {
    public class ContextManager {
        private Stack<BaseContext> _contextStack = new Stack<BaseContext>(); //保存上下文的栈

        private ContextManager() {
            //NoStackPush(new ActiveMenuContext());
            //NoStackPush(new OptionPanelContext());
            //NoStackPush(new PopInterFacePanelContext());
        }

        public void NoStackPush(BaseContext nextContext) {
            BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
            nextView.OnEnter(nextContext);
        }

        public void NoStackPop(BaseContext curContext) {
            BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
            curView.OnExit(curContext); 
        }

        public void Push(BaseContext nextContext) {
            if (_contextStack.Count != 0) {  //若栈非空，则执行当前栈的ONPAUSE
                BaseContext curContext = _contextStack.Peek();
                if (curContext == null) Debug.Log("null");
                else Debug.Log(curContext.ViewType.Name);
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnPause(curContext);
            }

            _contextStack.Push(nextContext);
            BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
            nextView.OnEnter(nextContext);
        } //保存当前上下文入栈

        public void Pop() {
            if (_contextStack.Count != 0) {
                BaseContext curContext = _contextStack.Peek();
                _contextStack.Pop();

                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnExit(curContext);
            }

            if (_contextStack.Count != 0) {
                BaseContext lastContext = _contextStack.Peek();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(lastContext.ViewType).GetComponent<BaseView>();
                curView.OnResume(lastContext);
            }
        }//退出当前上下文出栈，弹出之前一个上下文

        public BaseContext PeekOrNull() {
            if (_contextStack.Count != 0) {
                return _contextStack.Peek();
            }
            return null;
        }

    
        public void PopAll() {
            foreach( BaseContext basecontext in _contextStack) {
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(basecontext.ViewType).GetComponent<BaseView>();
                curView.OnExit(basecontext);
            }
        }

       
    } //上下文管理类
}

