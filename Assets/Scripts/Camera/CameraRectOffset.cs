using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    [RequireComponent(typeof(Camera))]
    public class CameraRectOffset : MonoBehaviour {
        public RectTransform rectTransform;
        public Vector2 offset;
        private new Camera camera;

        void Awake() {
            camera = GetComponent<Camera>();
        }

        void Update() {
            Rect newRect = camera.rect;

            if (offset.x != 0) {
                newRect.x = offset.x / rectTransform.sizeDelta.x;
            }

            if (offset.y != 0) {
                newRect.y = offset.y / rectTransform.sizeDelta.y;
            }

            camera.rect = newRect;
        }
    }
}