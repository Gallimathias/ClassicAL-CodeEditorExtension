using System.Windows;
using System.Windows.Input;

namespace AnZw.NavCodeEditor.Extensions.UI
{
    /// <summary>
    /// Interaction logic for VariableSelection.xaml
    /// </summary>
    public partial class VariableSelection : Window
    {

        public VariableSelectionVM ViewModel
        {
            get => (VariableSelectionVM)DataContext;
            set => DataContext = value; 
        }

        public VariableSelection()
        {
            InitializeComponent();
            Loaded += VariableSelection_Loaded;
        }

        private void VariableSelection_Loaded(object sender, RoutedEventArgs e)
        {
            ctVariables.Focus();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.Selected != null)
                DialogResult = true;
        }

        private void CtVariables_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.Selected != null)
                DialogResult = true;
        }
    }
}
