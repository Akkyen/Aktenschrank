using Aktenschrank.Model.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Aktenschrank.Model;

public abstract class AStatement : ObservableObject, ICloneable, IVisitorComponent
{
    protected Guid _guid = Guid.NewGuid();

    protected AStatement? _predecessor;
    protected AStatement? _successor;

    public Guid Guid
    {
        get => _guid;
    }

    public AStatement? Predecessor
    {
        get => _predecessor;
        set
        {
            if (Equals(value, _predecessor)) return;
            _predecessor = value;
            OnPropertyChanged();
        }
    }

    public AStatement? Successor
    {
        get => _successor;
        set
        {
            if (Equals(value, _successor)) return;
            _successor = value;
            OnPropertyChanged();
        }
    }

    public void Accept(IStatementVisitor statementVisitor)
    {
        statementVisitor.Visit(this);
    }

    public object Clone()
    {
        AStatement rValue = (AStatement)MemberwiseClone();

        rValue.Predecessor = this;

        Successor = rValue;
        rValue.Successor = Successor;

        return rValue;
    }
}