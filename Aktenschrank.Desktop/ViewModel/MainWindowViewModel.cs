using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using Aktenschrank.Model;

namespace Aktenschrank.Desktop.ViewModel;

public class MainWindowViewModel : INotifyPropertyChanged
{
    private string _tbNewSortingUnitName;

    private ObservableSet<SortingUnit> _sortingUnits = new();


    private void KeyPress_DeleteItem(ListBox listBox, KeyEventArgs e)
    {
        if (listBox.SelectedItem != null)
        {
            listBox.SelectedItems.Remove(listBox.SelectedItem);
        }
    }

    private void LbSortingUnits_KeyDuplicate(ListBox lbSortingUnits, KeyEventArgs e)
    {
        if (lbSortingUnits.SelectedItem is SortingUnit sortingUnit && lbSortingUnits.SelectedItems is ObservableSet<SortingUnit> setSortingUnits)
        {
            string origName = sortingUnit.Name;

            for (int i = 0; i < int.MaxValue; i++)
            {
                sortingUnit.Name = origName + i;

                if (setSortingUnits.Add(sortingUnit))
                {
                    break;
                }
            }

            throw new Exception("Too many duplicates of " + origName);
        }
    }


    public string TbNewSortingUnitName
    {
        get => _tbNewSortingUnitName;
        set
        {
            if (value == _tbNewSortingUnitName) return;
            _tbNewSortingUnitName = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public ObservableSet<SortingUnit> SortingUnits
    {
        get => _sortingUnits;
        set
        {
            if (Equals(value, _sortingUnits)) return;
            _sortingUnits = value ?? throw new ArgumentNullException(nameof(value));
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