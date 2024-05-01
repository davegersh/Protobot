using UnityEngine;

namespace Protobot {
    public abstract class PlacementData {
        public abstract Transform placementTransform { get; set; }
        public abstract Mesh GetDisplayMesh();
        public abstract string objectId { get; }
    }

    public static class PlacementDataExtensions {
        public static bool TryParse(this PlacementData placementData, out GameObjectPlacementData gameObjectPlacementData) {
            if (placementData.GetType() == typeof(GameObjectPlacementData)) {
                gameObjectPlacementData = (GameObjectPlacementData)placementData;
                return true;
            }

            gameObjectPlacementData = null;
            return false;
        }

        public static bool TryParse(this PlacementData placementData, out PartPlacementData partPlacementData) {
            if (placementData.GetType() == typeof(PartPlacementData)) {
                partPlacementData = (PartPlacementData)placementData;
                return true;
            }

            partPlacementData = null;
            return false;
        }

        public static bool TryGetPartData(this PlacementData placementData, out PartData partData) {
            PartType partType = null;
            
            if (placementData.TryParse(out GameObjectPlacementData objPlaceData)) {
                if (objPlaceData.GetGameObject().TryGetComponent(out SavedObject savedObject)) {
                    partType = PartsManager.GetPartType(savedObject.id);
                }
            }
            else if (placementData.TryParse(out PartPlacementData partPlaceData)) {
                partType = partPlaceData.partType;
            }

            if (partType != null) {
                var gen = partType.GetComponent<PartGenerator>();
                if (gen.TryGetPartData(out PartData data)) {
                    partData = data;
                    return true;
                }
            }
            
            partData = null;
            return false;
        }
    }
}