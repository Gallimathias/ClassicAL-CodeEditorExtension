using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using AnZw.NavCodeEditor.Extensions.LanguageService;

namespace AnZw.NavCodeEditor.Extensions.UI.CodeGenerators
{
    public class RecordFieldListCodeGeneratorVM : ObservableObject<RecordFieldListCodeGeneratorVM>
    {
        public string Template
        {
            get => template;
            set => SetProperty(ref template, value);
        }
        public BindingList<FieldInfo> Fields { get; }
        public List<FieldInfo> SelectedFields { get; }
        public BindingList<TypeInfo> Variables { get; }
        public TypeInfoManager TypeInfoManager { get; }
        public string VariableName
        {
            get => variableName;
            set
            {
                if (SetProperty(ref variableName, value))
                    LoadFields();
            }
        }

        private string template;
        private string variableName;

        public RecordFieldListCodeGeneratorVM(TypeInfoManager typeInfoManager)
        {
            Fields = new BindingList<FieldInfo>();
            SelectedFields = new List<FieldInfo>();
            Variables = new BindingList<TypeInfo>();
            TypeInfoManager = typeInfoManager;
            VariableName = "";
            Template = "";
        }

        protected void LoadFields()
        {
            Fields.Clear();

            IEnumerable<FieldInfo> loadedFieldList = TypeInfoManager.GetFields(VariableName);
            if (loadedFieldList != null)
            {
                foreach (var field in loadedFieldList)
                    Fields.Add(field);
            }
        }

        public void SetSelectedFields(IList selectedFields)
        {
            SelectedFields.Clear();
            foreach (FieldInfo fieldInfo in selectedFields)
            {
                SelectedFields.Add(fieldInfo);
            }
        }

    }
}
