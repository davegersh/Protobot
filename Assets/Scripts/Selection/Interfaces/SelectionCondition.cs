using UnityEngine;

namespace Protobot.SelectionSystem {
    public abstract class SelectionCondition : MonoBehaviour {
        public bool allowClearing;
        public abstract bool GetValue(ISelection selection);
    }
}