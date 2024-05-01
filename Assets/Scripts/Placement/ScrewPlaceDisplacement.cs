using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public class ScrewPlaceDisplacement : PlaceDisplacement {
        [SerializeField] private HoleFace currentHoleFace;
        public override bool ModifyRotation => true;

        public override bool TryGetDisplacement(PlacementData placementData, out Displacement displacement) {
            if (currentHoleFace.direction != Vector3.zero) {
                if (placementData.objectId.Contains("SCRW") && currentHoleFace.gameObject.activeInHierarchy) {
                    displacement = new Displacement(currentHoleFace.position, currentHoleFace.LookRotation);
                    return true;
                }
            }

            displacement = null;
            return false;
        }
    }
}