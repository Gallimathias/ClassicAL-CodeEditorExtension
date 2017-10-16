using System.Windows;
using Microsoft.Win32;
using AnZw.NavCodeEditor.Extensions.Snippets;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for ExtensionSettings.xaml
    /// </summary>
    public partial class ExtensionSettings : Window
    {
        public const string SETTINGS_FILEMASK = "Xml Files (*.xml)|*.xml|All files (*.*)|*.*";

        public SessionSettings Settings => (SessionSettings)DataContext;

        public ExtensionSettings()
        {
            InitializeComponent();
            DataContext = Session.Current.Settings;
        }

        private void BtnNewSnippet_Click(object sender, RoutedEventArgs e) => NewSnippet();

        private void BtnEditSnippet_Click(object sender, RoutedEventArgs e)
        {
            if (ctSnippets.SelectedItem is Snippet snippet)
                EditSnippet(snippet);
        }

        private void BtnDeleteSnippet_Click(object sender, RoutedEventArgs e)
        {
            if (ctSnippets.SelectedItem is Snippet snippet)
                DeleteSnippet(snippet);
        }

        protected void NewSnippet()
        {
            var snippet = new Snippet();
            var details = new SnippetDetails
            {
                Settings = Settings,
                DataContext = snippet
            };

            var result = details.ShowDialog();
            if ((result.HasValue) && (result.Value))
                Settings.Snippets.Add(snippet);
        }

        protected void EditSnippet(Snippet snippet)
        {
            var editableSnippet = new Snippet(snippet);
            var details = new SnippetDetails
            {
                Settings = Settings,
                DataContext = editableSnippet
            };

            var result = details.ShowDialog();

            if ((result.HasValue) && (result.Value))
                snippet.CopyFrom(editableSnippet);
        }

        protected void DeleteSnippet(Snippet snippet)
        {
            if (MessageBox.Show(
                $"Do you want to delete snippet '{snippet.Name}'?",
                "Question",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Settings.Snippets.Remove(snippet);
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e) => DialogResult = true;

        private void BtnLoad_Click(object sender, RoutedEventArgs e) => LoadSettings();

        private void BtnSave_Click(object sender, RoutedEventArgs e) => SaveSettings();

        private void BtnReset_Click(object sender, RoutedEventArgs e) => ResetSettings();

        protected void SaveSettings()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = SETTINGS_FILEMASK
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                if (Settings.SaveSettings(saveFileDialog.FileName, true))
                    MessageBox.Show("Settings have been saved to file.",
                        "Information",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
            }
        }

        protected void LoadSettings()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = SETTINGS_FILEMASK
            };

            if (openFileDialog.ShowDialog() == true)
            {
                SessionSettings newSettings = SessionSettings.LoadSettings(openFileDialog.FileName, false, true);
                if (newSettings != null)
                    Settings.CopyFrom(newSettings, false);
            }
        }

        protected void ResetSettings()
        {
            if (MessageBox.Show(
                "Do you want to load global settings? All your changes will be removed.",
                "Question",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Settings.CopyFrom(Session.Current.GlobalSettings, false);
            }
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void BtnDeleteVariable_Click(object sender, RoutedEventArgs e)
        {
            if (ctlParameters.SelectedItem is SnippetVariable selectedVariable)
                Settings.Variables.Remove(selectedVariable);
        }

        private void BtnNewVariable_Click(object sender, RoutedEventArgs e)
        {
            SnippetVariable selectedVariable = Settings.Variables.AddNew();
            ctlParameters.SelectedItem = selectedVariable;
        }
    }
}
