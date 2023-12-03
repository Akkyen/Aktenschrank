using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aktenschrank.Model;

public class Statement : INotifyPropertyChanged, ICloneable
{
    private Statement? _predecessor;
    private Statement? _successor;

    public Statement? Predecessor
    {
        get => _predecessor;
        set
        {
            if (Equals(value, _predecessor)) return;
            _predecessor = value;
            OnPropertyChanged();
        }
    }

    public Statement? Successor
    {
        get => _successor;
        set
        {
            if (Equals(value, _successor)) return;
            _successor = value;
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

    public object Clone()
    {
        Statement rValue = new Statement();

        rValue.Predecessor = this;

        this.Successor = rValue;
        rValue.Successor = this.Successor;

        return rValue;
    }
}