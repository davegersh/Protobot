using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Protobot {
    public class Screw : ConnectingPart {
        public float ScrewLength => GetComponent<MeshFilter>().mesh.bounds.size.z - 0.087f;

        public override bool CanFormConnection => holeDetector.TargetHoleFound;

        public override HoleDetector CreateHoleDetector() {
            var detector = HoleDetector.Create(transform, ScrewLength, HoleCollider.HoleType.Threaded);
            detector.transform.position -= (transform.forward * ScrewLength/2);
            return detector;
        }
        public override List<GameObject> GetAttachedParts() {
            if (CanFormConnection) {
                var orderedHoles = holeDetector.GetOrderedHoles();

                bool connect = false;

                List<GameObject> attachedParts = new List<GameObject>();

                //iterate backwards through the ordered holes to find the lowest threaded hole
                for (int i = orderedHoles.Count - 1; i >= 0; i--) {
                    if (holeDetector.IsTargetHole(orderedHoles[i]))
                        connect = true;

                    if (connect)
                        attachedParts.Add(orderedHoles[i].holeData.part);
                }

                return attachedParts;

            }
            
            return null;
        }

        /// <summary>
        /// Returns the list of attached parts given a custom a position and direction
        /// </summary>
        public List<GameObject> GetAttachedParts(Vector3 position, Vector3 direction) {
            List<GameObject> attachedParts = new List<GameObject>();

            var hits = GetRaycastHits(position, direction);

            for (int i = 0; i < hits.Length; i++) {
                var hit = hits[i];

                if (hit.collider.CompareTag("HoleCollider"))
                    attachedParts.Add(hit.transform.parent.gameObject);
            }

            return attachedParts;
        }


        private RaycastHit[] GetRaycastHits(Vector3 position, Vector3 direction) {
            Ray lowRay = new Ray(position + (direction * 0.1f), -direction);
            return Physics.RaycastAll(lowRay, ScrewLength);
        }

        private List<GameObject> GetAttachedPartsOrdered() {
            return GetRaycastHits(transform.position, transform.forward)
                .OrderBy(hit => Vector3.Distance(hit.point, transform.position))
                .Where(hit => hit.collider.CompareTag("HoleCollider"))
                .Select(hit => hit.transform.parent.gameObject)
                .ToList();
        }

        public Vector3 GetLowestPoint() {
            int attPartsCount = GetAttachedParts(transform.position, transform.forward).Count;

            if (attPartsCount > 0)
                return FindLowestPoint(attPartsCount - 1);
            else
                return transform.position;
        }
        
        public Vector3 FindLowestPoint(int attachedPartsIndex) {
            GameObject attPart = GetAttachedPartsOrdered()[attachedPartsIndex];
            Ray lowRay = new Ray(transform.position - (transform.forward * ScrewLength), transform.forward);
            Debug.DrawLine(lowRay.origin, transform.position);
            RaycastHit[] hits = Physics.RaycastAll(lowRay, ScrewLength);

            foreach (RaycastHit hit in hits) {
                if (hit.collider.CompareTag("HoleCollider") && hit.transform.parent.gameObject == attPart)
                    return hit.point;
            }
            return transform.position;
        }
    }
}