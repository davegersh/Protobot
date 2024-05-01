using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Protobot {
    public static class MouseInput {
        public static Mouse CurrentMouse => Mouse.current;

        public static ButtonControl LeftButton => CurrentMouse.leftButton;
        public static ButtonControl MiddleButton => CurrentMouse.middleButton;
        public static ButtonControl RightButton => CurrentMouse.rightButton;

        public static Vector3 Position => CurrentMouse.position.ReadValue();
        public static float xAxis => CurrentMouse.delta.x.ReadValue();
        public static float yAxis => CurrentMouse.delta.y.ReadValue();
        public static float scrollAxis => CurrentMouse.scroll.y.ReadValue();
        public static bool overUI => EventSystem.current.IsPointerOverGameObject();
        public static bool withinScreen => new Rect(0,0, Screen.width, Screen.height).Contains(Position);
    }
}