using System;
using System.ComponentModel;
using System.Linq;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    public class SnippetSelectionVM : ObservableObject<SnippetSelectionVM>
    {
        public SnippetManager SnippetManager { get; }
        public BindingList<Snippet> Snippets { get; }
        public Snippet Selected
        {
            get => selected;
            set { SetProperty(ref selected, value); }
        }
        public string NameFilter
        {
            get => nameFilter;
            set
            {
                if (SetProperty(ref nameFilter, value))
                    Selected = SelectSnippetByName(nameFilter);
            }
        }

        private string nameFilter;
        private Snippet selected;

        public SnippetSelectionVM(SnippetManager snippetManager)
        {
            SnippetManager = snippetManager;
            Snippets = new BindingList<Snippet>();

            foreach (var snippet in SnippetManager.Settings.Snippets)
                Snippets.Add(snippet);

            foreach (var generatorSnippet in SnippetManager.CodeGeneratorSnippets)
                Snippets.Add(generatorSnippet);

            Selected = Snippets.FirstOrDefault();
        }

        protected Snippet SelectSnippetByName(string nameToFind)
        {
            if (string.IsNullOrWhiteSpace(nameToFind))
                return Snippets.FirstOrDefault();

            nameToFind = NameFilter.ToLower();
            Snippet nameStartMatches = null;
            Snippet nameContains = null;

            foreach (var snippet in Snippets)
            {
                string snippetName = snippet.Name.ToLower();

                if (snippetName == nameToFind)
                    return snippet;

                if ((nameStartMatches == null) && snippet.Name.StartsWith(NameFilter,
                    StringComparison.CurrentCultureIgnoreCase))
                    nameStartMatches = snippet;

                if (nameContains == null && snippetName.Contains(NameFilter.ToLower()))
                    nameContains = snippet;
            }

            if (nameStartMatches != null)
                return nameStartMatches;

            return nameContains;
        }

    }
}
