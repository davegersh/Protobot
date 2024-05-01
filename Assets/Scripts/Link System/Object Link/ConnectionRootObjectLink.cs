using UnityEngine;
using Protobot.SelectionSystem;

namespace Protobot {
    public class ConnectionRootObjectLink : ObjectLink {
        [SerializeField] private ObjectLink link;
        public GameObject originalObj => link.obj;

        public override GameObject obj {
            get {
                Transform root = link.tform.root;

                if (root != null) {
                    return root.gameObject;
                }

                return originalObj;
            }
        }
    }
}