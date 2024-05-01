using UnityEngine;
using System.Collections;

namespace Protobot {
    public class ObjectLinkVectorLink : VectorLink {
        [SerializeField] private ObjectLink objectLink;
        [SerializeField] private TransformVectorLink.Direction direction;

        public override Vector3 Vector { 
            get {
                if (objectLink.active) {
                    return TransformVectorLink.GetVector(direction, objectLink.tform);
                }
                else {
                    return Vector3.zero;
                }
            }
        }
    }
}