using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirebaseClient;

namespace Protobot.UI {
    public class LoggedInUI : MonoBehaviour {

        [CacheComponent] private UIMovement movement;


        [SerializeField] private string beforeDisplayName;
        [SerializeField] private Text displayNameText;
        [SerializeField] private string afterDisplayName;

        void Start() {

        }

        void Update() {

        }

        public void UpdateCurrentUserUI() {
            AuthData userData = Authentication.currentUser;

            displayNameText.text = beforeDisplayName + userData.displayName + afterDisplayName;
        }

        public void Show() {
            movement.SetActivePos();
            UpdateCurrentUserUI();
        }

        public void Hide() {
            movement.SetInactivePos();
        }
    }
}
