using System;
using System.Collections.Generic;
using UnityEngine;
using Protobot.InputEvents;

namespace Protobot.SelectionSystem {
    public class AllSelector : Selector {
        public override event Action<ISelection> setEvent;
        public override event Action clearEvent;
        [SerializeField] private InputEvent input;

        private void Awake() {
            input.performed += () => {
                if (!MouseInput.overUI) {
                    Select();
                }
            };
        }

        public void Select() {
            List<GameObject> objs = new List<GameObject>();
            objs.AddRange(PartsManager.FindLoadedObjects());

            var selection = new MultiSelection(this, objs);
            setEvent?.Invoke(selection);
        }
    }
}