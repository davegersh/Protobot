using System;
using UnityEngine;

namespace Protobot {
    [Serializable]
    public class UserData {
        public string localId;
        public string pizza = "pizza";
        //public BuildData build;

        public UserData(string newLocalId) {
            localId = newLocalId;
            //build = new BuildData();
        }
    }
}


