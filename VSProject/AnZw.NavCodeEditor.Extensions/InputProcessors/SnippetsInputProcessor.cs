using System.Windows.Input;
using AnZw.NavCodeEditor.Extensions.Snippets;
using AnZw.NavCodeEditor.Extensions.UI;

namespace AnZw.NavCodeEditor.Extensions.InputProcessors
{
    public class SnippetsInputProcessor : InputProcessor
    {

        public SnippetsInputProcessor(CALKeyProcessor keyProcessor) : base(keyProcessor)
        {
        }

        public override void KeyDown(KeyEventArgs args, KeyStateInfo keyStateInfo)
        {
            if (args.Handled)
                return;

            //open snippets selection window
            if (keyStateInfo.Equals(Session.Current.Settings.SnippetSelectionKeyStateInfo))
            {
                var viewModel = new SnippetSelectionVM(Session.Current.SnippetManager);
                var snippetSelection = new SnippetSelection
                {
                    ViewModel = viewModel
                };

                if (snippetSelection.ShowDialog() == true)
                {
                    if (snippetSelection.ViewModel.Selected != null)
                        RunSnippet(snippetSelection.ViewModel.Selected);
                }

                args.Handled = true;
                return;
            }

            //check snippet hot keys
            foreach (var snippet in Session.Current.Settings.Snippets)
            {
                if (!string.IsNullOrWhiteSpace(snippet.HotKey))
                {
                    var snippetKeyStateInfo = new KeyStateInfo(snippet.HotKey);
                    if (keyStateInfo.Equals(snippetKeyStateInfo))
                    {
                        RunSnippet(snippet);
                        args.Handled = true;
                        return;
                    }
                }
            }
        }

        public void RunSnippet(Snippet snippet)
        {
            //collect current information
            var selectedText = KeyProcessor.SelectedText;
            Session.Current.SnippetManager.SelectedTextFunction.Value = selectedText;

            int indent = KeyProcessor.CurrentLineInformation.CaretColumn;

            if (!string.IsNullOrEmpty(selectedText))
                indent -= selectedText.Length;

            if (indent < 0)
                indent = 0;

            //parse snippet
            var newText = Session.Current.SnippetManager.ParseSnippet(snippet, indent, KeyProcessor);

            //clear cached information
            Session.Current.SnippetManager.SelectedTextFunction.Value = "";

            //add snippet text to source editor
            if (!string.IsNullOrEmpty(newText))
                KeyProcessor.EditorOperations.InsertText(newText);
        }

    }
}
