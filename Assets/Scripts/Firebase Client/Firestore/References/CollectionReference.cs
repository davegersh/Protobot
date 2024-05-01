using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using RSG;
using System;
//using Newtonsoft.Json;

namespace FirebaseClient.Firestore {
    public class CollectionReference {
        readonly string path;
        readonly string name;
        public CollectionReference(string docPath, string colName) {
            path = docPath + "/" + colName;
            name = colName;
        }

        public DocumentReference Document(string docName) => new DocumentReference(path, docName);

        public Promise<Document[]> GetDocuments(bool requireCurrentUserAuth = true) {
            var requestData = new RequestHelper {
                Uri = path,
                Method = "GET"
            };

            if (requireCurrentUserAuth) requestData = Authentication.AddCurrentUserAuth(requestData);

            var promise = new Promise<Document[]>();

            var requestPromise = RestClient.Request(requestData)
                .Then(response => {
                    string responseJson = response.Text;

                    //var docsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseJson);
                    //var docsJson = JsonConvert.SerializeObject(docsDict["documents"]);
                    Document[] docs = null;// = JsonConvert.DeserializeObject<Document[]>(docsJson);

                    promise.Resolve(docs);

                    Debug.Log("Successfully retreived documents in collection " + name + " at " + path);
                })
                .Catch(error => {
                    promise.Reject(error);
                    Debug.LogError("Error getting collection's documents: " + error.GetFirebaseRawMessage());
                });

            return promise;
        }
    }
}