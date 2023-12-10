using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Aktenschrank.Model;

public class FileManagingProfile : ObservableObject, ICloneable
{
    private Guid _id = Guid.NewGuid();

    private string _name = string.Empty;
    private string _description = string.Empty;

    private bool _enabled;
    private bool _autoRun;
    private bool _autonomous;

    private ObservableCollection<Behaviour> _behaviours = new();

    private ObservableCollection<Target> _targets = new();

    public FileManagingProfile()
    {
    }

    public FileManagingProfile(string name)
    {
        Name = name;
    }

    public void AddTarget(string folderPath)
    {
        Targets.Add(new Target(folderPath));
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

    public void AddBehaviour()
    {
        Behaviours.Add(new Behaviour("Behaviour" + Behaviours.Count));
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
            _name = value;
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

    public bool Autonomous
    {
        get => _autonomous;
        set
        {
            if (value == _autonomous) return;
            _autonomous = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Behaviour> Behaviours
    {
        get => _behaviours;
        set
        {
            if (Equals(value, _behaviours)) return;
            _behaviours = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Target> Targets
    {
        get => _targets;
        set
        {
            if (Equals(value, _targets)) return;
            _targets = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public override string ToString()
    {
        return Name;
    }

    public object Clone()
    {
        FileManagingProfile rValue = new FileManagingProfile(_name);

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

    protected bool Equals(FileManagingProfile other)
    {
        return _id.Equals(other._id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((FileManagingProfile)obj);
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }
}