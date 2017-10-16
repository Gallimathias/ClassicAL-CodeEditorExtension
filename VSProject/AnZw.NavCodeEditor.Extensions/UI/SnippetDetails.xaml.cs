using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for SnippetDetails.xaml
    /// </summary>
    public partial class SnippetDetails : Window
    {
        public SessionSettings Settings
        {
            get => settings;
            set
            {
                settings = value;
                if (snippetManager != null)
                    snippetManager.Settings = settings;
            }
        }
        public SnippetManager SnippetManager
        {
            get
            {
                if (snippetManager == null)
                    snippetManager = new SnippetManager(this.Settings);

                return snippetManager;
            }
        }

        private SessionSettings settings;
        private SnippetManager snippetManager;

        public Snippet Snippet => (Snippet)DataContext;

        public SnippetDetails()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e) => DialogResult = true;

        private void OnInsertVariable(Object sender, ExecutedRoutedEventArgs e) => InsertVariable();

        private void OnTestSnippet(Object sender, ExecutedRoutedEventArgs e) => TestSnippet();
        
        protected void InsertVariable()
        {
            VariableSelection selection = new VariableSelection
            {
                ViewModel = new VariableSelectionVM(SnippetManager)
            };

            if (selection.ShowDialog() == true)
            {
                if (selection.ViewModel.Selected != null)
                    txtContent.SelectedText = $"{{{ selection.ViewModel.Selected.Name}}}";
            }
        }

        protected void TestSnippet()
        {
            var editedSnippet = Snippet;
            if (editedSnippet != null)
            {
                try
                {
                    string text = SnippetManager.ParseSnippet(editedSnippet, 0, null);
                    MessageBox.Show(text, "Snippet", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Snippet parsing error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
