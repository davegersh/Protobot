using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI {
    public class QualitySetting : MonoBehaviour {
        [SerializeField] private Text qualityText;
        [SerializeField] private IntEvent OnSetQuality;

        private int CurrentQualityLevel => QualitySettings.GetQualityLevel();

        void Start() {
            SetQualityText(CurrentQualityLevel);
        }

        public void SetQualityText(int qualityLevel) {
            qualityText.text = QualitySettings.names[qualityLevel];
        }

        public void SetQuality(int newQualityLevel) {
            QualitySettings.SetQualityLevel(newQualityLevel);
            OnSetQuality?.Invoke(CurrentQualityLevel);
        }

        public void IncQuality() {
            QualitySettings.IncreaseLevel();
            OnSetQuality?.Invoke(CurrentQualityLevel);
        }

        public void DecQuality() {
            QualitySettings.DecreaseLevel();
            OnSetQuality?.Invoke(CurrentQualityLevel);
        }
    }
}

