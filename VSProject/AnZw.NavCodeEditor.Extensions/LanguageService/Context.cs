using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class Context : ReflectionWrapper
    {
        public LanguageService LanguageService
        {
            get
            {
                if (languageService == null)
                    languageService = new LanguageService(GetProperty("LanguageService"));
                return languageService;
            }
        }

        private LanguageService languageService;

        public Context(object source) : base(source)
        {
        }
    }
}
