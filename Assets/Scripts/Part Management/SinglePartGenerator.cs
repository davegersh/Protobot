using System.Collections.Generic;
using UnityEngine;

namespace Protobot {
    public class SinglePartGenerator : PartGenerator {
        [SerializeField] private GameObject singleObj;
        
        public override List<string> GetParam1Options() => null;

        public override List<string> GetParam2Options() => null;

        public override Mesh GetMesh() => singleObj.GetComponent<MeshFilter>().sharedMesh;

        public override GameObject Generate(Vector3 position, Quaternion rotation) {
            GameObject newObj = Instantiate(singleObj, position, rotation);
            RemoveDataScripts(newObj);
            SetId(newObj);
            
            return newObj;
        }

        public override bool TryGetPartData(out PartData partData) {
            partData = singleObj.GetComponent<PartData>();
            return true;
        }
    }
}