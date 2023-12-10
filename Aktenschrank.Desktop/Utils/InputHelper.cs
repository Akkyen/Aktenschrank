using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows;

namespace Aktenschrank.Desktop.Utils;

public static class InputHelper
{
    private static Regex _nameRegex = new Regex("([^A-Za-z0-9 ])");
    private static Regex _textRegex = new Regex("([^A-Za-z0-9,.!?\n])");

    public static void CheckIfNameIsAllowed_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        bool isAllowed = IsNameAllowed(e.Text);

        e.Handled = isAllowed;

        if (e.OriginalSource is TextBox textBox && isAllowed)
        {
            BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
            binding?.UpdateSource();
        }
    }

    public static void CheckIfTextIsAllowed_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        bool isAllowed = IsTextAllowed(e.Text);

        e.Handled = isAllowed;

        if (e.OriginalSource is TextBox textBox && isAllowed)
        {
            BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
            binding?.UpdateSource();
        }
    }

    public static void OnPaste(object sender, DataObjectPastingEventArgs e)
    {
        if (e.DataObject.GetDataPresent(typeof(string)))
        {
            string text = (string)e.DataObject.GetData(typeof(string));

            if (!IsTextAllowed(text))
            {
                e.CancelCommand();
            }
        }
        else
        {
            e.CancelCommand();
        }
    }

    public static void UpdateTextBinding(object sender, KeyEventArgs e)
    {
        if (e.OriginalSource is TextBox textBox)
        {
            BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
            binding?.UpdateSource();
        }
    }

    private static bool IsNameAllowed(string name)
    {
        return _nameRegex.IsMatch(name);
    }

    private static bool IsTextAllowed(string text)
    {
        return _textRegex.IsMatch(text);
    }
}