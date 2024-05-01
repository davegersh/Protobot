using UnityEngine;
using System.IO;

namespace Protobot.Builds.Windows {
    public static class WindowsSavingConfig {
        public static string saveDirectoryPath => Application.dataPath + "/Builds";
        public static string saveFileType => ".Build";
        private static bool OnWindows => Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            if (OnWindows && !Directory.Exists(saveDirectoryPath)) {
                Directory.CreateDirectory(saveDirectoryPath);
            }
        }
    }
}