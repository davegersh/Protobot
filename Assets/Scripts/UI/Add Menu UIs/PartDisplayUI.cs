using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using Protobot.StateSystems;

namespace Protobot.UI {
    public class PartDisplayUI : MonoBehaviour {
        public static PartDisplayUI selected;
        public static event Action<PartDisplayUI> OnChangeSelected;

        public PartType partType;
        public Text nameText;
        public Image mainIcon;
        public Image bgIcon;

        public void SetDisplay(PartType partType) {
            nameText.text = partType.name;

            mainIcon.sprite = bgIcon.sprite = partType.icon;

            this.partType = partType;
        }

        public void ToggleCurrent(bool enabled) {
            if (enabled) {
                selected = this;
                OnChangeSelected?.Invoke(this);
            }
        }
    }
}