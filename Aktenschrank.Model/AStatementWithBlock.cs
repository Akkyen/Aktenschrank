using Aktenschrank.Model.Statements;

namespace Aktenschrank.Model;

public abstract class AStatementWithBlock : AStatement
{
    protected AStatement _block;

    public AStatement Block
    {
        get => _block;
        set
        {
            if (Equals(value, _block)) return;
            _block = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }
}