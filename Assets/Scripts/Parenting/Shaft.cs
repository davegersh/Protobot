using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Protobot {
    public class Shaft : ConnectingPart {
        public float ShaftLength => transform.localScale.z;
        public override bool CanFormConnection => holeDetector.TargetHoleCount >= 2;

        public override HoleDetector CreateHoleDetector() {
            return HoleDetector.Create(transform, ShaftLength, HoleCollider.HoleType.Clamp);
        }

        private List<HoleCollider> GetOrderedHoles() {
            var furthestPos = transform.position + transform.forward * ShaftLength;

            return holeDetector
                .holes
                .OrderBy(x => Vector3.Distance(furthestPos, x.transform.position))
                .ToList();
        }

        public override List<GameObject> GetAttachedParts() {
            if (CanFormConnection) {
                var orderedHoles = GetOrderedHoles();

                var attachedObjs = new List<GameObject>();

                bool betweenTwoTargets = false;
                int foundTargets = 0;

                foreach (var hole in orderedHoles) {
                    bool isTarget = holeDetector.IsTargetHole(hole);
                    
                    if (isTarget) {
                        foundTargets++;
                        betweenTwoTargets = foundTargets < holeDetector.TargetHoleCount;
                    }
                    
                    if (betweenTwoTargets || isTarget)
                        attachedObjs.Add(hole.transform.parent.gameObject);
                }

                return attachedObjs;
            }

            return null;
        }
    }
}