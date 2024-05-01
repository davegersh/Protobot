using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class HoleFace : MonoBehaviour {
        public Vector3 direction;
        public Vector3 position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public Quaternion LookRotation => Quaternion.LookRotation(direction, transform.up);
        public HoleData hole;

        [CacheComponent] private MeshFilter meshFilter;

        public void Set(HoleData newHole, Vector3 newDir) {
            transform.rotation = Quaternion.LookRotation(-newDir,  newHole.rotation * Vector3.up);
            
            Vector3 newHolePos = newHole.position;

            Vector3 newPos = newHolePos + (newDir * (newHole.depth / 2));
            transform.position = newPos;
            
            direction = newDir;
            
            meshFilter.mesh = newHole.shape;
            transform.localScale = new Vector3(newHole.size.x, newHole.size.y, 0.001f);

            hole = newHole;
        }

        public void Set(HoleFace newHoleFace) => Set(newHoleFace.hole, newHoleFace.direction);
    }
}