using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.Transformations;

namespace Protobot {
    public class SetRotation : MonoBehaviour {
        [SerializeField] private MovementManager movementManager;

        [SerializeField] private Vector3 eulerAngles;
        public void Execute() => movementManager.RotateTo(Quaternion.Euler(eulerAngles));
    }
}
