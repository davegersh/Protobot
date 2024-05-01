using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using RSG;

namespace FirebaseClient {
    public static class Authentication {
        private const string apiKey = "INSERT API KEY HERE";

        public static Action<AuthData> OnSignUp;
        public static Action<AuthData> OnLogin;

        public static AuthData currentUser;
        public static Action<AuthData> OnUpdateCurrent;

        public static void UpdateCurrentUser(AuthData userData) {
            currentUser = userData;
            OnUpdateCurrent?.Invoke(userData);
        }

        public static RequestHelper AddCurrentUserAuth(RequestHelper requestHelper) {
            var newRequest = requestHelper;

            newRequest.Headers = new Dictionary<string, string> {
                {"Authorization", "Bearer " + currentUser.idToken }
            };

            return newRequest;
        }

        public static void SetAuthHeader(string token) {
            RestClient.DefaultRequestHeaders = new Dictionary<string, string> {
                { "Authorization", "Bearer " + token }
            };
        }

        public static void ClearAuthHeader() => RestClient.ClearDefaultHeaders();

        public static IPromise<AuthData> SignUp(string email, string password) {
            string payload = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";

            var promise = RestClient.Post<AuthData>($"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={apiKey}", payload);

            promise
                .Then(response => {
                    Debug.Log("Successfully Signed Up user " + email);
                    UpdateCurrentUser(response);
                    OnSignUp?.Invoke(response);
                })
                .Catch(error => {
                    Debug.LogError("Error during signup: " + error.ToFirebaseException().rawMessage);
                });

            return promise;
        }

        public static IPromise<AuthData> Login(string email, string password) {
            string payload = $"{{\"email\":\"{email}\",\"password\":\"{password}\",\"returnSecureToken\":true}}";

            var promise = RestClient.Post<AuthData>($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}", payload);

            promise
                .Then(response => {
                    Debug.Log("Successfully Logged In user " + email);
                    UpdateCurrentUser(response);
                    OnLogin?.Invoke(response);
                })
                .Catch(error => {
                    Debug.LogError("Error during login: " + error.ToFirebaseException().rawMessage);
                });


            return promise;
        }

        public static IPromise<AuthData> UpdateProfile(string idToken, string displayName) {
            string payload = $"{{\"idToken\":\"{idToken}\",\"displayName\":\"{displayName}\",\"returnSecureToken\":false}}";

            var promise = RestClient.Post<AuthData>($"https://identitytoolkit.googleapis.com/v1/accounts:update?key={apiKey}", payload);

            promise
                .Then(response => {
                    Debug.Log("Successfully Updated Profile for user " + response.email);
                    response.idToken = idToken;
                    UpdateCurrentUser(response);  
                })
                .Catch(error => {
                    Debug.LogError("Error when updating profile: " + error.ToFirebaseException().rawMessage);
                });

            return promise;
        }
    }
}
