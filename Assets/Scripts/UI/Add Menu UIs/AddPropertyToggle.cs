using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


namespace Protobot.UI {
    public class AddPropertyToggle : MonoBehaviour {
        [SerializeField] private Text displayText;
        
        [CacheComponent] private Toggle cachedToggle;
        public Toggle Toggle => cachedToggle;

        public UnityEvent OnToggleEnable;

        public void Start() {
            Toggle.onValueChanged.AddListener(toggleValue => {
                OnToggle(toggleValue);
            });
        }

        public void Activate(string text) {
            Toggle.interactable = true;
            displayText.text = text;
        }

        public void OnToggle(bool value) {
            if (value)
                OnToggleEnable?.Invoke();
        }

        public void Deactivate() {
            Toggle.interactable = false;
            displayText.text = "";
        }
    }
}
