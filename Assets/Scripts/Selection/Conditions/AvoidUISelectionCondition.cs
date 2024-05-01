using UnityEngine;

namespace Protobot.SelectionSystem {
    public class AvoidUISelectionCondition : SelectionCondition {
        public override bool GetValue(ISelection selection) => !MouseInput.overUI;
    }
}