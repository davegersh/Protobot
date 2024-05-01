using UnityEngine;

namespace Protobot.SelectionSystem {
    public abstract class SelectionResponse : MonoBehaviour {
        public abstract bool RespondOnlyToSelectors { get; }
        public abstract void OnSet(ISelection selection);
        public abstract void OnClear(ClearInfo selection);
    }
}