using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Protobot.UI;

namespace Protobot.SelectionSystem {
    public class BoxSelector : Selector {
        public override event Action<ISelection> setEvent;
        public override event Action clearEvent;

        [SerializeField] private BoxSelectUI boxSelectUI;
        [SerializeField] private Camera cam;


        void Start() {
            boxSelectUI.OnReleaseBox += SetBoxSelection;
        }

        void SetBoxSelection(Vector2 startPos, Vector2 finalPos) {
            clearEvent?.Invoke();

            List<GameObject> objs = new List<GameObject>();
            Bounds bounds = GetViewportBounds(cam, startPos, finalPos);

            if (Vector3.Distance(startPos, finalPos) > 5) {
                foreach (GameObject loadedObj in PartsManager.FindLoadedObjects()) {
                    if (bounds.Contains(cam.WorldToViewportPoint(loadedObj.transform.position))) {
                        objs.Add(loadedObj);
                    }
                }

                if (objs.Count > 0) {
                    var selection = new MultiSelection(this, objs);
                    setEvent?.Invoke(selection);
                }
            }
        }

        public static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2) {
            var v1 = camera.ScreenToViewportPoint(screenPosition1);
            var v2 = camera.ScreenToViewportPoint(screenPosition2);
            var min = Vector3.Min(v1, v2);
            var max = Vector3.Max(v1, v2);
            min.z = camera.nearClipPlane;
            max.z = camera.farClipPlane;

            var bounds = new Bounds();

            bounds.SetMinMax(min, max);

            return bounds;
        }
    }
}
