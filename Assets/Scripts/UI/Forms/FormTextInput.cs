using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI.Forms  {
    public class FormTextInput : MonoBehaviour {

        [Header("Input")]
        [SerializeField] public InputField inputField;
        public string InputText => inputField.text;

        [SerializeField] public string inputName;

        [Header("Validity")]
        [SerializeField] private GameObject validDisplay;
        [SerializeField] private GameObject invalidDisplay;
        [SerializeField] private Tooltip invalidTooltip;

        private bool displaySet = false;

        private ITextValidator[] validators;

        private bool CheckValidity(string text) {
            foreach (ITextValidator validator in validators) {
                if (!validator.IsValid(text))
                    return false;
            }

            return true;
        }

        public bool CheckInputValidity() => CheckValidity(InputText);

        private string GetValidityMessage(string text) {
            string message = "";

            foreach (ITextValidator validator in validators) {
                if (!validator.IsValid(text)) {
                    if (message != "") message += "\n";
                    message += "- " + validator.InvalidMessage;
                }
            }

            return message;
        }

        private void Awake() {
            validators = GetComponents<ITextValidator>();

            inputField.onEndEdit.AddListener(inputText => {
                SetDisplay(inputText);
            });
            inputField.onValueChanged.AddListener(inputText => {
                if (displaySet) {
                    SetDisplay(inputText);
                }
            });
        }

        public void AddValidityCheck(InputField otherInputField) {
            otherInputField.onValueChanged.AddListener(inputText => {
                if (displaySet) {
                    SetDisplay(InputText);
                }
            });
        }

        private void SetDisplay(string text) {
            bool validity = CheckValidity(text);

            validDisplay.SetActive(validity);
            invalidDisplay.SetActive(!validity);

            if (!validity) {
                invalidTooltip.text = GetValidityMessage(text);
            }

            displaySet = true;
        }
    }
}