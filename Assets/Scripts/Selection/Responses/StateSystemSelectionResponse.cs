using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.StateSystems;

namespace Protobot.SelectionSystem {
    public class StateSystemSelectionResponse : SelectionResponse {
        public override bool RespondOnlyToSelectors => true;

        [SerializeField] SelectionManager selectionManager;

        private void Start() {
            StateSystem.AddElement(new SelectionElement(selectionManager, null));
        }

        public override void OnSet(ISelection selection) {
            selection.selector = null;
            StateSystem.AddState(new SelectionElement(selectionManager, selection));
        }

        public override void OnClear(ClearInfo clearInfo) {
            StateSystem.AddState(new SelectionElement(selectionManager, null));
        }
    }
}
