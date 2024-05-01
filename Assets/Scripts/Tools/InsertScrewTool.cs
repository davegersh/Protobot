using UnityEngine;
using Protobot.Transformations;

namespace Protobot.Tools {
    public class InsertScrewTool : MonoBehaviour {
        [SerializeField] private HoleFace targetHoleFace = null;
        [SerializeField] private MovementManager movementManager;
        public void SetScrewPosition() {
            if (targetHoleFace.gameObject.activeInHierarchy) {
                Vector3 newPos = targetHoleFace.position;

                Displacement finalOrientation = new Displacement(newPos, targetHoleFace.direction);
                movementManager.DisplaceTo(finalOrientation);
            }
        }
    }
}