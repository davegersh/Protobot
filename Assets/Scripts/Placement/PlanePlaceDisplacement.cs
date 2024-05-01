using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public class PlanePlaceDisplacement : PlaceDisplacement {
        [SerializeField] PlaneCast planeCast;
        public override bool ModifyRotation => false;

        public override bool TryGetDisplacement(PlacementData placementData, out Displacement displacement) {
            displacement = new Displacement(planeCast.point, Quaternion.identity);
            return true;
        }
    }
}