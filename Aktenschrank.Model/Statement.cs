using CommunityToolkit.Mvvm.ComponentModel;

namespace Aktenschrank.Model;

public class Statement : ObservableObject, ICloneable
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

    public object Clone()
    {
        Statement rValue = new Statement();

        rValue.Predecessor = this;

        this.Successor = rValue;
        rValue.Successor = this.Successor;

        return rValue;
    }
}