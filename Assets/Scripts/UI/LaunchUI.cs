using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI {
    public class LaunchUI : MonoBehaviour {
        [CacheComponent] private Canvas canvas;
        [CacheComponent] private GraphicRaycaster graphicRaycaster;

        [SerializeField] private AuthUI authUI;
        [SerializeField] private LoggedInUI loggedInUI;

        public void SetToAuth() {
            authUI.Show();
            loggedInUI.Hide();
        }

        public void SetToLoggedIn() {
            authUI.Hide();
            loggedInUI.Show();
        }

        public void DisplayLaunchUI(bool enabled) {
            canvas.enabled = enabled;
            graphicRaycaster.enabled = enabled;
        }
    }
}