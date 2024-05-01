using System;
using UnityEngine;
using Protobot.InputEvents;

namespace Protobot.SelectionSystem {
    public class HoleFaceSelectionResponse : SelectionResponse {
        public override bool RespondOnlyToSelectors => false;

        [SerializeField] private HoleFace holeFace = null;
        [SerializeField] private MouseCast mouseCast = null;
        [SerializeField] private InputEvent flipInput;

        private bool flipFaceDir => flipInput.IsPressed;

        public override void OnSet(ISelection holeColliderSel) {
            if (holeColliderSel.gameObject.TryGetComponent(out HoleCollider holeCollider)) {
                HoleData selHole = holeCollider.holeData;

                holeFace.gameObject.SetActive(true);

                Vector3 faceDir = mouseCast.hit.normal;

                if (flipFaceDir)
                    faceDir = -faceDir;

                holeFace.Set(selHole, faceDir);
            }
        }

        public override void OnClear(ClearInfo info) {
            holeFace.gameObject.SetActive(false);
        }
    }
}