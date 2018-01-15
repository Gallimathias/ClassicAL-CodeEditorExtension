using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions.Reflection
{
    public class ReflectionTypeProxyCache
    {
        private static Dictionary<string, ReflectionTypeProxy> proxyCache;

        static ReflectionTypeProxyCache()
        {
            proxyCache = new Dictionary<string, ReflectionTypeProxy>();
        }

        public static ReflectionTypeProxy GetReflectionTypeProxy(Type objectType)
        {
            var key = objectType.FullName;

            if (proxyCache.ContainsKey(key))
                return proxyCache[key];

            var proxyObject = new ReflectionTypeProxy(objectType);
            proxyCache.Add(key, proxyObject);
            return proxyObject;
        }

    }
}
