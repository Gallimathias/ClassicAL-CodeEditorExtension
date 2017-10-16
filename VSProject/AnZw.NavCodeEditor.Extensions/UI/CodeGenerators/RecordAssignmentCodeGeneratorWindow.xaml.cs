using System.Windows;

namespace AnZw.NavCodeEditor.Extensions.UI.CodeGenerators
{
    /// <summary>
    /// Interaction logic for RecordAssignmentCodeGeneratorWindow.xaml
    /// </summary>
    public partial class RecordAssignmentCodeGeneratorWindow : Window
    {
        public RecordAssignmentCodeGeneratorVM ViewModel
        {
            get => (RecordAssignmentCodeGeneratorVM)DataContext;
            set => DataContext = value;
        }

        public RecordAssignmentCodeGeneratorWindow()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e) => DialogResult = true;

    }
}
