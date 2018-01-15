using System.Windows;

namespace AnZw.NavCodeEditor.Setup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindowVM ViewModel
        {
            get => (MainWindowVM)DataContext;
            set => DataContext = value;
        }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowVM();
        }

        private void BtnShowSettings_Click(object sender, RoutedEventArgs e) => ViewModel.Session.ShowSettings();

    }
}
