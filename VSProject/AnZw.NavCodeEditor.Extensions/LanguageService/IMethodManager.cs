using System.Collections.Generic;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public interface IMethodManager
    {

        IList<IMethod> Methods { get; }
        IMethod ActiveMethod { get; }

    }
}
