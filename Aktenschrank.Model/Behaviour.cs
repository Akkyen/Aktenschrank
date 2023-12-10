using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Aktenschrank.Model
{
    public class Behaviour : INotifyPropertyChanged, ICloneable
    {
        private Guid _id = Guid.NewGuid();

        private string _name;
        private string _description;

        private AStatement _rootStatement;
        private ObservableCollection<AStatement> _statements = new();

        private bool _enabled;
        private bool _autorun;

        public Behaviour(string name)
        {
            _name = name;
        }

        public Behaviour(string name, string description, bool enabled, bool autorun)
        {
            _name = name;
            _description = description;

            _enabled = enabled;
            _autorun = autorun;
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

        public ObservableCollection<AStatement> Statements
        {
            get => _statements;
            set
            {
                if (Equals(value, _statements)) return;
                _statements = value ?? throw new ArgumentNullException(nameof(value));
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

        public bool Autorun
        {
            get => _autorun;
            set
            {
                if (value == _autorun) return;
                _autorun = value;
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

        public object Clone()
        {
            Behaviour rValue = new Behaviour(_name, _description, _enabled, _autorun);

            foreach (AStatement statement in _statements)
            {
                rValue._statements.Add((AStatement)statement.Clone());
            }

            return rValue;
        }

        protected bool Equals(Behaviour other)
        {
            return _id.Equals(other._id);
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
            return _id.GetHashCode();
        }
    }
}