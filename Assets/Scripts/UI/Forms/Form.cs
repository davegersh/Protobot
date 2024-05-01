using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Protobot.UI.Forms {
    public class Form : MonoBehaviour {
        [Header("Text Inputs (Ordered by navigation)")]
        [SerializeField] private FormTextInput[] inputs;
        [SerializeField] private Button submitButton;

        public Action OnUpdateAnyInput;

        private bool AllValid {
            get {
                foreach (FormTextInput input in inputs) {
                    if (!input.CheckInputValidity())
                        return false;
                }
                return true;
            }
        }

        private void Start() {
            foreach (FormTextInput input in inputs) {
                input.inputField.onValueChanged.AddListener(value => {
                    OnUpdateAnyInput?.Invoke();
                });
            }

            OnUpdateAnyInput += () => {
                submitButton.interactable = AllValid;
            };

            submitButton.interactable = false;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                SelectNextInput();
            }
        }

        public void SelectNextInput() {
            int currentInput = GetCurrentInput();

            if (currentInput == -1) return;

            int nextInput = currentInput + 1;
            if (nextInput >= inputs.Length)
                nextInput = 0;

            inputs[nextInput].inputField.Select();
        }

        public int GetCurrentInput() {
            for (int i = 0; i < inputs.Length; i++) {
                if (inputs[i].inputField.isFocused)
                    return i;
            }
            return -1;
        }

        public void ClearAllInputs() {
            foreach (FormTextInput input in inputs) {
                input.inputField.text = "";
            }
        }
    }
}