using System.Collections;
using System.Collections.Generic;
using Protobot;
using UnityEngine;

namespace Protobot {
    public class AluminumPartGenerator : PartGenerator {
        [SerializeField] private List<string> param1Options;
        [SerializeField] private List<AluminumSubParts> subParts;
        
        private int HoleCount {
            get {
                if (int.TryParse(param2.value, out var val))
                    return val;
                
                return int.Parse(param2.customDefault);
            }
        }

        public override List<string> GetParam1Options() => param1Options;
        public override List<string> GetParam2Options() => new List<string>{" "};

        public override Mesh GetMesh() => subParts[param1Options.IndexOf(param1.value)].GetMesh(HoleCount);

        public override GameObject Generate(Vector3 position, Quaternion rotation) {
            var partObj = subParts[param1Options.IndexOf(param1.value)].GeneratePart(HoleCount);
            partObj.transform.position = position;
            partObj.transform.rotation = rotation;

            RemoveDataScripts(partObj);
            SetId(partObj);
            
            return partObj;
        }
    }
}
