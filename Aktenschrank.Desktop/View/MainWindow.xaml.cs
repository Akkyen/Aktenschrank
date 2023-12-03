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
        private readonly MainWindowViewModel _mainWindowViewModel = new();

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

        private void TbNewSortingProfileName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (TbNewSortingProfileName.Text != "")
                {
                    _mainWindowViewModel.SortingProfiles.Add(new SortingProfile(TbNewSortingProfileName.Text));
                }
            }
        }

        private void BtCreateSortingProfile_Click(object sender, RoutedEventArgs e)
        {
            if (TbNewSortingProfileName.Text != "")
            {
                _mainWindowViewModel.SortingProfiles.Add(new SortingProfile(TbNewSortingProfileName.Text));
            }
        }

        private void BtDeleteSortingProfile_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile)
            {
                _mainWindowViewModel.SortingProfiles.Remove(sortingProfile);
            }
        }

        private void BtDuplicateSortingProfile_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile)
            {
                _mainWindowViewModel.SortingProfiles.Add(sortingProfile);
            }
        }

        private void LbDeleteSortingProfileCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile)
            {
                _mainWindowViewModel.SortingProfiles.Remove(sortingProfile);
            }
        }

        private void LbDuplicateSortingProfileCommand(object sender, ExecutedRoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile)
            {
                _mainWindowViewModel.SortingProfiles.Add((SortingProfile)sortingProfile.Clone());
            }
        }

        //-----------
        // Details
        //-----------

        private void BtCreateTarget_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile)
            {
                string folderPath = string.Empty;

                OpenFolderDialog openFolderDialog = new OpenFolderDialog();
                if (openFolderDialog.ShowDialog() == true)
                    folderPath = openFolderDialog.FolderName;

                sortingProfile.Targets.Add(new Target(folderPath));
            }
        }

        private void BtDeleteTarget_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile && LbSelectedSortingProfileTargets.SelectedItem is Target target)
            {
                sortingProfile.Targets.Remove(target);
            }
        }

        private void BtCreateBehaviour_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile)
            {
                sortingProfile.AddBehaviour();
            }
        }

        private void BtDeleteBehaviour_Click(object sender, RoutedEventArgs e)
        {
            if (LbSortingProfiles.SelectedItem is SortingProfile sortingProfile && LbSelectedSortingProfileBehaviours.SelectedItem is Behaviour behaviour)
            {
                sortingProfile.RemoveBehaviour(behaviour);
            }
        }
    }
}