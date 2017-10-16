using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class Method : ReflectionWrapper, IMethod
    {

        public int Id { get; }
        public string Name { get; }
        public string Signature { get; }

        public Method(object source) : base(source)
        {
            Id = GetProperty<int>("Id");
            Name = GetProperty<string>("Name");
            Signature = GetProperty<string>("Signature");
        }

        public IEnumerable<string> GetLines() => CallMethod<IEnumerable<string>>("GetLines");

        public Tuple<SnapshotPoint, SnapshotPoint> GetCodeInterval()
            => (Tuple<SnapshotPoint, SnapshotPoint>)CallMethod("GetCodeInterval");

    }
}
