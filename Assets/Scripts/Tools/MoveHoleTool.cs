using UnityEngine;
using System.Collections.Generic;
using Protobot.Transformations;

namespace Protobot.Tools {
    public class MoveHoleTool : MonoBehaviour {
        [SerializeField] private MovementManager movementManager;

        [SerializeField] private ObjectLink hoverObjectLink; //the object being hovered over by mousecast
        [SerializeField] private GameObject HoverObj => hoverObjectLink?.obj; //the object being hovered over by mousecast

        [SerializeField] private HoleFace selectedHoleFace;
        [SerializeField] private HoleFace hoverHoleFace;

        [SerializeField] private GameObject pivotRotateRing;

        public void SetHolePosition() {
            if (!hoverObjectLink.active) return;

            if (HoverObj.CompareTag("Screw")) {
                Screw screw = HoverObj.GetComponent<Screw>();
               // if (!screw.holeDetector.holes.Contains(selectedHoleFace.hole)) {
                    var displacement = new Displacement(screw.GetLowestPoint(), HoverObj.transform.forward);
                    movementManager.DisplaceTo(displacement);
                //}
            }
            else if (HoverObj.tag.Contains("Hole")) {
                var displacement = new Displacement(hoverHoleFace.position, -hoverHoleFace.direction);
                movementManager.DisplaceTo(displacement);
            }
        }

        public void TogglePivotRotateRing() {
            pivotRotateRing.SetActive(!pivotRotateRing.activeInHierarchy);
        }
    }
}