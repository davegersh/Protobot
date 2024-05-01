using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public class ShaftPlaceDisplacement : PlaceDisplacement {
        [SerializeField] private HoleFace currentHoleFace;
        [SerializeField] private MouseCast mouseCast;
        public override bool ModifyRotation => true;
        public override bool TryGetDisplacement(PlacementData placementData, out Displacement displacement) {
            displacement = null;
            
            if (!placementData.objectId.Contains("SHFT")) return false;
            
            bool currentHoleActive = currentHoleFace.direction != Vector3.zero &&
                                     currentHoleFace.gameObject.activeInHierarchy;

            Motor refMotor = null;

            if (currentHoleActive) {
                refMotor = currentHoleFace.hole.part.GetComponent<Motor>();
                displacement = new Displacement(currentHoleFace.position, currentHoleFace.LookRotation);
            }

            if (mouseCast.overObj && mouseCast.gameObject.TryGetComponent(out Motor motor))
                refMotor = motor;

            if (refMotor != null) {
                var (length, hs) = GetShaftInfo(placementData);
                displacement = refMotor.GetShaftDisplacement(length, hs);
            }

            return displacement != null;
        }

        public (float length, bool highStrength) GetShaftInfo(PlacementData placementData) {
            float length = 0;
            bool highStrength = false;
            
            if (placementData.TryParse(out GameObjectPlacementData objectPlacement)) {
                var obj = objectPlacement.GetGameObject();
                length = obj.transform.localScale.z;

                highStrength = obj.GetComponent<SavedObject>().id.Contains("High");
            }
            
            if (placementData.TryParse(out PartPlacementData partPlacement)) {
                var gen = partPlacement.partGenerator;
                length = float.Parse(gen.param2.value);
                highStrength = gen.param1.value.Contains("High");
            }

            return (length, highStrength);
        }
    }
}