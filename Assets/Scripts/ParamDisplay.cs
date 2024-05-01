using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Protobot {
    public class ParamDisplay : MonoBehaviour, IScrollHandler {
        [SerializeField] private Text title;
        [SerializeField] private Dropdown dropdown;
        [SerializeField] private InputField customInput;
        [SerializeField] private Text customUnit;

        [SerializeField] private UnityEvent OnSetDisplay;
        [SerializeField] private StringUnityEvent OnUpdateValue;

        private List<string> dropdownOptions;
        private Parameter parameter;
        
        private int ParamIndex => dropdownOptions.IndexOf(parameter.value);

        private void Start() {
            dropdown.onValueChanged.AddListener(index => {
                OnUpdateValue?.Invoke(dropdown.options[index].text);
            });
            
            customInput.onValueChanged.AddListener(inputText => {
                if (inputText.Length > 0)
                    OnUpdateValue?.Invoke(ClampCustomInput(parameter, inputText));
            });
            
            customInput.onEndEdit.AddListener(inputText => {
                customInput.SetTextWithoutNotify(parameter.value);
            });
        }

        public void SetDisplay(Parameter parameter, List<string> options) {
            bool sameParam = this.parameter == parameter;
            
            this.parameter = parameter;

            title.text = parameter.name + ":";
            if (!sameParam) SetCustomDisplay(parameter);

            if (!parameter.custom) {
                SetDropdownOptions(options);
                dropdown.value = ParamIndex;
            }
            

            OnSetDisplay?.Invoke();
        }
        
        public void SetDropdownOptions(List<string> options) {
            dropdown.ClearOptions();
            dropdown.AddOptions(options);
            dropdownOptions = options;

            dropdown.interactable = options.Count > 1;

        }

        public void SetCustomDisplay(Parameter p) {
            bool enable = p.custom;
            
            customInput.gameObject.SetActive(enable);
            dropdown.gameObject.SetActive(!enable);

            if (enable) {
                customUnit.text = parameter.customUnit;
                customInput.text = parameter.customDefault;

                if (p.customUnit == "Holes") {
                    customInput.contentType = InputField.ContentType.IntegerNumber;
                }
                else {
                    customInput.contentType = InputField.ContentType.DecimalNumber;
                }
            }
        }

        private string ClampCustomInput(Parameter p, String value) {
            float valueFloat = float.Parse(value);
            
            if (valueFloat > p.customLimits.y) return p.customLimits.y.ToString();
            if (valueFloat < p.customLimits.x) return p.customLimits.x.ToString();
            
            return value;
        }

        public void OnScroll(PointerEventData eventData) {
            int dir = (eventData.scrollDelta.y < 1) ? -1 : 1;
            
            if (parameter.custom) {
                float newVal = float.Parse(parameter.value) + dir;

                if (newVal >= parameter.customLimits.x && newVal <= parameter.customLimits.y)
                    customInput.text = newVal.ToString();
            }
            else {
                int newIndex = dropdown.value + dir;

                if (newIndex < dropdownOptions.Count && newIndex >= 0)
                    dropdown.value = newIndex;
            }
        }
    }
}