using RSG;
using UnityEngine;
using Proyecto26;
//using Newtonsoft.Json;
using System.Collections.Generic;

namespace FirebaseClient.Firestore {
    public class DocumentReference {
        readonly string collectionPath;
        readonly string path;
        readonly string name;
        public DocumentReference(string colPath, string docName) {
            path = colPath + "/" + docName;
            collectionPath = colPath;
            name = docName;
        }
        public CollectionReference Collection(string colName) => new CollectionReference(path, colName);

        public IPromise Set(string fieldData, bool requireCurrentUserAuth = true) {
            var requestData = new RequestHelper {
                Uri = path,
                Method = "PATCH",
                BodyString = fieldData,
            };

            if (requireCurrentUserAuth) requestData = Authentication.AddCurrentUserAuth(requestData);

            var promise = RestClient.Request(requestData)
                .Then(response => {
                    Debug.Log("Successfully set document " + name + " at " + path);
                })
                .Catch(error => {
                    Debug.LogError("Error creating document: " + error.GetFirebaseRawMessage());
                });

            return promise;
        }

        public IPromise<T> Get<T>(bool requireCurrentUserAuth = true) {
            var requestData = new RequestHelper {
                Uri = path,
                Method = "GET"
            };

            if (requireCurrentUserAuth) requestData = Authentication.AddCurrentUserAuth(requestData);

            var promise = new Promise<T>();

            var requestPromise = RestClient.Request(requestData)
                .Then(response => {
                    string responseJson = response.Text;
                    Document doc = null;// = JsonConvert.DeserializeObject<Document>(responseJson);

                    promise.Resolve(doc.ConvertFields<T>());

                    Debug.Log("Successfully retreived document " + name + " at " + path);
                })
                .Catch(error => {
                    promise.Reject(error);
                    Debug.LogError("Error getting document: " + error.GetFirebaseRawMessage());
                });

            return promise;
        }

        public IPromise Delete(bool requireCurrentUserAuth = true) {
            var requestData = new RequestHelper {
                Uri = path,
                Method = "DELETE",
            };

            if (requireCurrentUserAuth) requestData = Authentication.AddCurrentUserAuth(requestData);

            var promise = RestClient.Request(requestData)
                .Then(response => {
                    Debug.Log("Successfully deleted document " + name + " at " + path);
                })
                .Catch(error => {
                    Debug.LogError("Error deleting document: " + error.GetFirebaseRawMessage());
                });

            return promise;
        }
    }
}