using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class ParameterInfo : ReflectionWrapper
    {
        public string TypeName { get; }
        public string ParameterName { get; }

        public ParameterInfo(object source) : base(source)
        {
            TypeName = GetProperty<string>("TypeName");
            ParameterName = GetProperty<string>("ParameterName");
        }

    }
}
