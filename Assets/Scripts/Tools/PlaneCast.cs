using UnityEngine;

namespace Protobot {
    public class PlaneCast : MonoBehaviour {
        [SerializeField] private new Camera camera = null;
        public Ray ray => camera.ScreenPointToRay(MouseInput.Position);
        public RaycastHit hit {
            get {
                if (gameObject.GetComponent<Collider>().Raycast(ray, out RaycastHit lastHit, Mathf.Infinity))
                    return lastHit;
                else
                    return new RaycastHit();
            }
        }
        public bool hasHit => (hit.collider != null);
        public Vector3 point => hit.point;
    }
}
