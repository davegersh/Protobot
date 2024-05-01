using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class SavedObject : MonoBehaviour {
        public string id;
        public string nameId => id.Split('-')[0];
        public string state;
    }
}