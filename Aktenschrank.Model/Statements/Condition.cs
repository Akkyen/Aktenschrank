using System.Collections.ObjectModel;
using Aktenschrank.Model.Enums;

namespace Aktenschrank.Model.Statements;

public class Condition : AStatementWithBlock
{
    private ConditionType _conditionType = ConditionType.If;

    private ObservableCollection<Check> _checks = new();

    private AStatement _body;

    public Condition(AStatement predecessor)
    {
        _predecessor = predecessor;
    }

    public ConditionType ConditionType
    {
        get => _conditionType;
        set
        {
            if (value == _conditionType) return;
            _conditionType = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Check> Checks
    {
        get => _checks;
        set
        {
            if (Equals(value, _checks)) return;
            _checks = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public AStatement Body
    {
        get => _body;
        set
        {
            if (Equals(value, _body)) return;
            _body = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }
}