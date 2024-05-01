using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Protobot.StateSystems {
    public class ObjectElement : IElement {
        public GameObject obj;
        public bool existing;
        public Vector3 position;
        public Quaternion rotation;

        public ObjectElement(GameObject newObj) {
            obj = newObj;
            existing = newObj.layer != LayerMask.NameToLayer("Deleted");
            position = newObj.transform.position;
            rotation = newObj.transform.rotation;
        }
        public void Load() {
            ApplyExistence(existing);

            if (obj.transform.position != position)
                obj.transform.DOMove(position, 0.25f);

            if (obj.transform.rotation != rotation)
                obj.transform.DORotateQuaternion(rotation, 0.25f);
        }

        public static void SetExistence(GameObject gameObject, bool fromExistence, bool toExistence) {
            ObjectElement currentElement = new ObjectElement(gameObject);
            currentElement.ApplyExistence(fromExistence);
            StateSystem.AddElement(currentElement);

            currentElement.ApplyExistence(toExistence);

            StateSystem.AddState(new ObjectElement(gameObject));
        }

        public static void SetExistence(Pivot pivot, bool fromExistence, bool toExistence) {
            var savedObjs = pivot.objects.Where(x => x.GetComponent<SavedObject>() != null).ToList();

            foreach (GameObject obj in savedObjs) {
                ObjectElement currentElement = new ObjectElement(obj);
                currentElement.ApplyExistence(fromExistence);
                StateSystem.AddElement(currentElement);
            }

            SetExistence(pivot.gameObject, fromExistence, toExistence);

            foreach (GameObject obj in savedObjs) {
                ObjectElement currentElement = new ObjectElement(obj);
                currentElement.ApplyExistence(toExistence);
                StateSystem.AddElement(currentElement);
            }
        }


        /// <summary>
        /// Creates object elements from list of gameObjects and adds them to the CURRENT state
        /// </summary>
        /// <param name="gameObjects"></param>
        public static void AddObjectElements(List<GameObject> gameObjects) {
            foreach (GameObject gameobject in gameObjects) {
                StateSystem.AddElement(new ObjectElement(gameobject));
            }
        }

        /// <summary>
        /// Applies a given value for existence on the object (does not change state value)
        /// </summary>
        /// <param name="value"></param>
        public void ApplyExistence(bool value) {
            obj.SetActive(value);
            obj.layer = value ? 0 : LayerMask.NameToLayer("Deleted");
        }

    }
}