using System.Collections;
using System.Collections.Generic;
using AnZw.NavCodeEditor.Extensions.Reflection;

namespace AnZw.NavCodeEditor.Extensions.LanguageService
{
    public class TypeInfoManager : ReflectionWrapper
    {

        public TypeInfoManager(object source) : base(source)
        {
        }

        public IEnumerable<SignatureInfo> GetSignatures(string methodName)
        {
            var sourceSignatureList = CallMethod<IEnumerable>("GetSignatures", methodName);

            if (sourceSignatureList == null)
                return null;

            var signatures = new List<SignatureInfo>();

            foreach (var sourceSignature in sourceSignatureList)
                signatures.Add(new SignatureInfo(sourceSignature));

            return signatures;
        }

        public IEnumerable<FieldInfo> GetFields(string owner)
        {
            var sourceFieldList = CallMethod<IEnumerable>("GetFields", owner);

            if (sourceFieldList == null)
                return null;

            var fields = new List<FieldInfo>();

            foreach (var sourceField in sourceFieldList)
                fields.Add(new FieldInfo(sourceField));
            
            return fields;
        }

    }
}
