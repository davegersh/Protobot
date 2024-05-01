using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Protobot.UI {
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public class MovementCanvas : MonoBehaviour {
        [SerializeField] private ObjectLink refObj;
        public List<string> permittedTags; //the list of objects tags that are allowed to have this canvas enabled on them
        public bool toggled; //this means that the tool toggle associated with this canvas has been enabled
        public bool active; //when toggled AND canvas component is enabled

        [CacheComponent] private Canvas canvas; //displays the canvas and all children
        [CacheComponent] private GraphicRaycaster graphicRaycaster; //allows for input for this canvas and all children

        private bool onActiveCalled;
        private bool onInactiveCalled;
        public UnityEvent OnActive;
        public UnityEvent OnInactive;

        public void Update() {
            active = GetActivity();

            if (active) {
                transform.position = refObj.obj.transform.position;
                if (!onActiveCalled) 
                    OnActive.Invoke();
                onInactiveCalled = false;
            }
            else {
                if (!onInactiveCalled)
                    OnInactive.Invoke();
                onActiveCalled = false;
            }

            canvas.enabled = active;
            graphicRaycaster.enabled = active;
        }

        private bool GetActivity() {
            if (toggled && refObj.active) {
                if (permittedTags.Contains(refObj.obj.tag) || permittedTags.Count == 0) {
                    return true;
                }
            }
            return false;
        }

        public void ToggleCanvas() {
            toggled = !toggled;
        }

        public void SetToggle(bool value) {
            toggled = value;
        }
    }
}