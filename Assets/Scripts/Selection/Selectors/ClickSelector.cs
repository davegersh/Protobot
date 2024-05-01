using System;
using UnityEngine;
using Protobot.InputEvents;

namespace Protobot.SelectionSystem {
    public class ClickSelector : Selector {
        public override event Action<ISelection> setEvent;
        public override event Action clearEvent;
        
        [SerializeField] private MouseCast mouseCast = null;
        [SerializeField] private InputEvent input;

        public void Awake() {
            input.performed += () => OnPerformInput();
        }

        private void OnPerformInput() {
            if (!MouseInput.overUI) {
                if (mouseCast.overObj) {
                    var selection = new ObjectSelection {
                        gameObject = mouseCast.gameObject,
                        selector = this
                    };

                    setEvent?.Invoke(selection);
                }
                else {
                    clearEvent?.Invoke();
                }
            }
        }
    }
}