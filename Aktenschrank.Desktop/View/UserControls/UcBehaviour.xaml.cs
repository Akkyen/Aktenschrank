using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Aktenschrank.Model;

namespace Aktenschrank.Desktop.View.UserControls
{
    /// <summary>
    /// Interaktionslogik für UcBehaviour.xaml
    /// </summary>
    public partial class UcBehaviour : UserControl, INotifyPropertyChanged
    {
        private Behaviour _behaviour;

        private static readonly DependencyProperty BehaviourProperty = DependencyProperty.Register("Behaviour", typeof(Behaviour), typeof(UcBehaviour));

        public UcBehaviour()
        {
            InitializeComponent();

            DataContext = this;
        }

        public Behaviour Behaviour
        {
            get => _behaviour;
            set
            {
                if (Equals(value, _behaviour)) return;
                _behaviour = value;
                OnPropertyChanged();
            }
        }

        private void UpdateTextBinding(object sender, KeyEventArgs e)
        {
            if (e.OriginalSource is TextBox textBox)
            {
                BindingExpression binding = textBox.GetBindingExpression(TextBox.TextProperty);
                binding?.UpdateSource();
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
