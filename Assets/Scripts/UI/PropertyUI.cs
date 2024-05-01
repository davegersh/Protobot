using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot {
    public class PropertyUI : MonoBehaviour {
        [SerializeField] private ObjectLink refObj;
        
        enum ObjProperties {xPos, yPos, zPos, xRot, yRot, zRot}
        [SerializeField] private ObjProperties property;

        [SerializeField] private Text displayedText;
        [SerializeField] private Button button;

        void Update() {
            if (refObj.active) {
                displayedText.text = GetPropertyText();
                button.interactable = true;
            }
            else {
                displayedText.text = "-";
                button.interactable = false;
            }
        }

        private string GetPropertyText() {
            Vector3 pos = refObj.tform.position;
            Vector3 rot = refObj.tform.rotation.eulerAngles;
            
            float value = 0;
            
            if (property == ObjProperties.xPos) value = pos.x;
            if (property == ObjProperties.yPos) value = pos.y;
            if (property == ObjProperties.zPos) value = pos.z;

            if (property == ObjProperties.xRot) value = rot.x;
            if (property == ObjProperties.yRot) value = rot.y;
            if (property == ObjProperties.zRot) value = rot.z;

            return value.ToString("F3");
        }
    }
}