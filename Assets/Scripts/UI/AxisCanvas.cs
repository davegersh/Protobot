using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protobot.SelectionSystem;

namespace Protobot.UI {
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class AxisCanvas : MonoBehaviour {
        public Vector3 axis;
        public bool unsigned; //if true, will allow for enabling even if Slection's forward is exactly opposite to the axis
        private bool enable;

        //Component reference
        [CacheComponent] private Canvas canvas;
        [CacheComponent] private GraphicRaycaster graphicRaycaster;
        [SerializeField] private ObjectLink refObj;

        void Update() {
            if (OnAxis())
                enable = !(transform.parent != null && !transform.parent.GetComponent<Canvas>().enabled);
            else
                enable = false;

            canvas.enabled = enable;
            graphicRaycaster.enabled = enable;
        }

        bool OnAxis() {
            if (refObj.obj != null) {
                Vector3 refForward = refObj.tform.forward;
                return (refForward == axis || (unsigned && refForward == -axis));
            }
            return false;
        }
    }
}
