using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public abstract class PlaceDisplacement : MonoBehaviour {
        public abstract bool ModifyRotation { get; }
        public abstract bool TryGetDisplacement(PlacementData placementData, out Displacement displacement);
    }
}