using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Protobot.Builds {
    public interface IBuildsListSource {
        event Action<List<BuildData>> OnGetData;
        void GetData();
    }
}