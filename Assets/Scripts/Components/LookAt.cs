using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class LookAt : MonoBehaviour {
        [SerializeField] private Transform target; //the transform this object will look at
        public VectorLink vectorLink;
        public Vector3 upVector => vectorLink.Vector; //The vector at which this object will always rotate about

        [SerializeField] private bool singleAxis; //the axis by which this object is only able to move about

        private bool isSingleAxis; //true if singleAxis was enabled on first frame

        void Start() {
            isSingleAxis = singleAxis;
        }

        void Update() {
            if (upVector == Vector3.zero || upVector == Vector3.one)
                singleAxis = false;
            else if (isSingleAxis)
                singleAxis = true;

            Vector3 newPos = target.position;

            if (singleAxis)
                newPos = target.position - Vector3.Project(target.position, upVector) + Vector3.Project(transform.position, upVector);
                
            if (upVector != Vector3.zero)
                transform.LookAt(newPos, upVector);
            else
                transform.rotation = target.rotation * Quaternion.identity;
        }
    }
}
