using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;
using Protobot.Transformations;

namespace Protobot {
    public class Pivot : MonoBehaviour {
        public List<GameObject> objects = new List<GameObject>();

        public bool IsEmpty => objects.Count == 0;

        /// <summary> De-parents all children but preserves 'objects' list </summary>
        public void DetachObjects() {
            for (int i = 0; i < objects.Count; i++) {
                if (objects[i] != null) {
                    objects[i].transform.parent = null;
                }
            }
        }

        /// <summary> Parents all objects </summary>
        public void AttachObjects() {
            for (int i = 0; i < objects.Count; i++)
                objects[i].transform.SetParent(transform);
        }

        /// <summary> Runs <see cref="DetachObjects"></see> and clears 'objects' list </summary>
        public void SetEmpty() {
            DetachObjects();
            objects = new List<GameObject>();
        }

        /// <summary> Replace and detach the current list of objects and add a given one </summary>
        public void SetObjects(List<GameObject> newObjs) {
            DetachObjects();
            objects = new List<GameObject>();
            AddObjects(newObjs);
        }

        /// <summary> Add a given object to the object list and parent it </summary>
        public void AddObject(GameObject obj, bool checkGroups = true) {
            if (checkGroups && obj.TryGetGroup(out Transform groupTransform)) {
                obj = groupTransform.gameObject;
            }

            if (objects.Contains(obj))
                return;

            objects.Add(obj);
            obj.transform.SetParent(transform);
        }

        /// <summary> Add a given list of objects to the object list and parent them </summary>
        public void AddObjects(List<GameObject> objs, bool checkGroups = true) {
            for (int i = 0; i < objs.Count; i++) {
                AddObject(objs[i], checkGroups);
            }
        }

        /// <summary> Destroys all children while keeping pivot gameobject </summary>
        public void DestroyObjects() {
            for (int i =0; i < objects.Count; i++)
                Destroy(objects[i]);
            objects = new List<GameObject>();
        }

        /// <summary> Destroys only the pivot gameobject (detaches all children) </summary>
        public void Destroy() {
            DetachObjects();
            Destroy(gameObject);
        }

        // POSITIONING
        /// <summary>
        /// Sets transform position of pivot to newPos
        /// If moveAttached = true, then the pivot simply moves  all attached objects
        /// If moveattached = false, then ONLY the pivot moves while all attached objects remain stationary
        /// </summary>
        public void SetPosition(Vector3 newPos, bool moveAttached) {
            if (!moveAttached) {
                DetachObjects();
                transform.position = newPos;
                AttachObjects();
            } 
            else
                transform.position = newPos;
        }

        /// <summary> Executes <see cref="SetPosition"></see> using <see cref="GetCenter"></see> </summary>
        public void Center() {
            SetPosition(GetCenter(), false);
        }

        /// <summary> Returns world position of the average center of all object mesh bounds </summary>
        public Vector3 GetCenter() {
            if (objects.Count > 0) {
                List<Bounds> objectBounds = new List<Bounds>();

                for (int p = 0; p < objects.Count; p++) {
                    if (objects[p].transform.childCount > 0)
                        objectBounds.AddRange(objects[p].transform.GetComponentsInChildren<MeshRenderer>().Select(m => m.bounds));
                    else {
                        MeshRenderer rend = objects[p].GetComponent<MeshRenderer>();
                        if (rend != null)
                            objectBounds.Add(rend.bounds);
                    }
                }

                float xMax = objectBounds.Select(b => b.max.x).Max();
                float xMin = objectBounds.Select(b => b.min.x).Min();

                float yMax = objectBounds.Select(b => b.max.y).Max();
                float yMin = objectBounds.Select(b => b.min.y).Min();

                float zMax = objectBounds.Select(b => b.max.z).Max();
                float zMin = objectBounds.Select(b => b.min.z).Min();

                Vector3 centerPoint = new Vector3((xMax + xMin) / 2, (yMax + yMin) / 2, (zMax + zMin) / 2);

                return centerPoint;
            }
            return Vector3.zero;
        }

        /// <summary>
        /// Creates an empty pivot with a given name
        /// </summary>
        /// <param name = "name"> The name of the pivot gameobject </param>
        public static Pivot Create(string name) {
            Pivot newPivot = new GameObject(name, typeof(Pivot)).GetComponent<Pivot>();
            newPivot.tag = "Pivot";
            return newPivot;
        }

        /// <summary>
        /// Creates a pivot with a given name, list of objects, and starting position     
        /// </summary>
        /// <param name = "name"> The name of the pivot gameobject </param>
        /// <param name = "objs"> The list of objects that will become children of the pivot </param>
        /// <param name = "startPos"> The initial position of the pivot </param>
        public static Pivot Create(string name, List<GameObject> objs, Vector3 startPos) {
            Pivot newPivot = Create(name);
            newPivot.transform.position = startPos;
            newPivot.SetObjects(objs);
            return newPivot;
        }

        /// <summary>
        /// Creates a pivot with a list of objects and a position at the center of all object bounds
        /// </summary>

        /// <param name = "name"> The name of the pivot gameobject </param>
        /// <param name = "objs"> The list of objects that will become children of the pivot </param>
        public static Pivot Create(string name, List<GameObject> objs) {
            Pivot newPivot = Create(name, objs, Vector3.zero);
            newPivot.Center();
            return newPivot;
        }

        /// <summary>
        /// Creates a temporary "moving" pivot with a list of objects, a start and final position.
        /// Once the pivot reaches the final position, it automatically destroys itself (not including the objects inside)
        /// </summary>

        /// <param name = "objs"> The list of objects that will become children of the pivot </param>
        /// <param name = "startPos"> The starting position of the pivot </param>
        /// <param name = "finalPos"> The final position of the pivot </param>
        public static Pivot CreateMoving(List<GameObject> objs, Displacement startDisplacement, Displacement finalDisplacement) {
            Pivot newPivot = new GameObject("Moving Pivot", typeof(Pivot)).GetComponent<Pivot>();
            newPivot.tag = "Pivot";

            newPivot.transform.position = startDisplacement.translation.Position;
            newPivot.transform.forward = startDisplacement.rotation.Forward;
            newPivot.SetObjects(objs);

            Tweener moveTween = newPivot.transform.DOMove(finalDisplacement.translation.Position, 0.25f);
            newPivot.transform.forward = finalDisplacement.rotation.Forward;

            moveTween.OnComplete(() => newPivot.Destroy());

            return newPivot;
        }
    }
}