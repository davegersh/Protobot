using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Protobot.UI {
    [RequireComponent(typeof(Button))]
    public class ButtonCode : MonoBehaviour {
        [CacheComponent] private Button button;
        [SerializeField] private string code;
        [SerializeField] private InputField inputField;
        [SerializeField] private KeyCode submitKey;
        public bool passed => inputField.text == code;

        [Space(10)]
        public UnityEvent onPassedSubmit;

        private void Update() {
            button.interactable = passed;

            if (Input.GetKeyDown(submitKey))
                SubmitPassed();
        }

        public void SubmitPassed() {
            if (passed)
                onPassedSubmit?.Invoke();
        }
    }
}