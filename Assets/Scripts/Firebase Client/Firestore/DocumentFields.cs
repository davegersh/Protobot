using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Newtonsoft.Json;
using System.Reflection;
using System;
using System.Linq;

namespace FirebaseClient.Firestore {
    public static class DocumentFields {
        public static string Serialize(object obj) {
            var docfields = MapValueFormatter.ToDictionary(obj);

            return "";// JsonConvert.SerializeObject(docfields);
        }

        public static Dictionary<string,object> ToProperDict(string fieldsJson) {
            Dictionary<string, object> fieldsDict = null;// JsonConvert.DeserializeObject<Dictionary<string, object>>(fieldsJson);

            var newFieldsDict = new Dictionary<string, object>();

            foreach (KeyValuePair<string, object> entry in fieldsDict) {
                var entryDict = fieldsDict[entry.Key].ToDictionary();
                var typeName = entryDict.Keys.First();

                var value = entryDict[typeName];

                object newValue = value;

                if (typeName == "mapValue") {
                    string mapValueJson = value.ToDictionary()["fields"].ToString();
                    newValue = ToProperDict(mapValueJson);
                }
                else if (typeName == "arrayValue") {
                    string arrayValueJson = value.ToDictionary()["values"].ToString();
                    newValue = ToProperList(arrayValueJson);
                }

                newFieldsDict.Add(entry.Key, newValue);
            }
            return newFieldsDict;
        }

        public static List<object> ToProperList(string arrayJson) {
            List<object> fieldsArray = null;// = JsonConvert.DeserializeObject<List<object>>(arrayJson);

            var newFieldsArray = fieldsArray;

            for (int i = 0; i < fieldsArray.Count; i++) {
                var obj = fieldsArray[i];

                var objDict = obj.ToDictionary();

                var typeName = objDict.Keys.First();

                if (typeName == "mapValue") {
                    string mapValueJson = objDict["mapValue"].ToDictionary()["fields"].ToString();
                    newFieldsArray[i] = ToProperDict(mapValueJson);
                }
                else {
                    newFieldsArray[i] = objDict[typeName];
                }

            }

            return newFieldsArray;
        }

        public static T Deserialize<T>(string fieldsJson) {
            Dictionary<string, object> properDict = ToProperDict(fieldsJson);
           // string properJson = JsonConvert.SerializeObject(properDict);
           // return null; //JsonConvert.DeserializeObject<T>(properJson);
           throw new Exception();
        }

        /// <summary>
        /// Converts any object into a Dictionary<string, object>
        /// </summary>
        public static Dictionary<string, object> ToDictionary(this object obj) {
            /*var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);*/
            throw new Exception();
        }

        /// <summary>
        /// Converts any object into a List<object>
        /// </summary>
        public static List<object> ToObjectList(this object obj) {
            /*var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<List<object>>(json);*/
            throw new Exception();
        }
    }
}