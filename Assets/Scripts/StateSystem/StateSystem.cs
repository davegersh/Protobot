using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Protobot.InputEvents;
using System;

namespace Protobot.StateSystems {
    public class StateSystem : MonoBehaviour {
        public static List<State> states = new List<State>();
        public static State curState => states[curIndex];
        public static int lastStateIndex => states.Count - 1;

        [SerializeField] private InputEvent undoInput;
        [SerializeField] private InputEvent redoInput;
        public static int curIndex;

        public static Action OnChangeState; //Runs when a state is added or loaded

        public static StateSystem instance;

        private void Awake() {
            instance = this;

            AddEmptyState();

            undoInput.performed += () => Undo();
            redoInput.performed += () => Redo();

            OnChangeState += () => Debug.Log("State has changed!");
        }

        public void ResetStates() {
            states.Clear();
            AddEmptyState();
        }

        public static IEnumerator WaitOnChangeState() {
            yield return new WaitForSeconds(0.25f);
            OnChangeState?.Invoke();
        }

        public static void InvokeOnChangeState() => instance.StartCoroutine(WaitOnChangeState());

        /// <summary>
        /// Adds an element to the current state of the scene
        /// </summary>
        /// <param name="newElement"></param>
        public static void AddElement(IElement newElement) {
            curState.elements.Add(newElement);
        }

        /// <summary>
        /// Adds a list of elements to the current state of the scene
        /// </summary>
        /// <param name="newElement"></param>
        public static void AddElements(List<IElement> newElements) {
            curState.elements.AddRange(newElements);
        }

        /// <summary>
        /// Adds and advances the current state to a new one
        /// </summary>
        /// <param name="newState"></param>
        public static void AddState(State newState) {
            if (curIndex < lastStateIndex)
                 states = states.GetRange(0, curIndex+1);

            states.Add(newState);

            curIndex = lastStateIndex;
            InvokeOnChangeState();
        }


        /// <summary>
        /// Adds and advances the current state with a given element
        /// </summary>
        /// <param name="singleElement"></param>
        public static void AddState(IElement singleElement) {
            List<IElement> newElements = new List<IElement>();
            newElements.Add(singleElement);

            AddState(new State(newElements));
        }

        /// <summary>
        /// Adds and advances the current state with no new elements
        /// </summary>
        public static void AddEmptyState() {
            AddState(new State(new List<IElement>()));
        }
        
        /// <summary>
        /// Changes the current state to the previous one
        /// </summary>
        public void Undo() {
            if (curIndex - 1 >= 0) {
                curIndex--;
                states[curIndex].Load();
                InvokeOnChangeState();
            }
        }

        /// <summary>
        /// Changes the current state to the next one if not at the last created state
        /// </summary>
        public void Redo() {
            if (curIndex + 1 <= lastStateIndex) {
                curIndex++;
                states[curIndex].Load();
                InvokeOnChangeState();
            }
        }
    }
}