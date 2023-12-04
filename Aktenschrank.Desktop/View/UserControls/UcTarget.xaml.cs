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
    /// Interaktionslogik für UcTarget.xaml
    /// </summary>
    public partial class UcTarget : UserControl, INotifyPropertyChanged
    {
        private Target _target;

        private static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(Target), typeof(UcTarget));


        public UcTarget()
        {
            InitializeComponent();

            DataContext = this;
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
