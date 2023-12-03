using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aktenschrank.Model;

public class SortingProfile : INotifyPropertyChanged, ICloneable
{
    private string _name = string.Empty;
    private string _description = string.Empty;

    private bool _enabled;
    private bool _autoRun;

    private ObservableSet<Behaviour> _behaviours = new();

    private ObservableSet<Target> _targets = new();

    public SortingProfile()
    {
    }

    public SortingProfile(string name)
    {
        Name = name;
    }

    public bool AddTarget(string folderPath)
    {
        return Targets.Add(new Target(folderPath));
    }

    public bool RemoveTarget(string folderPath)
    {
        Target toRemove = null!;

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
        Behaviour toRemove = null!;

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

    public bool AutoRun
    {
        get => _autoRun;
        set
        {
            if (value == _autoRun) return;
            _autoRun = value;
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

    public override string ToString()
    {
        return Name;
    }

    public object Clone()
    {
        SortingProfile rValue = new SortingProfile(_name + "_Duplicate");

        rValue.Description = _description;
        rValue.Enabled = _enabled;
        rValue.AutoRun = _autoRun;

        foreach (Behaviour behaviour in _behaviours)
        {
            rValue.Behaviours.Add((Behaviour)behaviour.Clone());
        }

        foreach (Target target in _targets)
        {
            rValue.Targets.Add((Target)target.Clone());
        }

        return rValue;
    }

    protected bool Equals(SortingProfile other)
    {
        return _name == other._name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((SortingProfile)obj);
    }

    public override int GetHashCode()
    {
        return _name.GetHashCode();
    }
}