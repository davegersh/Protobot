using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Protobot.InputEvents;
using Protobot.Transformations;

namespace Protobot {
    public class ShaftAxisPlaceDisplacement : PlaceDisplacement {
        [SerializeField] private MouseCast mouseCast;
        [SerializeField] private InputEvent flipInput;
        private GameObject HoverObj => mouseCast.gameObject;
        public override bool ModifyRotation => true;
        

        public bool IsMotionObject(PlacementData placementData) {
            if (placementData.TryParse(out GameObjectPlacementData objPlaceData)) { 
                return objPlaceData.GetGameObject().CompareTag("Motion");
            }
            else if (placementData.TryParse(out PartPlacementData partPlaceData)) {
                return partPlaceData.partType.group == PartType.PartGroup.Motion;
            }

            return false;
        }

        public float GetOffset(PlacementData placementData) {
            if (placementData.TryGetPartData(out var partData))
                return partData.PrimaryHoleDepth / 2;
            
            return 0;
        }

        public float GetInsertsOffset(PlacementData placementData, GameObject shaftObj) {
            if (!placementData.TryGetPartData(out var partData)) return 0;
            
            if (partData.allowCenterInserts && !shaftObj.name.Contains("HS"))
                return 0.05f;

            return 0;
        }

        public override bool TryGetDisplacement(PlacementData placementData, out Displacement displacement) {
            if (HoverObj != null && (placementData.objectId.Contains("Hole") || GetOffset(placementData) > 0)) {
                if (HoverObj.tag.Contains("Shaft")) {
                    ConnectingPart connectingPart = HoverObj.GetComponent<ConnectingPart>();
                    Transform shaftTransform = HoverObj.transform;

                    Vector3 mouseDirPos = Vector3.Project(mouseCast.hit.point, shaftTransform.forward); //the position on the shaft of mouse only in forward direction
                    Vector3 shaftDirPos = Vector3.Project(shaftTransform.position, shaftTransform.forward); //the position of the shaft only in its forward direction
                    Vector3 shaftMousePos = shaftTransform.position - shaftDirPos + mouseDirPos;

                    var rayForward = new Ray(shaftMousePos, shaftTransform.forward);
                    var rayBackward = new Ray(shaftMousePos, -shaftTransform.forward);

                    var forwardCast = Physics.Raycast(rayForward, out RaycastHit forwardHit, 12, HoleCollider.HOLE_COLLISIONS_MASK);
                    var backwardCast = Physics.Raycast(rayBackward, out RaycastHit backwardHit, 12, HoleCollider.HOLE_COLLISIONS_MASK);

                    float forwardDistance = 12;
                    float backwardDistance = 12;

                    if (forwardCast) forwardDistance = Vector3.Distance(forwardHit.point, shaftMousePos);
                    if (backwardCast) backwardDistance = Vector3.Distance(backwardHit.point, shaftMousePos);

                    Vector3 offset = shaftTransform.forward * (GetOffset(placementData) + GetInsertsOffset(placementData, HoverObj));

                    var rotation = shaftTransform.GetAlignmentRotation(flipInput.IsPressed);

                    if (forwardDistance < backwardDistance && forwardDistance < 0.325f) {
                        if (placementData.objectId.Contains("Hole")) {
                            rotation = Quaternion.LookRotation(forwardHit.normal, shaftTransform.up);
                        }
                        displacement = new Displacement(forwardHit.point - offset, rotation);
                        return true;
                    }

                    if (backwardDistance < forwardDistance && backwardDistance < 0.325f) {
                        if (placementData.objectId.Contains("Hole")) {
                            rotation = Quaternion.LookRotation(backwardHit.normal, shaftTransform.up);
                        }
                        displacement = new Displacement(backwardHit.point + offset, rotation);
                        return true;
                    }

                    displacement = new Displacement(shaftMousePos, rotation);
                    return true;
                }
            }

            displacement = null;
            return false;
        }
    }
}