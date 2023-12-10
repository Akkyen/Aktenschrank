using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Aktenschrank.Desktop.Utils;

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
}