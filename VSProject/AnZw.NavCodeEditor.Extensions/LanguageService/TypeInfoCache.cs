using System.Collections;
using System.Collections.Generic;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class TypeInfoCache : ReflectionWrapper
    {
        public TypeInfoCache(object source) : base(source)
        {
        }

        public IList<TypeInfo> Get(object key)
        {
            if (key is ReflectionWrapper reflectionWrapperKey)
                key = reflectionWrapperKey.ReflectionWrapperSourceObject;

            var sourceList = CallMethod<IEnumerable>("Get", key);
            var typeList = new List<TypeInfo>();

            foreach (object sourceTypeInfo in sourceList)
                typeList.Add(new TypeInfo(sourceTypeInfo));

            return typeList;
        }

    }
}
