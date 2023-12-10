using System.Windows;
using Aktenschrank.Desktop.ViewModel;
using Aktenschrank.Desktop.Utils;

namespace Aktenschrank.Desktop.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _mainWindowViewModel = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _mainWindowViewModel;


            FmpOv_TbNewProfileName.PreviewTextInput += InputHelper.CheckIfTextIsAllowed_PreviewTextInput;

            FmpDt_TbName.PreviewTextInput += InputHelper.CheckIfNameIsAllowed_PreviewTextInput;
            FmpDt_TbDescription.PreviewTextInput += InputHelper.CheckIfTextIsAllowed_PreviewTextInput;


            FmpOv_TbNewProfileName.KeyUp += InputHelper.UpdateTextBinding;

            FmpDt_TbName.KeyUp += InputHelper.UpdateTextBinding;
            FmpDt_TbDescription.KeyUp += InputHelper.UpdateTextBinding;


            DataObject.AddPastingHandler(FmpOv_TbNewProfileName, InputHelper.OnPaste);

            DataObject.AddPastingHandler(FmpDt_TbName, InputHelper.OnPaste);
            DataObject.AddPastingHandler(FmpDt_TbDescription, InputHelper.OnPaste);
        }
    }
}