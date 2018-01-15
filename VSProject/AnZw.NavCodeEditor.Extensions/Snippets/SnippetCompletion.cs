using Microsoft.VisualStudio.Language.Intellisense;

namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class SnippetCompletion : Completion
    {
        public Snippet Snippet { get; }
        public int Indent { get; }

        public SnippetCompletion(Snippet snippet, int indent)
        {
            Snippet = snippet;
            Description = Snippet.Description;
            DisplayText = Snippet.Name;
            Indent = indent;
        }

        public override string InsertionText => 
            Session.Current.SnippetManager.ParseSnippet(Snippet, Indent, null);
        

    }
}
