using UnityEngine;

namespace Protobot {
[RequireComponent(typeof(Camera))]
    public class MouseCast : MonoBehaviour {
        [CacheComponent] private new Camera camera;
        public Ray ray => camera.ScreenPointToRay(MouseInput.Position);
        public RaycastHit hit {
            get {
                if (Physics.Raycast(ray, out RaycastHit lastHit, Mathf.Infinity, layerMask) && lastHit.collider.gameObject.transform.root.gameObject.layer != Placement.PLACEMENT_LAYER)
                    return lastHit;
                else
                    return new RaycastHit();
            }
        }
        public bool overObj => (hit.collider != null);
        public new GameObject gameObject => overObj ? hit.collider.gameObject : null;
        public new Transform transform => gameObject.transform;

        public int layerMask;

        private void Awake() {
            ResetLayerMask();
        }

        public void ResetLayerMask() => layerMask = LayerMask.GetMask("Default", "HoleCollisions");

        public void SetHolesOnlyMode(bool value) {
            if (value)
                layerMask = LayerMask.GetMask("HoleCollisions");
            else
                ResetLayerMask();
        }
    }
}