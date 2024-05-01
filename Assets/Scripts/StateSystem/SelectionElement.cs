using DG.Tweening;
using UnityEngine;
using Protobot.SelectionSystem;

namespace Protobot.StateSystems {
    public class SelectionElement : IElement {
        public SelectionManager manager;
        public ISelection selection;

        public SelectionElement(SelectionManager newMan, ISelection newSel) {
            manager = newMan;
            selection = newSel;
        }

        public void Load() {
            if (selection == null) {
                manager.ClearCurrent(false, true);
            }
            else {
                selection.selector = null;
                manager.SetCurrent(selection);
            }
        }
    }
}