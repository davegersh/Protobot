using System.Collections;
using System.Collections.Generic;
using Protobot.Transformations;
using UnityEngine;

namespace Protobot {
    public class Motor : MonoBehaviour {
        [SerializeField] private HoleCollider shaftHole;
        private float HsOffset => 0.2775f;
        private float NormOffset => 0.47f;
        
        public Displacement GetShaftDisplacement(float shaftLength, bool highStrength) {
            if (shaftHole.IsOccupied) return null;
            Vector3 normal = shaftHole.holeData.forward;

            float offset = highStrength ? HsOffset : NormOffset;
            Vector3 pos = shaftHole.transform.position + (normal * (shaftLength / 2 - offset));

            return new Displacement(pos, normal);
        }
    }
}