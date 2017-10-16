using System.Windows;

namespace AnZw.NavCodeEditor.Extensions.UI.CodeGenerators
{
    /// <summary>
    /// Interaction logic for RecordFieldListCodeGeneratorWindow.xaml
    /// </summary>
    public partial class RecordFieldListCodeGeneratorWindow : Window
    {

        public RecordFieldListCodeGeneratorVM ViewModel
        {
            get => (RecordFieldListCodeGeneratorVM)DataContext;
            set => DataContext = value;
        }

        public RecordFieldListCodeGeneratorWindow()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SetSelectedFields(lstFields.SelectedItems);
            DialogResult = true;
        }

    }
}
