using UnityEngine;
using System.Collections;
using FirebaseClient.Firestore;
using Protobot.UserManagement;
using System;

namespace Protobot.Builds.Firestore {
    public class FirestoreBuildHandler : MonoBehaviour, IBuildHandler {
        public void Save(BuildData buildData) {
            var docRef = UsersDatabase.GetUserDoc()
                .Collection("builds")
                .Document(buildData.fileName);

            var fieldData = DocumentFields.Serialize(buildData);
            docRef.Set(fieldData);
        }

        public void Delete(BuildData buildData) {
            UsersDatabase.GetUserDoc()
                .Collection("builds")
                .Document(buildData.fileName)
                .Delete();
        }

        public DateTime GetExactWriteTime(BuildData buildData) {
            return DateTime.Parse(buildData.lastWriteTime);
        }
    }
}