using System.Windows;
using Aktenschrank.Model;
using Aktenschrank.Desktop.Utils;

namespace Aktenschrank.Desktop.View.UserControls
{
    /// <summary>
    /// Interaktionslogik für UcBehaviour.xaml
    /// </summary>
    public partial class UcBehaviour
    {
        private Behaviour _behaviour;

        public UcBehaviour()
        {
            InitializeComponent();

            BhDt_TbName.PreviewTextInput += InputHelper.CheckIfNameIsAllowed_PreviewTextInput;
            BhDt_TbDescription.PreviewTextInput += InputHelper.CheckIfTextIsAllowed_PreviewTextInput;

            BhDt_TbName.KeyUp += InputHelper.UpdateTextBinding;
            BhDt_TbDescription.KeyUp += InputHelper.UpdateTextBinding;

            DataObject.AddPastingHandler(BhDt_TbName, InputHelper.OnPaste);
            DataObject.AddPastingHandler(BhDt_TbDescription, InputHelper.OnPaste);
        }
    }
}
