using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Protobot.InputEvents;
using UnityEngine.UI;

namespace Protobot.UI {
    public class ContextMenuUI : MonoBehaviour {
        [CacheComponent] private RectTransform rectTransform;
        [SerializeField] private bool EnableOnActive = false;
        [SerializeField] private InputEvent enableInput;
        [Space(10)]

        [SerializeField] private UnityEvent OnDisableMenu;

        [Header("Mouse Positioning")]
        [SerializeField] private bool enableAtMousePos = true;
        [SerializeField] private Vector2 offset = Vector2.zero;

        [Header("Padding")]
        [SerializeField] private int menuPadding = 20;
        private RectTransform paddingRect;

        [Header("Debug")]
        [SerializeField] private bool menuEnabled;


        private void Start() {
            DisableMenu();

            CreatePaddingObject();

            if (enableInput != null)
                enableInput.performed += () => EnableMenu();
        }

        public void Update() {
            if (menuEnabled && !CursorWithinMenu())
                DisableMenu();
        }

        private void CreatePaddingObject() {
            GameObject paddingGameObject = new GameObject(
                "Context Menu Padding",
                typeof(RectTransform)
            );

            paddingGameObject.AddComponent<LayoutElement>().ignoreLayout = true;

            paddingGameObject.transform.SetParent(transform);

            paddingRect = paddingGameObject.GetComponent<RectTransform>();

            SetPadding();
            paddingRect.localScale = Vector3.one;
        }

        private void SetPadding() {
            paddingRect.anchorMin = Vector2.zero;
            paddingRect.anchorMax = Vector2.one;

            paddingRect.offsetMin = new Vector2(-menuPadding, -menuPadding);
            paddingRect.offsetMax = new Vector2(menuPadding, menuPadding);
        }

        public bool CursorWithinMenu() => RectTransformUtility.RectangleContainsScreenPoint(paddingRect, MouseInput.Position);

        public void DisableMenu() {
            OnDisableMenu?.Invoke();
            gameObject.SetActive(false);
            menuEnabled = false;
        }

        public void OnEnable() {
            if (EnableOnActive)
                menuEnabled = true;
        }

        public void EnableMenu() {
            gameObject.SetActive(true);

            if (enableAtMousePos) {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);
                rectTransform.position = MouseInput.Position + new Vector3(offset.x, offset.y);

                float xOffset = rectTransform.position.x + (rectTransform.sizeDelta.x / 2) - Screen.width;
                float yOffset = rectTransform.position.y - rectTransform.sizeDelta.y * 1.375f;

                if (xOffset > 0)
                    rectTransform.position += Vector3.left * xOffset;

                if (yOffset < 0)
                    rectTransform.position += Vector3.down * yOffset;
            }

            menuEnabled = true;
        }
    }
}