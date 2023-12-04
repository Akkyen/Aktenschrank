using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Aktenschrank.Utils;

public static class Commands
{
    //General Commands
    public static RelayCommand<TextBox> UpdateTextBinding_Command = new(textBox =>
    {
        if (textBox != null)
        {
            BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
            binding?.UpdateSource();
        }
    });

    public static RelayCommand<ListBox> DeleteSelectedItem_Command = new RelayCommand<ListBox>(listBox =>
    {
        if (listBox != null)
        {
            foreach (var item in listBox.SelectedItems)
            {
                listBox.Items.Remove(item);
            }
        }
    });

    public static RoutedCommand LbDeleteSortingProfileCommand = new();
    public static RoutedCommand LbDuplicateSortingProfileCommand = new();
}