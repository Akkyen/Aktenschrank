using Aktenschrank.Model.Enums;

namespace Aktenschrank.Model;

public class Action : Statement
{
    private ActionType _actionType;

    public ActionType ActionType
    {
        get => _actionType;
        set
        {
            if (value == _actionType) return;
            _actionType = value;
            OnPropertyChanged();
        }
    }

    public new object Clone()
    {
        Action rValue = (Action)base.Clone();

        rValue.ActionType = this.ActionType;

        return rValue;
    }
}