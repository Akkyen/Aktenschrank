using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aktenschrank.Model
{
    public class Behaviour : INotifyPropertyChanged
    {
        private string _name;
        private string _description;

        private ObservableCollection<Action> _actions;

        private bool _runInService;

        public Behaviour(string name)
        {
            _name = name;

            Actions = new ObservableCollection<Action>();
        }

        public Behaviour(string name, string description)
        {
            Name = name;
            Description = description;

            Actions = new ObservableCollection<Action>();
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

        public ObservableCollection<Action> Actions
        {
            get => _actions;
            set
            {
                if (Equals(value, _actions)) return;
                _actions = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged();
            }
        }

        public bool RunInService
        {
            get => _runInService;
            set
            {
                if (value == _runInService) return;
                _runInService = value;
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

        protected bool Equals(Behaviour other)
        {
            return _name == other._name;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Behaviour)obj);
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}