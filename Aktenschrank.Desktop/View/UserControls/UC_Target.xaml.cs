using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Aktenschrank.Model;

namespace Aktenschrank.Desktop.View.UserControls
{
    /// <summary>
    /// Interaktionslogik für UC_Target.xaml
    /// </summary>
    public partial class UC_Target : UserControl, INotifyPropertyChanged
    {
        private Target _target;

        public UC_Target(Target target)
        {
            _target = target;

            InitializeComponent();


        }

        public Target Target
        {
            get => _target;
            set
            {
                if (Equals(value, _target)) return;
                _target = value ?? throw new ArgumentNullException(nameof(value));
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
    }
}
