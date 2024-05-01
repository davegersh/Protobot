using System;

namespace Protobot {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class CacheComponentAttribute : Attribute {}
}
