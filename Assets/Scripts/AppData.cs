using UnityEngine;

namespace Protobot {
    public static class AppData {
        public static string Version;

        [RuntimeInitializeOnLoadMethod]
        public static void Init() {
            Version = Application.version;
        }
    }
}