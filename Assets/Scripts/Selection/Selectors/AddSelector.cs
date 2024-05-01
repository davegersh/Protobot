using System;
using UnityEngine;
using Protobot.UI;

namespace Protobot.SelectionSystem {
    public class AddSelector : Selector {
        public override event Action<ISelection> setEvent;
        public override event Action clearEvent;
        
        [SerializeField] private AddPartsUI addPartsUI = null;
        private GameObject prevAddedObj = null;
        
        private void Update() {
            GameObject addedObj = addPartsUI.lastAddedObj;

            if (addedObj != prevAddedObj) {
                var selection = new ObjectSelection {
                    gameObject = addedObj,
                    selector = this
                };

                setEvent?.Invoke(selection);
            }

            prevAddedObj = addedObj;
        }
    }
}