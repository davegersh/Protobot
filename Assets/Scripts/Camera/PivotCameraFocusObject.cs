using UnityEngine;
using Protobot.InputEvents;

namespace Protobot {
    public class PivotCameraFocusObject : MonoBehaviour {
        [SerializeField] private PivotCamera cam; 
        [SerializeField] private ObjectLink refObj;
        [SerializeField] private InputEvent input;

        private void Awake() {
            input.performed += () => Execute();
        }

        public void Execute() {
            if (refObj.obj != null)
                cam.MoveFocusPosition(refObj.tform.position);
        }
    }
}