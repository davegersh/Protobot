using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI {
    public class AddPropertiesUI : MonoBehaviour {
        [SerializeField] private Placement placement;

        [SerializeField] private CanvasGroup upperAddMenu;
        [SerializeField] private Text titleText;

        [SerializeField] private float singleParamSize = 335;
        [SerializeField] private float doubleParamSize = 515;

        [SerializeField] private ParamDisplay param1Display;
        [SerializeField] private ParamDisplay param2Display;

        private PartType partType;
        private PartGenerator generator;
        void Start() {
            PartDisplayUI.OnChangeSelected += UpdateDisplay;
            upperAddMenu.alpha = 0;
        }
        
        public void UpdateDisplay(PartDisplayUI partDisplayUI) {
            partType = partDisplayUI.partType;

            generator = partType.gameObject.GetComponent<PartGenerator>();

            if (generator.UsesParams) {
                titleText.text = "Add " + partType.name;

                UpdateDisplayedDropdowns(generator.UsesTwoParams);

                upperAddMenu.alpha = 1;
            }
            else {
                upperAddMenu.alpha = 0;
            }
            
            UpdatePlacement();
        }

        public void UpdateDisplayedDropdowns(bool usesTwoParams) {
            RectTransform rectTransform = upperAddMenu.GetComponent<RectTransform>();
            float sizeY = doubleParamSize;

            param1Display.gameObject.SetActive(true);
            param2Display.gameObject.SetActive(true);
            SetDropdown1();
            
            if (!usesTwoParams) {
                sizeY = singleParamSize;
                param2Display.gameObject.SetActive(false);
            }

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, sizeY);
        }

        public void SetDropdown1() {
            param1Display.SetDisplay(generator.param1, generator.GetParam1Options());
            
            if (generator.UsesTwoParams)
                SetDropdown2();
        }
        
        public void SetDropdown2() {
            param2Display.SetDisplay(generator.param2, generator.GetParam2Options());
        }

        public void UpdateParam1(string value) {
            generator.param1.value = value;
            SetDropdown2();
            UpdatePlacement();
        }

        public void UpdateParam2(string value) {
            generator.param2.value = value;
            UpdatePlacement();
        }

        private void UpdatePlacement() {
            var placementData = new PartPlacementData(generator, partType, placement.transform);

            placement.StartPlacing(placementData);
        }
    }
}