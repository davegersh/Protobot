using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI {
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class DirectionalCanvas : MonoBehaviour {
        [CacheComponent] private Canvas canvas;
        [CacheComponent] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private ObjectLink refObj;

        void Update() {
            if (refObj.active && canvas.enabled && graphicRaycaster)
                transform.forward = refObj.tform.forward;
        }
    }
}