using System.Windows;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for SnippetSelection.xaml
    /// </summary>
    public partial class SnippetSelection : Window
    {

        public SnippetSelectionVM ViewModel
        {
            get => (SnippetSelectionVM)DataContext;
            set => DataContext = value; 
        }

        public SnippetSelection()
        {
            InitializeComponent();
            Loaded += SnippetSelection_Loaded;
        }

        private void SnippetSelection_Loaded(object sender, RoutedEventArgs e) => ctSnippets.Focus();
        
        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Selected != null)
                Session.Current.ShowSettings();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e) => DialogResult = true;
        
    }
}
