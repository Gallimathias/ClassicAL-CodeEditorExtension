using System.ComponentModel;
using AnZw.NavCodeEditor.Extensions.LanguageService;

namespace AnZw.NavCodeEditor.Extensions.UI.CodeGenerators
{
    public class RecordAssignmentCodeGeneratorVM : ObservableObject<RecordAssignmentCodeGeneratorVM>
    {
        public BindingList<TypeInfo> Variables { get; }
        public string SourceVariableName
        {
            get => sourceVariableName; 
            set => SetProperty(ref sourceVariableName, value); 
        }
        public string DestVariableName
        {
            get => destVariableName; 
            set => SetProperty(ref destVariableName, value); 
        }
        public bool WithValidation
        {
            get => withValidation; 
            set => SetProperty(ref withValidation, value); 
        }
        public bool AllFields
        {
            get => allFields; 
            set => SetProperty(ref allFields, value); 
        }
        public bool MatchByName
        {
            get => matchByName; 
            set => SetProperty(ref matchByName, value); 
        }

        private string destVariableName;
        private string sourceVariableName;
        private bool withValidation;
        private bool allFields;
        private bool matchByName;
        
        public RecordAssignmentCodeGeneratorVM()
        {
            Variables = new BindingList<TypeInfo>();
            DestVariableName = "";
            SourceVariableName = "";
            WithValidation = true;
            AllFields = true;
            MatchByName = true;
        }

    }
}
