using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    [CreateAssetMenu(fileName = "New Cursor Modifier")]
    public class CursorModifier : ScriptableObject {
        public Texture2D[] cursorTextures;

        public void SetCursor(int index) {
            Cursor.SetCursor(cursorTextures[index], Vector2.zero, CursorMode.Auto);
        }

        public void ResetCursor() {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
