using Minesweeper.Models;
using Prism.Commands;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class CellViewModel : BindableBase
    {
		private readonly int _height = 20;
        private readonly int _width = 20;
	    private Point _position;

	    public int Height
        {
            get { return _height; }
        }

        public int Width
        {
            get { return _width; }
        }

	    public Point Position
	    {
		    get { return _position; }
		    set { SetProperty(ref _position, value, nameof(Position)); }
	    }

		public DelegateCommand ClickCommand { get; set; }

	    public CellViewModel()
	    {
		    ClickCommand = new DelegateCommand(OnClick);
	    }

	    private void OnClick()
	    {
		    
	    }
    }
}
