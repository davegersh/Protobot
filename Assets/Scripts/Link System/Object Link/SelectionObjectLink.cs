using UnityEngine;
using Protobot.SelectionSystem;

namespace Protobot {
    public class SelectionObjectLink : ObjectLink {
        public override GameObject obj => selectionManager.current?.gameObject;
        [SerializeField] private SelectionManager selectionManager;
    }
}