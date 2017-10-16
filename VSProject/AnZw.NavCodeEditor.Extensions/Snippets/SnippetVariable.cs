namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetVariable : SnippetFunction
    {
        public string Value
        {
            get => value; 
            set => SetProperty(ref this.value, value); 
        }

        private string value;
        
        public SnippetVariable()
        {
            Value = "";
        }
        public SnippetVariable(SnippetVariable source) : this()
        {
            CopyFrom(source);
        }

        public void CopyFrom(SnippetVariable source)
        {
           Name = source.Name;
           Description = source.Description;
           Value = source.Value;
        }

        public override string GetValue(string formatString) => Value;
        
    }
}
