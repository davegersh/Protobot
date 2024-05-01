using UnityEngine;

namespace Protobot.SelectionSystem {
    public class TagSelectionCondition : SelectionCondition {
        public string targetTags;
        public override bool GetValue(ISelection selection) {
            if (targetTags.Contains(selection.gameObject.tag)) {
                return true;
            }

            SavedObject savedObject = selection.gameObject.GetComponent<SavedObject>();
            if (savedObject != null && targetTags.Contains(savedObject.nameId)) {
                return true;
            }
            
            return false;
        }

        public void SetTargetTags(string newTargetTags) => targetTags = newTargetTags;
    }
}