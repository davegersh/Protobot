using UnityEngine;
using System;
using Protobot.InputEvents;

namespace Protobot {
    [RequireComponent(typeof(PivotCamera))]
    public class MousePivotCameraInput : MonoBehaviour, IPivotCameraInput {
        public event Action<Vector2> updateOrbit;
        public event Action<float> updateZoom;
        public event Action<Vector2> updatePan;

        [SerializeField] private float zoomSensitvity = 2f;
        [SerializeField] private float orbitSensitivity = 2f;
        [SerializeField] private bool invertOrbit;
        private int invertOrbitValue => invertOrbit ? -1 : 1;
        [SerializeField] private float panSensitivity = 0.15f;

        [SerializeField] private InputEvent orbitInput;
        [SerializeField] private InputEvent panInput;

        bool disableOrbitInput = false;
        bool disablePanInput = false;

        private void Start() {
            orbitInput.performed += () => {
                if (panInput.IsPressed) {
                    disablePanInput = true;
                }
            };

            panInput.performed += () => {
                if (orbitInput.IsPressed) {
                    disableOrbitInput = true;
                }
            };

            panInput.canceled += () => {
                disablePanInput = false;
                disableOrbitInput = false;
            };

            orbitInput.canceled += () => {
                disableOrbitInput = false;
                disablePanInput = false;
            };
        }

        private void Update() {
            if (orbitInput.IsPressed && !disableOrbitInput) {
                Orbit();
            }

            if (panInput.IsPressed && !disablePanInput) {
                Pan();
            }

            ZoomScroll();
        }

        public void Orbit() {
            if (MouseInput.xAxis != 0 || MouseInput.yAxis != 0) {
                float xRot = MouseInput.yAxis * invertOrbitValue * orbitSensitivity;
                float yRot = MouseInput.xAxis * orbitSensitivity;

                updateOrbit?.Invoke(new Vector2(xRot, yRot));
            }
        }

        public void SetInvertOrbit(bool value) {
            invertOrbit = value;
        }

        public void SetOrbitSensitivity(float newSensitivity) {
            orbitSensitivity = newSensitivity;
        }

        public void ZoomMouseY() {
            float mouseYInput = (MouseInput.yAxis * zoomSensitvity) / 12;
            updateZoom?.Invoke(mouseYInput);
        }

        public void ZoomScroll() {
            if (!MouseInput.overUI && MouseInput.withinScreen) {
                float scrollInput = MouseInput.scrollAxis;
                if (scrollInput != 0)
                    updateZoom?.Invoke(scrollInput * zoomSensitvity);
            }
        }

        public void SetZoomSensitivity(float newSensitivity) {
            zoomSensitvity = newSensitivity;
        }

        public void Pan() {
            Vector2 panValue = new Vector3(MouseInput.xAxis, MouseInput.yAxis) * panSensitivity;
            updatePan?.Invoke(panValue);
        }

        public void SetPanSensitivity(float newSensitivity) {
            panSensitivity = newSensitivity;
        }
    }
}