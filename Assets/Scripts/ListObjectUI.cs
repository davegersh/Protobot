using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot {
    public class ListObjectUI : MonoBehaviour {
        [SerializeField] private Text nameText;
        [SerializeField] private Image objectIcon;
        [SerializeField] private Sprite groupSprite;
        [SerializeField] private GameObject ExpandButton;

        [SerializeField] private GameObject refObj;

        public void SetDisplay(SavedObject savedObject) {
            refObj = savedObject.gameObject;
            ExpandButton.SetActive(false);

            if (savedObject.id == "GRUP") {
                objectIcon.sprite = groupSprite;
                nameText.text = "Group";
                ExpandButton.SetActive(true);
            }
            else {
                /*Part partData = PartsManager.GetPart(savedObject.id);
                objectIcon.sprite = partData.icon;
                nameText.text = partData.sizeName + " " + partData.subTypeName + " " + partData.typeName;*/
            }
        }
    }
}