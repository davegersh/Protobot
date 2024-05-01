using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using FirebaseClient.Firestore;
using Protobot.UserManagement;

namespace Protobot.Builds.Firestore {
    public class FirestoreBuildsListSource : MonoBehaviour, IBuildsListSource {
        public event Action<List<BuildData>> OnGetData;

        public void GetData() {
            var buildsCollection = UsersDatabase.GetUserDoc().Collection("builds");

            buildsCollection.GetDocuments()
                .Then(response => {
                    List<BuildData> buildsList = new List<BuildData>();

                    foreach (Document doc in response) {
                        var build = doc.ConvertFields<BuildData>();
                        buildsList.Add(build);
                    }

                    OnGetData.Invoke(buildsList);
                });
        }
    }
}