using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoR2QoLRewrite.Util
{
    public static class StaticCache
    {
        private static readonly Dictionary<Type, object> _cache;

        public static T Get<T>() => (T)_cache[typeof(T)];
        public static void Add<T>(T val) => _cache[typeof(T)] = val;
        public static void Dispose<T>() => _cache[typeof(T)] = null;
    }
}
