using System.Collections;
using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public class HolePlaceDisplacement : PlaceDisplacement {
        [SerializeField] private MouseCast mouseCast;
        private GameObject HoverObj => mouseCast.gameObject;

        [SerializeField] private HoleFace hoverHoleFace;
        [SerializeField] private HoleFace selectedHoleFace;

        public override bool ModifyRotation => true;

        public override bool TryGetDisplacement(PlacementData placementData, out Displacement displacement) {
            if (HoverObj != null && placementData.objectId.Contains("Hole")) {
                if (HoverObj.CompareTag("Screw")) {
                    Screw screw = HoverObj.GetComponent<Screw>();
                    //if (!screw.holeDetector.holes.Contains(selectedHoleFace.hole)) {
                        displacement = new Displacement(screw.GetLowestPoint(), HoverObj.GetAlignmentRotation(true));
                        return true;
                    //}
                }
                
                if (HoverObj.tag.Contains("Hole")) {
                    displacement = new Displacement(hoverHoleFace.position, hoverHoleFace.LookRotation);
                    return true;
                }
            }
            
            displacement = null;
            return false;
        }
    }
}