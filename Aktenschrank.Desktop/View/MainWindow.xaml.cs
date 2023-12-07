using Aktenschrank.Model;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Aktenschrank.Desktop.ViewModel;
using Microsoft.Win32;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.Windows.Data;

namespace Aktenschrank.Desktop.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _mainWindowViewModel;
        }

        private void UpdateTextBinding(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox)
            {
                BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }

        private void OnlyLettersAndNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("([A-Z])|([a-z])|[0-9]");
            e.Handled = !regex.IsMatch(e.Text);

            if (e.OriginalSource is TextBox textBox)
            {
                BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }

        private void UcBehaviour_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void LbSortingProfiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}