using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Protobot.UI {
    public class PullOutMenu : MonoBehaviour {
        public UIMovement movement;

        public void OpenMenu() {
            movement.SetActivePos();
        }

        public void CloseMenu() {
            movement.SetInactivePos();
        }

        public void ToggleMenu() {
            if (movement.atActivePos) {
                CloseMenu();
            }
            else if (movement.atInactivePos) {
                OpenMenu();
            }
        }
    }
}