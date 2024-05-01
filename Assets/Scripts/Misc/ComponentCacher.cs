using System;
using System.Reflection;
using UnityEngine;
using System.Linq;

namespace Protobot {
    public static class ComponentCacher {
        [RuntimeInitializeOnLoadMethod]
        public static void CacheComponents() {
            var monoBehaviors = Resources.FindObjectsOfTypeAll<MonoBehaviour>();

            int componentsCached = 0;
            int getComponentCalls = 0;

            foreach (MonoBehaviour m in monoBehaviors) {
                Type type = m.GetType();

                var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

                var attFields = fields.Where(f => f.GetCustomAttributes(typeof(CacheComponentAttribute), true).Length != 0).ToArray();

                foreach (FieldInfo f in attFields) {
                    GameObject gameObject = m.gameObject;

                    Component[] components = gameObject.GetComponents<Component>();
                    getComponentCalls += components.Length;

                    bool componentFound = false;

                    foreach (Component c in components) {
                        Type cType = c.GetType();
                        Type fType = f.FieldType;

                        if (cType == fType || fType.IsAssignableFrom(cType)) {
                            f.SetValue(m, c);
                            componentFound = true;
                            componentsCached++;
                        }
                    }

                    if (!componentFound)
                        Debug.LogError("[ComponentCacher] Component of type " + f.FieldType + " not found on " + gameObject.name);                
                }
            }

            Debug.LogWarning("[ComponentCacher] Components Cached: " + componentsCached);
            Debug.LogWarning("[ComponentCacher] GetComponent Calls: " + getComponentCalls);
            
        }
    }
}