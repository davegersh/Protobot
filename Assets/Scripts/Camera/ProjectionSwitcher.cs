using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Protobot {
    public class ProjectionSwitcher : MonoBehaviour {
        //Matricies and Projection data
        private Matrix4x4 ortho;
        private Matrix4x4 perspective;
        private float fov = 60f;
        private float near = .3f;
        private float far = 1000f;
        private float orthographicSize = 5f;
        private float aspect;
        private float lastAspect;

        [Header("General")]
        public Camera cam;
        public bool isOrtho;

        [Header("Planes")]
        public GameObject orthoPlane;
        public GameObject persPlane;

        [Header("Switch Data")]
        public float switchDuration;
        public bool switching;

        [Space(10)]

        public UnityEvent OnSwitchToOrtho;
        public UnityEvent OnSwitchToPers;

        private bool enableGrid = true;

        void Start() {
            SetCameraData();
            UpdateOrthoMatrix();
            UpdatePerspectiveMatrix();

            BlendToMatrix(perspective, 0);
        }
    
        void Update() {
            SetCameraData();

            if (isOrtho) {
                UpdateOrthoMatrix();
                if (!switching)
                    cam.projectionMatrix = ortho;
            }
            else {
                UpdatePerspectiveMatrix();
            }

            if (lastAspect != aspect)
                BlendToMatrix(isOrtho ? ortho : perspective, 0);

            lastAspect = aspect;
        }

        public static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time) {
            Matrix4x4 ret = new Matrix4x4();
            for (int i = 0; i < 16; i++)
                ret[i] = Mathf.Lerp(from[i], to[i], time);
            return ret;
        }
    
        private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration) {
            switching = true;
            float startTime = Time.time;
            while (Time.time - startTime < duration) {
                cam.projectionMatrix = MatrixLerp(src, dest, (Time.time - startTime) / duration);
                yield return 1;
            }
            cam.projectionMatrix = dest;
            switching = false;
        }
    
        public Coroutine BlendToMatrix(Matrix4x4 targetMatrix, float duration) {
            StopAllCoroutines();
            return StartCoroutine(LerpFromTo(cam.projectionMatrix, targetMatrix, duration));
        }

        private void SetCameraData() {
            fov = cam.fieldOfView;
            near = cam.nearClipPlane;
            far = cam.farClipPlane;
            orthographicSize = cam.orthographicSize;
            aspect = cam.aspect;
        }

        public void SwitchToOrtho(float duration) {
            BlendToMatrix(ortho, duration);
            isOrtho = true;

            if (enableGrid) {
                orthoPlane.SetActive(true);
                persPlane.SetActive(false);
            }

            OnSwitchToOrtho?.Invoke();
        }
        public void SwitchToPers(float duration) {
            BlendToMatrix(perspective, duration);
            isOrtho = false;

            if (enableGrid) {
                orthoPlane.SetActive(false);
                persPlane.SetActive(true);
            }

            OnSwitchToPers?.Invoke();
        }

        public void ToggleProjection() {
            if (isOrtho) {
                SwitchToPers(switchDuration);
            }
            else {
                SwitchToOrtho(switchDuration);
            }
        }

        public void SetGridActive(bool value) {
            if (value) {
                if (isOrtho) {
                    orthoPlane.SetActive(true);
                    persPlane.SetActive(false);
                }
                else {
                    orthoPlane.SetActive(false);
                    persPlane.SetActive(true);
                }
                enableGrid = true;
            }
            else {
                persPlane.SetActive(false);
                orthoPlane.SetActive(false);
                enableGrid = false;
            }
        }

        private void UpdateOrthoMatrix() {
            orthographicSize = cam.orthographicSize;
            ortho = Matrix4x4.Ortho(-orthographicSize * aspect, orthographicSize * aspect, -orthographicSize, orthographicSize, near, far);
        }

        private void UpdatePerspectiveMatrix() {
            perspective = Matrix4x4.Perspective(fov, aspect, near, far);
        }
    }
}