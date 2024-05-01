using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Protobot {
    public abstract class ConnectingPart : MonoBehaviour {
        [SerializeField] public HoleDetector holeDetector;

        private List<GameObject> connectedParts = new();
        
        public abstract bool CanFormConnection { get; }

        public abstract HoleDetector CreateHoleDetector();

        public abstract List<GameObject> GetAttachedParts();
        
        public void Awake() {
            if (holeDetector == null)
                holeDetector = CreateHoleDetector();
        }

        public void FormConnection() {
            var attachedParts = GetAttachedParts();

            if (connectedParts.Count > attachedParts.Count) {
                var diff = connectedParts.Except(attachedParts);
                foreach (var obj in diff) {
                    GroupManager.BreakConnection(this, obj);
                }
            }

            GroupManager.FormConnection(this, attachedParts);
            connectedParts = attachedParts;
        }

        public void BreakConnection() => GroupManager.Remove(gameObject);

        private void AddConnectionEvents() {
            holeDetector.OnAddTargetHole += () => {
                if (CanFormConnection)
                    FormConnection();
            };
            holeDetector.OnRemoveTargetHole += () => {
                if (!CanFormConnection)
                    BreakConnection();
                else
                    FormConnection();
            };
        }
        
        public void OnEnable() {
            AddConnectionEvents();
        }

        public void OnDisable() {
            holeDetector.ClearEvents();
        }

        public void OnDestroy() {
            holeDetector.ClearEvents();
        }
    }
}