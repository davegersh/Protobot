using System;
using System.Collections.Generic;
using UnityEngine;
using Protobot.InputEvents;
using System.Linq;

namespace Protobot.SelectionSystem {
    public class MultiSelector : SelectionResponse, IResponseSelector {
        public override bool RespondOnlyToSelectors => false;

        private HashSet<GameObject> objs = new();
        [SerializeField] private string ignoredTags;
        [SerializeField] private InputEvent mutliSelectInput;

        private GameObject lastSelectedObj;

        public override void OnSet(ISelection selection) {
            if (IsObjectSelection(selection))
                lastSelectedObj = selection.gameObject;
        }

        private bool IsObjectSelection(ISelection sel) {
            return (sel as ObjectSelection != null);
        }

        public ISelection GetResponseSelection(ISelection incomingSelection) {

            if (IsObjectSelection(incomingSelection)) {
                if (mutliSelectInput.IsPressed && lastSelectedObj != null) {
                    incomingSelection.gameObject.GetConnectedObjects(true).ForEach(x => objs.Add(x));
                    lastSelectedObj.GetConnectedObjects(true).ForEach(x => objs.Add(x));

                    return new MultiSelection(incomingSelection.selector, objs.ToList());
                }
            }

            return null;
        }

        public override void OnClear(ClearInfo info) {
            if (!mutliSelectInput.IsPressed) {
                lastSelectedObj = null;
                objs.Clear();
            }
        }
    }
}