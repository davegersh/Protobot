using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using System;
//using Newtonsoft.Json;
using System.Collections.Specialized;

namespace FirebaseClient {
    public class FirebaseException {
        public int code;
        public string id;
        public string message;
        public string rawMessage;

        private StringDictionary idMessage = new StringDictionary {
            { "INVALID_EMAIL", "The email address must be in a valid format." },
            { "EMAIL_EXISTS", "The email address is already in use by another account." },
            { "OPERATION_NOT_ALLOWED", "This operation is not allowed." },
            { "EMAIL_NOT_FOUND", "No account found with this email."},
            { "INVALID_PASSWORD", "Incorrect email/password combination." },
            { "USER_DISABLED", "The user account has been disabled by an administrator." },
            { "TOO_MANY_ATTEMPTS_TRY_LATER", "We have blocked all requests from this device due to unusual activity. Try again later." },
            { "EXPIRED_OOB_CODE", "The action code has expired." },
            { "INVALID_OOB_CODE", "The action code is invalid. This can happen if the code is malformed, expired, or has already been used." },
            { "INVALID_ID_TOKEN", "The user's credential is no longer valid. Sign in again." },
            { "WEAK_PASSWORD", "The password must be 6 characters long or more." },
            { "USER_NOT_FOUND", "There is no user record corresponding to this identifier. The user may have been deleted." },
            { "TOKEN_EXPIRED", "The user's credential is no longer valid. Sign in again." },
            { "CREDENTIAL_TOO_OLD_LOGIN_AGAIN", "The user's credential is no longer valid. Sign in again." }
        };

        public FirebaseException(Exception exception) {
            RequestException requestException = exception as RequestException;
            rawMessage = requestException.Response;

            /*var dict = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(rawMessage);
            var errorDict = dict["error"];

            code = int.Parse(errorDict["code"].ToString());*/

            string fullMessage = ""; //= dict["error"]["message"].ToString();

            id = fullMessage.Split(':')[0];

            message = idMessage[id];
        }
    }
    public static class FirebaseExceptionExtension {
        public static FirebaseException ToFirebaseException(this Exception exception) => new FirebaseException(exception);
        public static string GetFirebaseMessage(this Exception exception) => new FirebaseException(exception).message;
        public static string GetFirebaseRawMessage(this Exception exception) => new FirebaseException(exception).rawMessage;
        public static string GetFirebaseExceptionId(this Exception exception) => new FirebaseException(exception).id;
        public static int GetFirebaseExceptionCode(this Exception exception) => new FirebaseException(exception).code;
    }
}
