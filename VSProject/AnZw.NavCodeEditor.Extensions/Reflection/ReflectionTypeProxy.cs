using System;
using System.Collections.Generic;
using System.Reflection;

namespace AnZw.NavCodeEditor.Extensions.Reflection
{
    public class ReflectionTypeProxy
    {
        public Type ObjectType { get; }

        public Dictionary<string, MethodInfo> MethodsCache { get; }
        public Dictionary<string, PropertyInfo> PropertiesCache { get; }

        public ReflectionTypeProxy(Type objectType)
        {
            ObjectType = objectType;
            MethodsCache = new Dictionary<string, MethodInfo>();
            PropertiesCache = new Dictionary<string, PropertyInfo>();
        }

        public object GetProperty(object sourceObject, string propertyName)
            => GetProperty(propertyName).GetValue(sourceObject);

        public void SetProperty(object sourceObject, string propertyName, object newValue)
            => GetProperty(propertyName).SetValue(sourceObject, newValue);

        public object CallMethod(object sourceObject, string methodName, params object[] parameters)
            => GetMethod(methodName).Invoke(sourceObject, parameters);

        protected MethodInfo GetMethod(string methodName)
        {
            if (MethodsCache.ContainsKey(methodName))
                return MethodsCache[methodName];

            MethodInfo methodInfo = ObjectType.GetMethod(methodName, BindingFlags.Instance |
                BindingFlags.NonPublic |
                BindingFlags.Public);
            MethodsCache.Add(methodName, methodInfo);
            return methodInfo;
        }

        protected PropertyInfo GetProperty(string propertyName)
        {
            if (PropertiesCache.ContainsKey(propertyName))
                return PropertiesCache[propertyName];

            PropertyInfo propertyInfo = ObjectType.GetProperty(propertyName, BindingFlags.Instance |
                        BindingFlags.NonPublic |
                        BindingFlags.Public);

            PropertiesCache.Add(propertyName, propertyInfo);
            return propertyInfo;
        }



    }
}
