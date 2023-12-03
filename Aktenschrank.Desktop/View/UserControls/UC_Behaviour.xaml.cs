using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using Aktenschrank.Model;

namespace Aktenschrank.Desktop.View.UserControls
{
    /// <summary>
    /// Interaktionslogik für UC_Behaviour.xaml
    /// </summary>
    public partial class UC_Behaviour : UserControl, INotifyPropertyChanged
    {
        private Behaviour _ucBehaviour;
        public UC_Behaviour(Behaviour ucBehaviour)
        {
            _ucBehaviour = ucBehaviour;

            InitializeComponent();
        }

        public Behaviour UcBehaviour
        {
            get => _ucBehaviour;
            set
            {
                if (Equals(value, _ucBehaviour)) return;
                _ucBehaviour = value ?? throw new ArgumentNullException(nameof(value));
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
