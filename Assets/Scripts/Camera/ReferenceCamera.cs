using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    [RequireComponent(typeof(Camera))]
    public class ReferenceCamera : MonoBehaviour {
        private new Camera camera;
        public Camera referenceCamera;

        [Header("Reference: ")]
        public bool projectionMatrix = true;
        public bool viewportRect = true;

        void Awake() {
            camera = GetComponent<Camera>();
        }

        void Update() {
            if (projectionMatrix) camera.projectionMatrix = referenceCamera.projectionMatrix; 
            if (viewportRect) camera.rect = referenceCamera.rect;
        }
    }
}