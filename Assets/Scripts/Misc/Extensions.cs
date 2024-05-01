using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Rendering;

namespace Protobot {
    public static class Extensions {

        //VECTOR3 EXTENSIONS

        /// <summary>
        /// Returns the given Vector3 rounded to the given increments
        /// </summary>
        public static Vector3 Round(this Vector3 v, float inc) {
            v.x = Mathf.Round(v.x / inc) * inc;
            v.y = Mathf.Round(v.y / inc) * inc;
            v.z = Mathf.Round(v.z / inc) * inc;
            return v;
        }

        /// <summary>
        /// Returns the given Vector3 with the absolute value of each axis
        /// </summary>
        public static Vector3 Abs(this Vector3 v) {
            v.x = Mathf.Abs(v.x);
            v.y = Mathf.Abs(v.y);
            v.z = Mathf.Abs(v.z);
            return v;
        }

        //MESH EXTENSIONS

        /// <summary>
        /// Returns a newly created mesh with all the combined submeshes of the given mesh
        /// </summary>
        public static Mesh CombineSubmeshes(this Mesh refMesh) {
            Mesh mesh = new Mesh() {
                indexFormat = IndexFormat.UInt32,
                vertices = refMesh.vertices,
                triangles = refMesh.triangles,
                tangents = refMesh.tangents,
                normals = refMesh.normals,
            };
        
            mesh.SetTriangles(mesh.triangles, 0);
            mesh.subMeshCount = 1;

            return mesh;
        }

        //INPUT SYSTEM
        /// <summary>
        /// Returns whether all the controls in an input action are pressed
        /// </summary>
        public static bool AllControlsPressed(this InputAction action) {
            if (action.IsPressed()) {
                for (int i = 0; i < action.controls.Count; i++) {
                    if (!action.controls[i].IsPressed() && action.controls[i].path != "")
                        return false;
                }

                return true;
            }

            return false;
        }

        //STRINGS
        /// <summary>
        /// Returns a version of valid string with all invalid characters of a filename removed
        /// </summary>
        public static string ToValidFileName(this string str) {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars();
            return new string(str.Where(m => !invalidChars.Contains(m)).ToArray<char>());
        }

        // GAMEOBJECTs

        /// <summary>
        /// Returns whether or not the given obj is within a group
        /// Also returns the transform of the Group if applicable
        /// (Does not include a GetComponent call)
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="groupTransform"></param>
        /// <returns></returns>
        public static bool TryGetGroup(this GameObject obj, out Transform groupTransform) {
            Transform parent = obj.transform.parent;

            if (parent != null && parent.CompareTag("Group")) {
                groupTransform = parent;
                return true;
            }

            groupTransform = null;
            return false;
        }

        public static bool IsDeleted(this GameObject obj) {
            int deletedLayer = LayerMask.NameToLayer("Deleted");

            if (obj.layer == deletedLayer) return true;
            if (obj.transform.root.gameObject.layer == deletedLayer) return true;

            return false;
        }

        public static Quaternion GetAlignmentRotation(this GameObject obj, bool flipDirection = false) {
            return GetAlignmentRotation(obj.transform, flipDirection);
        }
        
        public static Quaternion GetAlignmentRotation(this Transform tform, bool flipDirection = false) {
            int flip = flipDirection ? -1 : 1;
            return Quaternion.LookRotation(tform.forward * flip, tform.up);
        }
    }
}
