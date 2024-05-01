using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Protobot.UI {
    /// <summary>
    /// UI that displays whenever the user attempts to exit or load with unsaved changes on the current build
    /// </summary>
    public class UnsavedChangesUI : MonoBehaviour {
        [SerializeField] private TMP_Text warningText;
        
        [SerializeField] private Button saveButton, discardButton;
        private string UnsavedChangesText => "This build has unsaved changes. ";
        private string NotSavedYetText => "This build has not been saved yet.";
        

        public Action OnPressSave;
        public Action OnPressDiscard;

        private void OnEnable() {
            saveButton.onClick.AddListener(() => {
                OnPressSave?.Invoke();
                gameObject.SetActive(false);
            });
            discardButton.onClick.AddListener(() => {
                OnPressDiscard?.Invoke();
                gameObject.SetActive(false);
            });
        }
        
        private void OnDisable() {
            saveButton.onClick.RemoveAllListeners();
            discardButton.onClick.RemoveAllListeners();
        }

        public void Enable(bool noFilePath) {
            warningText.text = noFilePath ? NotSavedYetText : UnsavedChangesText;
            gameObject.SetActive(true);
        }
    }
}