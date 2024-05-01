using UnityEngine;
using UnityEngine.Events;
using System;

namespace Protobot.SelectionSystem {
    public abstract class Selector : MonoBehaviour {
        public abstract event Action<ISelection> setEvent;
        public abstract event Action clearEvent;
        public SelectionResponse[] setResponses;

        private void Start() {
            setEvent += RunSetResponses;
        }

        public void RunSetResponses(ISelection selection) {
            foreach (SelectionResponse response in setResponses)
                response.OnSet(selection);
        }
    }
}