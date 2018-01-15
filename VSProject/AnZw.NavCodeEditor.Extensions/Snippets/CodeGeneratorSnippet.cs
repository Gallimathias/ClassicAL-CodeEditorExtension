using System.Collections.Generic;
using AnZw.NavCodeEditor.Extensions.LanguageService;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{

    /// <summary>
    /// Snippet generating code
    /// </summary>
    public class CodeGeneratorSnippet : Snippet
    {
        public override string Run(CALKeyProcessor keyProcessor) => null;

        protected IList<TypeInfo> GetLocalTypes(CALKeyProcessor keyProcessor) 
            => keyProcessor.NavConnector.Context.LanguageService.Locals.Get(keyProcessor.MethodManager.ActiveMethod);
        
        protected void AddRecordTypes(CALKeyProcessor keyProcessor, IList<TypeInfo> destList)
        {
            IList<TypeInfo> types = GetLocalTypes(keyProcessor);
            foreach (TypeInfo typeInfo in types)
            {
                if (typeInfo.DataTypeName == "Record")
                    destList.Add(typeInfo);
            }
        }

    }

}
