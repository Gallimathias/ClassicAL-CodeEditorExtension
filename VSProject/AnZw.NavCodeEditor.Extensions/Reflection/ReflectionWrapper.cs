using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnZw.NavCodeEditor.Extensions.Reflection
{
    public class ReflectionWrapper
    {
        public object ReflectionWrapperSourceObject => source;

        private object source;
        private Type sourceType;
        private ReflectionTypeProxy proxy;

        public ReflectionWrapper()
        {
            source = null;
            sourceType = null;
            proxy = null;
        }
        public ReflectionWrapper(object source)
        {
            Initialize(source);
        }

        protected void Initialize(object source, Type sourceType)
        {
            this.source = source;
            this.sourceType = sourceType;
            proxy = ReflectionTypeProxyCache.GetReflectionTypeProxy(this.sourceType);
        }
        protected void Initialize(object source) => Initialize(source, source.GetType());

        public object GetProperty(string propertyName) => proxy.GetProperty(source, propertyName);
        public T GetProperty<T>(string propertyName) => (T)GetProperty(propertyName);

        public object CallMethod(string methodName, params object[] parameters)
            => proxy.CallMethod(source, methodName, parameters);
        public T CallMethod<T>(string methodName, params object[] parameters) => (T)CallMethod(methodName, parameters);


    }
}
