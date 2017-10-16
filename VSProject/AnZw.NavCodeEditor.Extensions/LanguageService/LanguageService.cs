using AnZw.NavCodeEditor.Extensions.Reflection;


namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class LanguageService : ReflectionWrapper
    {
        public TypeInfoCache Globals
        {
            get
            {
                if (globals == null)
                    globals = new TypeInfoCache(GetProperty("Globals"));

                return locals;
            }
        }
        public TypeInfoCache Locals
        {
            get
            {
                if (locals == null)
                    locals = new TypeInfoCache(GetProperty("Locals"));

                return locals;
            }
        }

        private TypeInfoCache globals;
        private TypeInfoCache locals;
       

        public LanguageService(object source) : base(source)
        {
        }
        
    }
}
