using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class PartData : MonoBehaviour {
        [Header("Identification")]
        public string param1Value;
        public string param2Value;

        [Header("Holes")]
        [Space(5)]
        [TextArea(0, 100)]
        public string holeData;

        public float PrimaryHoleDepth => primaryHole != null ? primaryHole.holeData.depth : 0;
        public HoleCollider primaryHole;
        public bool allowCenterInserts;
    }
}
