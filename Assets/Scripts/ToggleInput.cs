using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Protobot.InputEvents;

namespace Protobot.UI {
    public class ToggleInput : MonoBehaviour {
        [CacheComponent] private Toggle toggle;

        [Tooltip("Toggles the toggle when input is pressed!")]
        [SerializeField] private InputEvent input;

        private void Awake() {
            if (input != null) {
                input.performed += () => {
                    if (!MouseInput.overUI)
                        toggle.isOn = !toggle.isOn;
                };
            }
        }
    }
}
