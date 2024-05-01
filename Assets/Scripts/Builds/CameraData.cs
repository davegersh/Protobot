using UnityEngine;
using System.Collections;
using System;

namespace Protobot.Builds {
    [Serializable]
    public class CameraData {
        public double xPos, yPos, zPos;
        public double xRot, yRot, zRot;
        public double zoom;
        public bool isOrtho;
    }
}