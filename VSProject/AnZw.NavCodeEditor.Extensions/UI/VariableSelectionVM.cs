using System.Linq;
using System.ComponentModel;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    public class VariableSelectionVM : ObservableObject
    {
        public SnippetManager SnippetManager { get; }
        public BindingList<SnippetFunction> Variables { get; set; }
        public SnippetFunction Selected
        {
            get => selected; 
            set { SetProperty(ref selected, value); }
        }

        private SnippetFunction selected;

        public VariableSelectionVM(SnippetManager snippetManager)
        {
            SnippetManager = snippetManager;
            Variables = new BindingList<SnippetFunction>();

            foreach (SnippetVariable variable in SnippetManager.Variables)
                Variables.Add(variable);

            foreach (SnippetFunction function in SnippetManager.Functions.Values)
                Variables.Add(function);

            selected = Variables.FirstOrDefault();
        }

    }
}
