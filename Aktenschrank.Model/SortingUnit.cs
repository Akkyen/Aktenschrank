using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aktenschrank.Model;

public class SortingUnit : INotifyPropertyChanged
{
    private string _name = string.Empty;
    private string _description = string.Empty;

    private bool _enabled;
    private bool _runAsService;

    private ObservableSet<Behaviour> _behaviours = new();

    private ObservableSet<Target> _targets = new();

    public SortingUnit()
    {
    }

    public SortingUnit(string name)
    {
        Name = name;
    }

    public bool AddTarget(string folderPath)
    {
        return Targets.Add(new Target(folderPath));
    }

    public bool RemoveTarget(string folderPath)
    {
        Target? toRemove = null;

        foreach (Target target in Targets)
        {
            if (target.FolderPath.Equals(folderPath))
            {
                toRemove = target;
                break;
            }
        }

        return Targets.Remove(toRemove);
    }

    public bool RemoveTarget(Target target)
    {
        return Targets.Remove(target);
    }

    public bool AddBehaviour()
    {
        return Behaviours.Add(new Behaviour("Behaviour" + Behaviours.Count));
    }

    public bool RemoveBehaviour(Behaviour behaviour)
    {
        return Behaviours.Remove(behaviour);
    }

    public bool RemoveBehaviour(string name)
    {
        Behaviour? toRemove = null;

        foreach (Behaviour behaviour in Behaviours)
        {
            if (behaviour.Name.Equals(name))
            {
                toRemove = behaviour;
                break;
            }
        }

        return Behaviours.Remove(toRemove);
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value == _description) return;
            _description = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (value == _enabled) return;
            _enabled = value;
            OnPropertyChanged();
        }
    }

    public bool RunAsService
    {
        get => _runAsService;
        set
        {
            if (value == _runAsService) return;
            _runAsService = value;
            OnPropertyChanged();
        }
    }

    public ObservableSet<Behaviour> Behaviours
    {
        get => _behaviours;
        set
        {
            if (Equals(value, _behaviours)) return;
            _behaviours = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public ObservableSet<Target> Targets
    {
        get => _targets;
        set
        {
            if (Equals(value, _targets)) return;
            _targets = value ?? throw new ArgumentNullException(nameof(value));
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

    public override string ToString()
    {
        return Name;
    }

    protected bool Equals(SortingUnit other)
    {
        return _name == other._name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SortingUnit)obj);
    }

    public override int GetHashCode()
    {
        return _name.GetHashCode();
    }
}