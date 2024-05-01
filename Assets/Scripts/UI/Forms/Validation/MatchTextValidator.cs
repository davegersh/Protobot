using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI.Forms {
    public class MatchTextValidator : MonoBehaviour, ITextValidator {
        [SerializeField] InputField inputField;

        private void Awake() {
            FormTextInput thisInput = GetComponent<FormTextInput>();

            thisInput.AddValidityCheck(inputField);
        }

        public bool IsValid(string text) => text == inputField.text;

        [SerializeField] private string invalidMessage;
        public string InvalidMessage => invalidMessage;
    }
}