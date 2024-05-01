using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Text.RegularExpressions;

namespace Protobot {
    public static class HoleGenerator {
        /// <Summary> Generates holes on the given GameObject with given data about the holes </Summary>
        public static void CreateHoles(GameObject obj, string holeDataString) {
            if (holeDataString == "") return;

            string[] dataLines = holeDataString.Split('\n');

            string shape = "";
            float size = 0;
            float depth = 0;
            float spacing = 0;
            HoleCollider.HoleType holeType = HoleCollider.HoleType.Normal;
            Vector3 axis = Vector3.zero;

            void LogError(string logMessage) {
                Debug.LogError("HoleGenerator terminated - " + logMessage);
            }

            for (int i = 0; i < dataLines.Length; i++) {
                string line = dataLines[i].Replace(" ", ""); //remove whitespace in between chars
                //Debug.Log("[" + i + "] " + line);
                string[] comSplit = line.Split('='); //every command has an "=" so we split them by command and input

                if (comSplit.Length != 2) continue;

                string command = comSplit[0];
                string input = comSplit[1];

                if (command == "shape") {
                    shape = input;
                }
                else if (command == "size") {
                    if (float.TryParse(input, out float floatSize)) {
                        size = floatSize;

                        if (spacing == 0)
                            spacing = size;
                    }
                    else {
                        LogError("Size float parse failed");
                        return;
                    }
                }
                else if (command == "depth") {
                    if (float.TryParse(input, out float floatDepth))
                        depth = floatDepth;
                    else {
                        LogError("Depth float parse failed");
                        return;
                    }
                }
                else if (command == "spacing") {
                    if (float.TryParse(input, out float floatSpacing))
                        spacing = floatSpacing;
                    else {
                        LogError("Spacing float parse failed");
                        return;
                    }
                }
                else if (command == "axis") {
                    if (input.Contains("x")) axis = Vector3.right;
                    else if (input.Contains("y")) axis = Vector3.up;
                    else if (input.Contains("z")) axis = Vector3.forward;
                }
                else if (command == "type") {
                    input = input.ToLower();
                    if (input == "threaded")
                        holeType = HoleCollider.HoleType.Threaded;
                    if (input == "clamp")
                        holeType = HoleCollider.HoleType.Clamp;
                    if (input == "normal")
                        holeType = HoleCollider.HoleType.Normal;
                }
                else if (command == "hole") {
                    foreach (Vector3 v in CreateVectors(input, spacing)) {
                        Mesh colMesh = HoleShapes.instance.GetShapeMesh(shape);

                        if (colMesh == null) {
                            LogError("Input shape does not exist");
                            return;
                        }

                        GameObject newHoleObj = new GameObject("HoleCollider", typeof(MeshCollider));
                        newHoleObj.tag = "HoleCollider";
                        newHoleObj.layer = HoleCollider.HOLE_COLLISIONS_LAYER;

                        newHoleObj.GetComponent<MeshCollider>().sharedMesh = colMesh;

                        newHoleObj.transform.position = v;
                        newHoleObj.transform.localScale = new Vector3(size, size, depth);
                        newHoleObj.transform.forward = axis;
                        newHoleObj.transform.SetParent(obj.transform);

                        HoleCollider holeComponent = newHoleObj.AddComponent<HoleCollider>();
                        holeComponent.holeType = holeType;
                    }
                }
            }
        }

        public static List<Vector3> CreateVectors(string vectorData, float spacing) {
            //Get rid of the parenthesis if they exist anywhere
            vectorData = vectorData.Replace("(", "");
            vectorData = vectorData.Replace(")", "");

            //split given vector data into axis
            string[] vectorAxis = vectorData.Split(',');

            //create position lists that will be iterated through later
            List<float> xPositions = new List<float>();
            List<float> yPositions = new List<float>();
            List<float> zPositions = new List<float>();
            
            //populate position lists based on vector range feature
            for (int i = 0; i < vectorAxis.Length; i++) {
                List<float> positions = new List<float>();
                string[] rangeSplit = vectorAxis[i].Split('>');

                //if there's two parts that means we have a range in place
                if (rangeSplit.Length == 2) {
                    float min = float.Parse(rangeSplit[0]);
                    float max = float.Parse(rangeSplit[1]);
                    for (float r = min; r <= max; r += spacing) 
                        positions.Add(r);
                }
                else if (rangeSplit.Length == 1) {
                    positions.Add(float.Parse(rangeSplit[0]));
                }
                if (i == 0) xPositions.AddRange(positions);
                if (i == 1) yPositions.AddRange(positions);
                if (i == 2) zPositions.AddRange(positions);
            }

            //create the vectors
            List<Vector3> vectors = new List<Vector3>();

            //populate the vectors list
            foreach (float x in xPositions) {
                foreach (float y in yPositions) {
                    foreach (float z in zPositions) {
                        vectors.Add(new Vector3(x,y,z));
                    }
                }
            }
            return vectors;
        }
    }
}