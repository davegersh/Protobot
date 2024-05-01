using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Protobot {
    [CustomEditor(typeof(Setting)), CanEditMultipleObjects]
    public class SettingEditor : Editor {
        public SerializedProperty currentValueProp;

        public SerializedProperty updateIntEventProp;
        public SerializedProperty updateFloatEventProp;
        public SerializedProperty updateBoolEventProp;

        public SerializedProperty initIntEventProp;
        public SerializedProperty initFloatEventProp;
        public SerializedProperty initBoolEventProp;

        private void OnEnable() {
            // Setup SerializedProperties
            currentValueProp = serializedObject.FindProperty("currentValue");

            updateIntEventProp = serializedObject.FindProperty("OnUpdateIntValue");
            updateFloatEventProp = serializedObject.FindProperty("OnUpdateFloatValue");
            updateBoolEventProp = serializedObject.FindProperty("OnUpdateBoolValue");

            initIntEventProp = serializedObject.FindProperty("OnInitIntValue");
            initFloatEventProp = serializedObject.FindProperty("OnInitFloatValue");
            initBoolEventProp = serializedObject.FindProperty("OnInitBoolValue");
        }

        public override void OnInspectorGUI() {
            serializedObject.Update();

            Setting setting = (Setting)target;

            GUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(currentValueProp);

            if (GUILayout.Button("Delete Saved Value")) {
                PlayerPrefs.DeleteKey(setting.name);
            }

            GUILayout.EndHorizontal();

            var valueType = setting.RealValue.GetType();

            if (valueType == typeof(int)) {
                EditorGUILayout.PropertyField(updateIntEventProp);
                EditorGUILayout.PropertyField(initIntEventProp);
            }
            else if (valueType == typeof(float)) {
                EditorGUILayout.PropertyField(updateFloatEventProp);
                EditorGUILayout.PropertyField(initFloatEventProp);
            }
            else if (valueType == typeof(bool)) {
                EditorGUILayout.PropertyField(updateBoolEventProp);
                EditorGUILayout.PropertyField(initBoolEventProp);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
