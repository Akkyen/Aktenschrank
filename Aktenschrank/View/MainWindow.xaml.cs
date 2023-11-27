using System.Windows;
using Aktenschrank.ViewModel;

namespace Aktenschrank.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel mainWindowViewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainWindowViewModel;


        }
    }
}