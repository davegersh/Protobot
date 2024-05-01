using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Protobot.Builds {
    [Serializable]
    public class BuildData {
        public string name;
        public string fileName; //the name used to determine path

        public ObjectData[] parts = Array.Empty<ObjectData>();
        public CameraData camera;

        public string lastWriteTime;

        public string version = AppData.Version;

        public bool CompareData(BuildData data) {
            if (data.parts.Length != parts.Length) {
                return false;
            }

            foreach (ObjectData part in parts) {
                if (!data.parts.Contains(part))
                    return false;
            }

            return true;
        }
    }
}