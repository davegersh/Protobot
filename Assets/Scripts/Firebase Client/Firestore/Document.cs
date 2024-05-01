using System;

namespace FirebaseClient.Firestore {
    [Serializable]
    public class Document {
        public string name;
        public object fields;
        public string createTime;
        public string updateTime;

        public T ConvertFields<T>() {
            return DocumentFields.Deserialize<T>(fields.ToString());
        }
    }
}