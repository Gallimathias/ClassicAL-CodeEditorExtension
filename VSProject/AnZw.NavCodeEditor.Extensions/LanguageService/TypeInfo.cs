using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class TypeInfo : ReflectionWrapper
    {

        public string VariableName { get; }
        public string ObjectName { get; }
        public string DataTypeName { get; }

        public TypeInfo(object source) : base(source)
        {
            VariableName = GetProperty<string>("VariableName");
            ObjectName = GetProperty<string>("ObjectName");
            DataTypeName = GetProperty<string>("DataTypeName");

        }

}
}
