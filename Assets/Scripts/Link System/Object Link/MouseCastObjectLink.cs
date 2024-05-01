using UnityEngine;

namespace Protobot {
    public class MouseCastObjectLink : ObjectLink {
        public override GameObject obj => mouseCast.gameObject;
        [SerializeField] private MouseCast mouseCast;
    }
}