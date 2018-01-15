using System.Collections;
using System.Collections.Generic;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class MethodManager : ReflectionWrapper, IMethodManager
    {
        public IList<IMethod> Methods
        {
            get
            {
                var methodList = new List<IMethod>();
                var sourceMethodList = GetProperty<IEnumerable>("Methods");

                foreach (object sourceMethod in sourceMethodList)
                {
                    methodList.Add(new Method(sourceMethod));
                }

                return methodList;
            }
        }

        public IMethod ActiveMethod
        {
            get
            {
                var sourceMethod = GetProperty("ActiveMethod");

                if (sourceMethod == null)
                    return null;

                return new Method(sourceMethod);
            }
        }

        public MethodManager(object source) : base(source)
        {
        }
        
    }
}
