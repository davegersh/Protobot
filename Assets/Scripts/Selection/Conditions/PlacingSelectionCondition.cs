using UnityEngine;
using Protobot.Tools;

namespace Protobot.SelectionSystem {
    public class PlacingSelectionCondition : SelectionCondition {
        [SerializeField] private Placement placement;
        public override bool GetValue(ISelection selection) => !placement.placing;
    }
}