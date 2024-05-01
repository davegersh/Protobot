using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot.Tools {
    public class TagToolRequirement : MonoBehaviour, IToolRequirement {
        [SerializeField] private ObjectLink objectLink;
        [SerializeField] private List<string> requiredTags;
        public bool isValid => objectLink.active && (requiredTags.Contains(objectLink.obj.tag) || requiredTags.Count == 0);
    }
}