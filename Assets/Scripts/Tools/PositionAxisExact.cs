using UnityEngine;
using DG.Tweening;
using Protobot.UI;
using UnityEngine.UI;

namespace Protobot.Tools {
    public class PositionAxisExact : MonoBehaviour {
        private PositionAxis positionAxis;
        [SerializeField] private ContextMenuUI inputContextMenu;
        [SerializeField] private InputField inputField;

        public void SetPositionFromInput(string text) {
            if (float.TryParse(text, out float result))
                SetPosition(result);
            else
                SetPosition(0);
        }

        public void SetPosition(float distance) {
            Vector3 axisOffset = Vector3.Project(Vector3.one * distance, positionAxis.normal.Vector);
            Vector3 newPos = positionAxis.initObjPos + axisOffset;

            Transform refTransform = positionAxis.refObj.transform;
            refTransform.DOMove(newPos, 0.25f);
        }

        public void EnableInputMenu(PositionAxis newPosAxis) {
            positionAxis = newPosAxis;
            inputContextMenu.EnableMenu();
                
            inputField.Select();
            inputField.ActivateInputField();
        }
    }
}
