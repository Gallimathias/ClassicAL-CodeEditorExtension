using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class FieldInfo : ReflectionWrapper
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Width { get; set; }

        public FieldInfo(object source) : base(source)
        {
            Id = GetProperty<int>("Id");
            Name = GetProperty<string>("Name");
            Type = GetProperty<string>("Type");
            Width = GetProperty<int>("Width");
        }

    }
}
