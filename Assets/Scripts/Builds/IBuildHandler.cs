using UnityEngine;
using System.Collections;

using System;
namespace Protobot.Builds {
    public interface IBuildHandler {
        void Save(BuildData buildData);
        void Delete(BuildData buildData);
        DateTime GetExactWriteTime(BuildData buildData);
    }
}