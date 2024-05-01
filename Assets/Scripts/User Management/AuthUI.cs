using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;
using UnityEngine.UI;
using FirebaseClient;
using Protobot.UI.Forms;

namespace Protobot.UI {
    public class AuthUI : MonoBehaviour {
        [CacheComponent] private UIMovement movement;

        [Header("Sign Up")]
        [SerializeField] private Form signUpForm;
        [SerializeField] private Tooltip signUpErrorTooltip;

        [SerializeField] private InputField displayNameInput;
        [SerializeField] private InputField signupEmailInput;

        [SerializeField] private InputField signupPasswordInput;

        [Header("Login")]
        [SerializeField] private Form loginForm;
        [SerializeField] private Tooltip loginErrorTooltip;

        [SerializeField] private InputField loginEmailInput;
        [SerializeField] private InputField loginPasswordInput;

        [Space(10)]

        [SerializeField] private UnityEvent OnLoggedIn;

        private void Start() {
            signUpForm.OnUpdateAnyInput += DisableSignUpErrorTooltip;
            loginForm.OnUpdateAnyInput += DisableLoginErrorTooltip;
        }

        public void DisableSignUpErrorTooltip() => signUpErrorTooltip.gameObject.SetActive(false);
        public void DisableLoginErrorTooltip() => loginErrorTooltip.gameObject.SetActive(false);


        public void SignUpUser() {
            Authentication.SignUp(signupEmailInput.text, signupPasswordInput.text)
                .Then(response => {
                    DisableSignUpErrorTooltip();
                    return Authentication.UpdateProfile(response.idToken, displayNameInput.text);
                })
                .Then(response => {
                    OnLoggedIn?.Invoke();
                })
                .Catch(error => {
                    signUpErrorTooltip.gameObject.SetActive(true);
                    signUpErrorTooltip.text = error.GetFirebaseMessage();
                });
        }

        public void LoginUser() {
            Authentication.Login(loginEmailInput.text, loginPasswordInput.text)
                .Then(response => {
                    DisableLoginErrorTooltip();
                    OnLoggedIn?.Invoke();
                })
                .Catch(error => {
                    loginErrorTooltip.gameObject.SetActive(true);
                    loginErrorTooltip.text = error.GetFirebaseMessage();
                });
        }

        public void Show() {
            movement.SetActivePos();
        }

        public void Hide() {
            movement.SetInactivePos();
        }
    }
}