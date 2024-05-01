using UnityEngine;
using Protobot.Transformations;

namespace Protobot.Tools {
    public class MoveShaftTool : MonoBehaviour {

        [SerializeField] private MovementManager movementManager;
        [SerializeField] private GameObject CurShaft => movementManager.MovingObj;

        [SerializeField] private ObjectLink targetObjLink;
        [SerializeField] private HoleFace targetHoleFace;

        [SerializeField] private Pivot moveShaftPivot;

        public void SetShaftPosition() {
            SavedObject savedObj = targetObjLink?.obj?.GetComponent<SavedObject>();

            if (savedObj != null && savedObj.nameId == "MOTR") {
                Transform motrTransform = targetObjLink.tform;

                float shaftLength = CurShaft.GetComponent<Shaft>().ShaftLength;
                Vector3 newPos = motrTransform.position + motrTransform.right * -0.5f + (motrTransform.forward * (shaftLength + 0.85f));

                var displacement = new Displacement(newPos, motrTransform.forward);
                movementManager.DisplaceTo(displacement);
            }
            else if (targetHoleFace.gameObject.activeInHierarchy) {
                var displacement = new Displacement(targetHoleFace.position, targetHoleFace.direction);
                movementManager.DisplaceTo(displacement);
            }
        }
    }
}
