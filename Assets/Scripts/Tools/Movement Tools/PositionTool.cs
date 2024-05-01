using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.EventSystems;
using Protobot.StateSystems;

namespace Protobot.Tools {
    public abstract class PositionTool : ClickDragTool {
        public static bool snapping = true;
        public abstract Vector3 FinalPosition {get;}

        /// <summary> Executes right when mouse is down before dragging </summary>
        abstract public void Initialize();
        abstract public void Move();

        //Input events
        public override void OnPointerDown() {
            Initialize();
        }

        public override void OnPointerUp() {
        }

        public Vector3 MoveToPos(Vector3 pos) {
            if (snapping)
                pos = pos.Round(0.125f);

            movementManager.MoveTo(pos);
            return pos;
        }
    }
}