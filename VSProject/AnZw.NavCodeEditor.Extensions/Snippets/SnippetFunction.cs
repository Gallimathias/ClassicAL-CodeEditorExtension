namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetFunction : ObservableObject<SnippetFunction>
    {
        public string Name
        {
            get => name; 
            set => SetProperty(ref name, value); 
        }
        public string Description
        {
            get => description; 
            set => SetProperty(ref description, value); 
        }

        private string name;
        private string description;

        public SnippetFunction()
        {
           Name = "";
           Description = "";
        }

        public virtual string GetValue(string formatString) => "";

    }
}
