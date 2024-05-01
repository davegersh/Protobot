using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI {
    public class PullOutMenuTab : PullOutMenu {
        [Header("UI Components")]
        [SerializeField] private Image tabIconImage;
        [SerializeField] private Text tabTitleText;
        [SerializeField] private Image tabImage;

        [Header("Enabled")]
        [SerializeField] private Color enabledColor;

        [Header("Defaults")]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private string defaultText;

        [Header("Disabled")]
        [SerializeField] private Sprite disabledSprite;
        [SerializeField] private string disabledText;
        [SerializeField] private Color disabledColor;

        public void Start() {
            DisplayDisabled();
        }

        public void SetTab(Sprite setIcon, string setText) {
            tabIconImage.sprite = setIcon;
            tabTitleText.text = setText;
        }

        public void DisplayEnabled(bool setDefaults) {
            tabImage.color = enabledColor;

            if (setDefaults) {
                SetTab(defaultSprite, defaultText);
            }
        }

        public void DisplayDisabled() {
            tabImage.color = disabledColor;
            SetTab(disabledSprite, disabledText);
        }
    }
}