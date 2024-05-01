using System;
using UnityEngine;

namespace Protobot.SelectionSystem {
    public class PublicSelector : Selector {
        public override event Action<ISelection> setEvent;
        public override event Action clearEvent;

        public void Clear() {
            clearEvent?.Invoke();
        }

        public void SelectObj(GameObject obj) {
            setEvent?.Invoke(new ObjectSelection {
                gameObject = obj,
                selector = this
            });
        }
    }
}