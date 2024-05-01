using UnityEngine;

namespace Protobot {
    public class SerializedObjectLink : ObjectLink {
        public override GameObject obj => serializedObj;
        [SerializeField] private GameObject serializedObj;
    }
}