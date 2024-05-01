using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
using Protobot.StateSystems;

namespace Protobot.Tools {
    public abstract class RotationTool : ClickDragTool {
        public abstract Quaternion FinalRotation {get;}

        abstract public void Initialize();
        abstract public void Rotate();

        //Input events
        public override void OnPointerDown() {
            Initialize();
        }

        public override void OnPointerUp() {
        }
    }
}