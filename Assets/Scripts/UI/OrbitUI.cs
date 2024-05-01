using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot.UI {
    public class OrbitUI : MonoBehaviour {
        [SerializeField] Transform mainCamTransform;

        void Start() {

        }

        void Update() {
            transform.position = mainCamTransform.position;
        }
    }
}