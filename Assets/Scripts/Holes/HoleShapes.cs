using UnityEngine;

namespace Protobot {
    [CreateAssetMenu(fileName = "New Hole Shapes")]
    public class HoleShapes : ScriptableObject {
        //Singleton set up
        public static HoleShapes instance {get; private set;}
        public HoleShapes() { 
            instance = this;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Init() {
            instance = (HoleShapes)Resources.LoadAll("General Data", typeof(HoleShapes))[0];
        }

        //actual hole shape data
        public HoleShape[] shapeList;

        public Mesh GetShapeMesh(string name) {
            foreach (HoleShape shape in shapeList) {
                if (name.ToLower().Contains(shape.shapeName.ToLower()))
                    return shape.shapeMesh;
            }

            return null;
        }

        public string GetShapeName(Mesh mesh) {
            foreach (HoleShape shape in shapeList) {
                if (shape.shapeMesh == mesh)
                    return shape.shapeName;
            }

            return null;
        }
    }

    [System.Serializable]
    public class HoleShape {
        public string shapeName;
        public Mesh shapeMesh;
    }
}