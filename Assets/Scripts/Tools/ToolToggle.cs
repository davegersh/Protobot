using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Protobot.InputEvents;

namespace Protobot.Tools {
    public class ToolToggle : MonoBehaviour {
        public bool toggled; //refers to when a tool is toggled
        [SerializeField] private InputEvent toggleOnInput;

        public bool active {
            get {
                if (toggled) {
                    foreach (IToolRequirement req in requirements) {
                        if (!req.isValid) {
                            return false;
                        }
                    }
                    return true;
                }
                return false;
            }
        }

        private bool prevActivity;

        private IToolRequirement[] requirements;

        [Space(10)]

        public UnityEvent OnToggle;
        public UnityEvent OnActivate;
        public UnityEvent OnDeactivate;

        void Awake() {
            requirements = GetComponents<IToolRequirement>();
            OnDeactivate?.Invoke();

            if (toggleOnInput != null) {
                toggleOnInput.performed += () => Toggle(true);
            }
        }

        void Update() {
            bool acivity = active;

            if (toggled && prevActivity != acivity) {
                if (acivity)
                    OnActivate?.Invoke();
                else
                    OnDeactivate?.Invoke();
            }

            prevActivity = acivity;
        }

        public void Toggle(bool value) {
            if (value) {
                toggled = true;
                OnToggle?.Invoke();
            }
            else {
                toggled = false;
                OnDeactivate?.Invoke();
            }
        }
    }
}
