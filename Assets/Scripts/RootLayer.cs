using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class RootLayer : MonoBehaviour {
        void Update() {
            var rootLayer = transform.root.gameObject.layer;

            if (gameObject.layer != rootLayer)
                gameObject.layer = rootLayer;
        }
    }
}
