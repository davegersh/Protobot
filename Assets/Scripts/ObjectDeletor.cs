using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protobot.StateSystems;

namespace Protobot {
    public class ObjectDeletor : ObjectLinkAction {
        private void DeleteObject() {
            if (!refObj.active) return;

            var obj = refObj.obj;

            if (obj.TryGetComponent(out HoleFace holeFace)) {
                obj = holeFace.hole.part;
            }

            Pivot pivot = obj.GetComponent<Pivot>();

            if (pivot != null && pivot.tag != "Group")
                ObjectElement.SetExistence(pivot, true, false);
            else
                ObjectElement.SetExistence(obj, true, false);
        }

        public override void Execute() {
            DeleteObject();
            OnExecute?.Invoke();
        }
    }
}