using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class DistanceScaler : MonoBehaviour {
        public Transform target;
        public float scaleFactor = 0.15f;
        void Update() => transform.localScale = Vector3.one * Vector3.Distance(transform.position, target.position) * scaleFactor;
    }
}