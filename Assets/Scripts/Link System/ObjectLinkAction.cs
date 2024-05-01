using UnityEngine;
using UnityEngine.Events;
using Protobot.InputEvents;

namespace Protobot {
    public abstract class ObjectLinkAction : MonoBehaviour {
        [SerializeField] public UnityEvent OnExecute;
        [SerializeField] public ObjectLink refObj;
        [SerializeField] private InputEvent input;
        [SerializeField] bool allowOverUI = false;
        public abstract void Execute();

        private void Awake() {
            if (input != null) {
                input.performed += () => {
                    if ((!allowOverUI && !MouseInput.overUI) || allowOverUI) {
                        Execute();
                    }
                };
            }
        }
    }
}