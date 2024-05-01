using System;
using UnityEngine;

namespace Protobot.SelectionSystem {
    public class HoverSelector : Selector {
        public override event Action<ISelection> setEvent;
        public override event Action clearEvent;
        [SerializeField] private MouseCast mouseCast = null;
        [SerializeField] private bool checkPrevObj;

        private GameObject prevObj;

        public void Update() {
            GameObject mouseCastObj = mouseCast.gameObject;

            if (mouseCastObj != null) {
                if (prevObj != mouseCastObj || !checkPrevObj) {
                    var selection = new ObjectSelection {
                        gameObject = mouseCastObj,
                        selector = this
                    };

                    setEvent?.Invoke(selection);
                }
            }
            else
                clearEvent?.Invoke();

            prevObj = mouseCastObj;
        }
    }
}