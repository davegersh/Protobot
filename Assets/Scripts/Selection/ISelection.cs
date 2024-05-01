using UnityEngine;
using System;
using System.Collections.Generic;

namespace Protobot.SelectionSystem {
    public interface ISelection {
        GameObject gameObject { get; set; }
        Selector selector { get; set; }

        void Select();
        void Deselect();
    }

    public class ObjectSelection : ISelection {
        public GameObject gameObject { get; set; }
        public Selector selector { get; set; }
        public void Select() { }
        public void Deselect() { }
    }

    public class MultiSelection : ISelection {
        public GameObject gameObject { get; set; }
        public Selector selector { get; set; }

        public Pivot pivot;

        public List<GameObject> objs = new List<GameObject>();

        public MultiSelection(Selector selector, List<GameObject> objs) {
            this.objs = objs;
            CreatePivot();
            this.selector = selector;
        }

        public void CreatePivot() {
            pivot = Pivot.Create("MultiPivot");
            gameObject = pivot.gameObject;
        }

        public void Select() {
            pivot.AddObjects(objs);
            pivot.Center();
        }

        public void Deselect() {
            if (gameObject.activeInHierarchy)
                pivot.DetachObjects();
        }
    }

    public class HoleSelection : ISelection {
        public GameObject gameObject { get; set; }
        public Selector selector { get; set; }

        public HoleFace holeFace;
        public HoleData holeData;
        public Vector3 faceDirection;
        public void Select() {
            holeFace.Set(holeData, faceDirection);
            holeFace.gameObject.SetActive(true);
        }

        public void Deselect() {
            holeFace.gameObject.SetActive(false);
        }
    }
}