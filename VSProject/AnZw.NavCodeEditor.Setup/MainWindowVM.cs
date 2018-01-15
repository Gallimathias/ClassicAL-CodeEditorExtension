using AnZw.NavCodeEditor.Extensions;

namespace AnZw.NavCodeEditor.Setup
{
    public class MainWindowVM
    {
        public Session Session { get; }

        public MainWindowVM()
        {
            Session = Session.Current;
        }

    }
}
