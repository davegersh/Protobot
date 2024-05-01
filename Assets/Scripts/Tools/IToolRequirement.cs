using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Protobot.Tools {
    public interface IToolRequirement {
        bool isValid { get; }
    }
}