using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public class NutPlaceDisplacement : PlaceDisplacement {
        [SerializeField] private ObjectLink mouseObj;
        public override bool ModifyRotation => true;
        private bool HoveringOverScrew => mouseObj.active && mouseObj.obj.CompareTag("Screw");
        public override bool TryGetDisplacement(PlacementData placementData, out Displacement displacement) {
            bool PlacingNut = placementData.objectId.Contains("NUT");

            if (PlacingNut && HoveringOverScrew) {
                Screw screw = mouseObj.obj.GetComponent<Screw>();

                displacement = new Displacement(screw.GetLowestPoint(), mouseObj.tform.forward);
                return true;
            }

            displacement = null;
            return false;
        }
    }
}