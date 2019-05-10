using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class CellViewModel : BindableBase
    {
        private readonly int _height = 40;
        private readonly int _width = 40;

        public int Height
        {
            get { return _height; }
        }

        public int Width
        {
            get { return _width; }
        }
    }
}
