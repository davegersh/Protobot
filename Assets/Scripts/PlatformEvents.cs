using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Protobot {
    public class PlatformEvents : MonoBehaviour {
        public UnityEvent OnWebStart;
        public UnityEvent OnWindowsStart;
        public UnityEvent OnMacStart;
        public UnityEvent OnLinuxStart;

        private void Awake() {
            if (AppPlatform.OnWindows) {
                //Application.targetFrameRate = 60;
                OnWindowsStart?.Invoke();
            }
            else if (AppPlatform.OnWeb) {
                OnWebStart?.Invoke();
            }
            else if (AppPlatform.OnMac) {
                OnMacStart?.Invoke();
            }
            else if (AppPlatform.OnLinux) {
                OnLinuxStart?.Invoke();
            }
        }

        public void SetFullscreen(bool value) {
            Screen.fullScreenMode = value ? FullScreenMode.ExclusiveFullScreen : FullScreenMode.Windowed;
        }
    }
}
