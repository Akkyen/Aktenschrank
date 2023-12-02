using Aktenschrank.Model;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Aktenschrank.Desktop.ViewModel;
using Microsoft.Win32;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Aktenschrank.Desktop.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _mainWindowViewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _mainWindowViewModel;
        }


        private void OnlyLettersAndNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("([A-Z])|([a-z])|[0-9]");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void TbNewSortingUnitName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (TbNewSortingUnitName.Text != "")
                {
                    _mainWindowViewModel.SortingUnits.Add(new SortingUnit(TbNewSortingUnitName.Text));
                }
            }
        }

        private void BtCreateSortingUnit_Click(object sender, RoutedEventArgs e)
        {
            if (TbNewSortingUnitName.Text != "")
            {
                _mainWindowViewModel.SortingUnits.Add(new SortingUnit(TbNewSortingUnitName.Text));
            }
        }

        private void BtDeleteSortingUnit_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingUnits.SelectedItem != null)
            {
                _mainWindowViewModel.SortingUnits.Remove((SortingUnit)LbSortingUnits.SelectedItem);
            }
        }

        //-----------
        // Details
        //-----------

        private void BtCreateTarget_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingUnits.SelectedItem is SortingUnit sortingUnit)
            {
                string folderPath = string.Empty;

                OpenFolderDialog openFolderDialog = new OpenFolderDialog();
                if (openFolderDialog.ShowDialog() == true)
                    folderPath = openFolderDialog.FolderName;

                sortingUnit.Targets.Add(new Target(folderPath));
            }
        }

        private void BtDeleteTarget_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingUnits.SelectedItem is SortingUnit sortingUnit && LbSelectedSortingUnitTargets.SelectedItem is Target target)
            {
                sortingUnit.Targets.Remove(target);
            }
        }

        private void BtCreateBehaviour_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingUnits.SelectedItem is SortingUnit sortingUnit)
            {
                sortingUnit.AddBehaviour();
            }
        }

        private void BtDeleteBehaviour_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingUnits.SelectedItem is SortingUnit sortingUnit && LbSelectedSortingUnitBehaviours.SelectedItem is Behaviour behaviour)
            {
                sortingUnit.RemoveBehaviour(behaviour);
            }
        }

        
    }
}