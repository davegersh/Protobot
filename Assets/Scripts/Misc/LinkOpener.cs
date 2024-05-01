using UnityEngine;
using System.Collections;

namespace Protobot {
    [CreateAssetMenu(fileName = "New Link Opener")]
    public class LinkOpener : ScriptableObject {
        [SerializeField] private string link;
        
        public void OpenLink() {
            Application.OpenURL(link);
        }
    }
}