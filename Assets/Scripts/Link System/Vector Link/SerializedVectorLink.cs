using UnityEngine;
using System.Collections;

namespace Protobot {
    public class SerializedVectorLink : VectorLink {
        [SerializeField] private Vector3 vector;
        public override Vector3 Vector => vector;
    }
}