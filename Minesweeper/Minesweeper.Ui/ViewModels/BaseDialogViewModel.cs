using System.Windows;
using System.Windows.Media.Imaging;
using Minesweeper.Ui.Constants;
using Prism.Commands;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class BaseDialogViewModel : BindableBase
    {
        private BitmapImage _logo;

        public BaseDialogViewModel()
        {
            CloseWindow = new DelegateCommand<Window>(w => w.Close());

            Logo = BaseStyles.SurprisedFace;
        }

        public DelegateCommand<Window> CloseWindow { get; }

        public BitmapImage Logo
        {
            get => _logo;
            set => SetProperty(ref _logo, value, nameof(Logo));
        }
    }
}
