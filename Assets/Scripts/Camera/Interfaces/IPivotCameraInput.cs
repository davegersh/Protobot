using UnityEngine;
using System;

namespace Protobot {
    public interface IPivotCameraInput {
        event Action<Vector2> updateOrbit;
        event Action<float> updateZoom;
        event Action<Vector2> updatePan;
    }
}