using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class PartType : MonoBehaviour {
        [Header("Identification")]
        public string id;

        public bool connectingPart;

        public enum PartGroup { Structure, Motion, Electronics, None };

        [Header("UI")]
        public PartGroup group;
        public Sprite icon;
    }
}