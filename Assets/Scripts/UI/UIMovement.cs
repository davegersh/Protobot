using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot.UI {
    public class UIMovement : MonoBehaviour {
        [Header("Position")]
        public bool atInitPos;
        public bool positioning;
        public bool atActivePos;
        public Vector2 activePos;
        public bool atInactivePos;
        public Vector2 inactivePos;
        public Vector2 targetPos {get; private set;}
        public Vector2 initPos {get; private set;}

        [Header("Rotation")]
        public bool atInitRot;
        public bool rotating;
        public float activeRot;
        public float inactiveRot;

        [SerializeField]
        public float targetRot {get; private set;}

        [SerializeField]
        public float initRot {get; private set;}

        private RectTransform rTransform;

        void Awake() {
            rTransform = GetComponent<RectTransform>();
            atInitPos = true;
            atInitRot = true;
            initPos = rTransform.anchoredPosition;
            initRot = rTransform.eulerAngles.z;
            targetPos = initPos;
            targetRot = initRot;
        }

        void Update() {
            if (positioning) {
                rTransform.anchoredPosition = Vector3.Lerp(rTransform.anchoredPosition, targetPos, Time.deltaTime * 10);
                if (Vector3.Distance(rTransform.anchoredPosition, targetPos) < 2) {
                    rTransform.anchoredPosition = targetPos;
                    positioning = false;
                }
            }

            if (rotating) {
                rTransform.rotation = Quaternion.Lerp(rTransform.rotation, Quaternion.Euler(rTransform.eulerAngles.x, rTransform.eulerAngles.y, targetRot), Time.deltaTime * 10);
                if (Mathf.Abs(rTransform.eulerAngles.z - targetRot) > 2) {
                    rTransform.rotation = Quaternion.Euler(rTransform.eulerAngles.x, rTransform.eulerAngles.y, targetRot);
                    rotating = false;
                }
            }

            atInitPos = (targetPos == initPos);
            atInitRot = (targetRot == initRot);

            atActivePos = (targetPos == activePos);
            atInactivePos = (targetPos == inactivePos);
        }

        //POSITION FUNCTIONS
        public void SetXPos(float newX) {
            positioning = true;
            targetPos = new Vector2(newX, targetPos.y);
        }
        public void SetYPos(float newY) {
            positioning = true;
            targetPos = new Vector2(targetPos.x, newY);
        }

        public void ResetPos() {
            positioning = true;
            targetPos = initPos;
        }

        public void SetActivePos() {
            positioning = true;
            targetPos = activePos;
        }

        public void SetInactivePos() {
            positioning = true;
            targetPos = inactivePos;
        }

        /// <Summary>
        /// Sets a new initial position and moves to it
        /// </Summary>
        public void SetInitPos(Vector2 newInitPos) {
            initPos = newInitPos;
            ResetPos();
        }

        //ROTATION FUNCTIONS
        public void SetRot(float rot) {
            rotating = true;
            targetRot = rot;
        }
        public void ResetRot() {
            rotating = true;
            targetRot = initRot;
        }

        public void SetActiveRot() {
            rotating = true;
            targetRot = activeRot;
        }

        public void SetInactiveRot() {
            rotating = true;
            targetRot = inactiveRot;
        }

        public void ToggleActivePos() {
            if (atActivePos) {
                SetInactivePos();
            }
            else {
                SetActivePos();
            }
        }

        public void ToggleActiveRot() {
            if (rTransform.eulerAngles.z == activeRot) {
                SetInactiveRot();
            }
            else {
                SetActiveRot();
            }
        }
    }
}