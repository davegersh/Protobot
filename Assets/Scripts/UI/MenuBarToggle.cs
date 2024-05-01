using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Protobot.UI {
    [RequireComponent(typeof(Toggle))]
    public class MenuBarToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        private Toggle toggle;
        [SerializeField] private GameObject menu;

        public bool isMouseOver = false;
        private bool LeftMousePressed => Mouse.current.leftButton.wasReleasedThisFrame;
        private bool AnyMenuTogglesOn => toggle.group.AnyTogglesOn();
        
        private void Start() {
            toggle = GetComponent<Toggle>();

            toggle.onValueChanged.AddListener(value => {
                menu.SetActive(value);
            });
        }

        private void Update() {
            if (toggle.isOn && !isMouseOver && LeftMousePressed)
                toggle.group.SetAllTogglesOff();
            }

        public void OnPointerEnter(PointerEventData eventData) {
            if (AnyMenuTogglesOn && !isMouseOver)
                toggle.isOn = true;

            isMouseOver = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            isMouseOver = false;
        }
    }
}
