using System.Collections;
using UnityEngine;

namespace Protobot.Outlining {
    [CreateAssetMenu(fileName = "New Outline Settings")]
    public class OutlineSettings : ScriptableObject {
        //Singleton set up
        public static OutlineSettings instance { get; private set; }
        public OutlineSettings() {
            instance = this;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            instance = (OutlineSettings)Resources.LoadAll("General Data", typeof(OutlineSettings))[0];
        }

        public static Color GetColor(int index) {
            return instance.colors[index];
        }

        public static float GetDefaultWidth() {
            return instance.defaultWidth;
        }

        public Color[] colors;

        [Space(10)]
        [Range(0,10)]
        public float defaultWidth;
    }
}