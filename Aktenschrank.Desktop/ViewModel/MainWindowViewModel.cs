using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Aktenschrank.Model;

namespace Aktenschrank.Desktop.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _tbNewSortingProfileName;

    private ObservableSet<SortingProfile> _sortingProfiles = new();


    private void KeyPress_DeleteItem(ListBox listBox, KeyEventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            listBox.SelectedItems.Remove(listBox.SelectedItem);
        }
    }

    private void LbSortingProfiles_KeyDuplicate(ListBox lbSortingProfiles, KeyEventArgs e)
    {
        if (lbSortingProfiles.SelectedItem is SortingProfile sortingProfile && lbSortingProfiles.SelectedItems is ObservableSet<SortingProfile> setSortingProfiles)
        {
            string origName = sortingProfile.Name;

            for (int i = 0; i < int.MaxValue; i++)
            {
                sortingProfile.Name = origName + i;

                if (setSortingProfiles.Add(sortingProfile))
                {
                    break;
                }
            }

            throw new Exception("Too many duplicates of " + origName);
        }
    }


    public string TbNewSortingProfileName
    {
        get => _tbNewSortingProfileName;
        set
        {
            if (value == _tbNewSortingProfileName) return;
            _tbNewSortingProfileName = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public ObservableSet<SortingProfile> SortingProfiles
    {
        get => _sortingProfiles;
        set
        {
            if (Equals(value, _sortingProfiles)) return;
            _sortingProfiles = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}