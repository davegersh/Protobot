using UnityEngine;

namespace Protobot {
    public class PartPlacementData : PlacementData {
        public override Transform placementTransform { get; set; }

        public PartType partType { get; }
        public PartGenerator partGenerator { get; }
        public override string objectId => partType.id;

        public PartPlacementData(PartGenerator partGenerator, PartType partType, Transform placementTransform) {
            this.partType = partType;
            this.partGenerator = partGenerator;
            this.placementTransform = placementTransform;
        }

        public override Mesh GetDisplayMesh() => partGenerator.GetMesh().CombineSubmeshes();
    }
}