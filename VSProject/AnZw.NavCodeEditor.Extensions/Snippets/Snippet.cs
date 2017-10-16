namespace AnZw.NavCodeEditor.Extensions.Snippets
{
    public class Snippet : ObservableObject<Snippet>
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
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        public string HotKey
        {
            get => hotKey;
            set => SetProperty(ref hotKey, value);
        }

        /// <summary>
        /// Selection transformation snippet
        /// </summary>
        public bool SelectionTransformationSnippet
        {
            get => selectionTransformationSnippet;
            set => SetProperty(ref selectionTransformationSnippet, value);
        }

        private string name;
        private string description;
        private string content;
        private string hotKey;
        private bool selectionTransformationSnippet;

        public Snippet()
        {
        }
        public Snippet(Snippet source) : this()
        {
            CopyFrom(source);
        }

        public virtual string Run(CALKeyProcessor keyProcessor) => Content;

    }
}

