using System;

namespace FirebaseClient {
    [Serializable]
    public class AuthData {
        public string localId;
        public string idToken;
        public string email;
        public string displayName;
    }
}
