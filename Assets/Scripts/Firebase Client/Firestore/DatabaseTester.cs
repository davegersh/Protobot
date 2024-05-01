using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FirebaseClient.Firestore {
    public static class DatabaseTester {
        //[RuntimeInitializeOnLoadMethod]
        public static void RunTest() {
            var testData = new SimpleTestData {
                testInt = 420,
                testString = "wowsers",
                testBool = true,
                testDouble = 4.2069
            };

            var dataJson = DocumentFields.Serialize(testData);

            var docRef = FirestoreConfig.DefaultDb.Collection("test").Document("FieldGen4");
            //docRef.Set(dataJson, false);

            docRef.Get<SimpleTestData>(false)
                .Then(response => {
                    Debug.Log(response.ToString());
                })
                .Catch(error => {
                    Debug.Log("error during document get / conversion: " + error.Message);
                });

        }
    }

    public class SimpleTestData {
        public int testInt;
        public string testString;
        public bool testBool;
        public double testDouble;

        public new string ToString() {
            return "SimpleTestData values: \ntestInt: " + testInt + "\ntestString: " + testString + "\ntestBool: " + testBool + "\ntestDouble: " + testDouble + "\n";
        }
    }

    public class AdvancedTestData {
        public SimpleTestData[] dataObjectArray;
    }
}
