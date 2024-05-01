using UnityEngine;

namespace Protobot {
    public class RuntimeEvents : MonoBehaviour {
        public delegate void RuntimeEventHandler();
        public static event RuntimeEventHandler OnAwake;
        public static event RuntimeEventHandler OnUpdate;
        public static event RuntimeEventHandler OnStart;

        void Awake() => OnAwake?.Invoke();

        void Start() => OnStart?.Invoke();

        void Update() => OnUpdate?.Invoke();
    }
}