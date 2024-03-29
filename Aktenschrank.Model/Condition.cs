﻿using System.Collections.ObjectModel;
using Aktenschrank.Model.Enums;

namespace Aktenschrank.Model;

public class Condition : Statement
{
    private ConditionType _conditionType;
    private ConditionComparatorType _conditionComparatorType;

    private ObservableCollection<Statement> _body = new();

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

    public ConditionComparatorType ConditionComparatorType
    {
        get => _conditionComparatorType;
        set
        {
            if (value == _conditionComparatorType) return;
            _conditionComparatorType = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Statement> Body
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