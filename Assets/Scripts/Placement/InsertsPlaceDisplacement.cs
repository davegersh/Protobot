using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public class InsertsPlaceDisplacement : PlaceDisplacement {
        [SerializeField] private HoleFace hoverHoleFace;
        public override bool ModifyRotation => true;

        public override bool TryGetDisplacement(PlacementData placementData, out Displacement displacement) {
            if (placementData.objectId == "PLSI" || placementData.objectId == "MESI") {
                if (hoverHoleFace.gameObject.activeInHierarchy && hoverHoleFace.hole.IsHighStrength()) {
                    displacement = new Displacement(hoverHoleFace.position - hoverHoleFace.direction * 0.05f, -hoverHoleFace.direction);
                    return true;
                }
                else {
                    displacement = null;
                    return false;
                }
            }
            displacement = null;
            return false;
        }
    }
}