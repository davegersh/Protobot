using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class MatchObjectLinkTransform : MonoBehaviour {
        [SerializeField] private ObjectLink objectLink;

        public bool matchPosition;
        public bool matchRotation;
        public bool matchScale;

        void Update() {
            if (objectLink.active) {
                if (matchPosition) transform.position = objectLink.tform.position;
                if (matchRotation) transform.rotation = objectLink.tform.rotation;
                if (matchScale) transform.localScale = objectLink.tform.localScale;
            }
        }
    }
}