using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FirebaseClient;
using FirebaseClient.Firestore;
using Protobot.Builds;
using System;

namespace Protobot.UserManagement {
    public static class UsersDatabase {
        [RuntimeInitializeOnLoadMethod]
        public static void Init() {
            var objData1 = new ObjectData {
                xPos = 1890283.2,
                yPos = 12938120.129083,
                zPos = 0.001,
                xRot = 0,
                yRot = 0,
                zRot = 90,
                partId = "holymolies",
                states = "",
            };

            var objData2 = new ObjectData {
                xPos = 1890283.2,
                yPos = 12938120.129083,
                zPos = 0.001,
                xRot = 0,
                yRot = 0,
                zRot = 90,
                partId = "wowsers",
                states = "",
            };

            var objData3 = new ObjectData {
                xPos = 123123,
                yPos = 6345345.123,
                zPos = 0.001,
                xRot = 0,
                yRot = 0,
                zRot = 23423,
                partId = "poop",
                states = "",
            };

            var docData = new BuildData {
                parts = new ObjectData[] { objData1, objData2, objData3 },
                camera = new CameraData {
                    xPos = 0,
                    yPos = 0,
                    zPos = 2138.023,
                    xRot = 30.023,
                    yRot = 17.229388,
                    zRot = 0.32,
                    zoom = 23.40103
                }
            };

            Authentication.OnSignUp += CreateUserDocument;
        }
        public static void CreateUserDocument(AuthData authResponse) {
            var userDoc = FirestoreConfig.DefaultDb.Collection("users").Document(authResponse.localId);
            userDoc.Set(null);
        }

        public static DocumentReference GetUserDoc() {
            return FirestoreConfig.DefaultDb.Collection("users").Document(Authentication.currentUser.localId);
        }
    }
}
