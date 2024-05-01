using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Misc {
    public class ForwardTest : MonoBehaviour {
        void Update() {
            //print(transform.forward == transform.rotation * Vector3.forward);
            if (Keyboard.current.oKey.isPressed) {
                transform.rotation = Quaternion.LookRotation(Vector3.up);
            }

            if (Keyboard.current.pKey.isPressed) {
                transform.forward = Vector3.up;
            }

            if (Keyboard.current.rKey.isPressed) {
                transform.rotation = Quaternion.identity;
            }
        }
    }
}