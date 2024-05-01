using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Protobot.Outlining;
using EPOOutline;
using Protobot.Transformations;

namespace Protobot {
    public class GameObjectPlacementData : PlacementData {
        public override Transform placementTransform { get; set; }

        private GameObject gameObject;

        public override string objectId {
            get {
                if (gameObject.TryGetComponent(out SavedObject savedObject))
                    return savedObject.id;
                else
                    return gameObject.tag;
            }
        }

        public GameObjectPlacementData(GameObject gameObject, Transform placementTransform) {
            this.gameObject = gameObject;
            this.placementTransform = placementTransform;
        }

        public override Mesh GetDisplayMesh() {
            var connectedObjs = gameObject.GetConnectedObjects(true);
            return MeshCombiner.CombineMeshes(connectedObjs, gameObject.transform.position, gameObject.transform.rotation);
        }

        public GameObject GetGameObject() => gameObject;
    }
}