using System.Collections;
using System.Collections.Generic;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class SignatureInfo : ReflectionWrapper
    {

        public string ParentTypeName { get; set; }
        public string MethodName { get; set; }
        public ParameterInfo ReturnType { get; set; }
        public List<ParameterInfo> Parameters { get; }
        public string FormattedValue { get; set; }

        public SignatureInfo(object source) : base(source)
        {
            Parameters = new List<ParameterInfo>();

            //copy properties
           ParentTypeName = GetProperty<string>("ParentTypeName");
           MethodName = GetProperty<string>("MethodName");
           FormattedValue = GetProperty<string>("FormattedValue");

            object sourceReturnType = GetProperty("ReturnType");
            if (sourceReturnType == null)
                ReturnType = null;
            else
                ReturnType = new ParameterInfo(sourceReturnType);

            var sourceParameters = GetProperty<IEnumerable>("Parameters");
            if (sourceParameters != null)
            {
                foreach (object sourceParameter in sourceParameters)
                    Parameters.Add(new ParameterInfo(sourceParameter));
            }
        }

    }
}
