using UnityEngine;
using UnityEngine.Events;
using System;

namespace Protobot {
    [Serializable]
    public class GameObjectUnityEvent : UnityEvent<GameObject> { }

    [Serializable]
    public class StringUnityEvent : UnityEvent<string> { }

    [Serializable]
    public class IntEvent : UnityEvent<int> { }

    [Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    [Serializable]
    public class FloatEvent : UnityEvent<float> { }

}