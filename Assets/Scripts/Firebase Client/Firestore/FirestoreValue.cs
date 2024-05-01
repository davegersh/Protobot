using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace FirebaseClient.Firestore {
    public class FirestoreValue {
        public string name { get; set; }
        public object value { get; set; }

        private string TypeName => GetTypeName(value.GetType());

        private object FormattedValue {
            get {
                if (TypeName == "mapValue") return MapValueFormatter.ToDictionary(value);
                if (TypeName == "arrayValue") return ArrayValueFormatter.ToDictionary(value);
                return value;
            }
        }

        public Dictionary<string, object> TypeValuePair => new Dictionary<string, object> { { TypeName, FormattedValue } };

        public Dictionary<string, object> ToDictionary() {
            return new Dictionary<string, object> {
                {name, TypeValuePair},
            };
        }

        public static string GetTypeName(Type type) {
            if (type == typeof(int)) return "integerValue";
            if (type == typeof(double)) return "doubleValue";
            if (type == typeof(string)) return "stringValue";
            if (type == typeof(bool)) return "booleanValue";

            if (type.IsArray) return "arrayValue";
            return "mapValue";
        }
    }

    public static class MapValueFormatter {
        public static Dictionary<string, object> ToDictionary(object obj) {
            var fieldValues = new Dictionary<string, object>();

            if (obj != null) {
                var objFields = obj.GetType().GetFields();

                foreach (FieldInfo field in objFields) {
                    var fieldValue = field.GetValue(obj);
                    if (fieldValue != null) {
                        var fieldName = field.Name;

                        var fieldFirestoreValue = new FirestoreValue {
                            name = fieldName,
                            value = fieldValue
                        };

                        fieldValues.Add(fieldName, fieldFirestoreValue.TypeValuePair);
                    }
                }
            }

            return new Dictionary<string, object> {
                {"fields", fieldValues}
            };
        }
    }

    public static class ArrayValueFormatter {
        public static Dictionary<string, object> ToDictionary(object obj) {
            if (obj == null) return null;

            var arrayValue = new List<object>();
            var array = obj as object[];

            foreach (object arrayObj in array) {
                if (arrayObj != null) {
                    var fieldFireStoreValue = new FirestoreValue {
                        value = arrayObj
                    };

                    arrayValue.Add(fieldFireStoreValue.TypeValuePair);
                }
            }

            return new Dictionary<string, object> {
                {"values", arrayValue.ToArray()}
            };
        }
    }
}