using System.Collections.ObjectModel;
using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Models;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameGridViewModel : BindableBase
    {
        private readonly IGameConfigurationService _gameConfigurationService;

        private readonly int _multiplier = 20;
	    private ObservableCollection<CellViewModel> _cells = new ObservableCollection<CellViewModel>();

	    public GameGridViewModel(IGameConfigurationService gameConfigurationService)
        {
            Guard.ArgumentNotNull(gameConfigurationService, nameof(gameConfigurationService));

            _gameConfigurationService = gameConfigurationService;

            GameConfiguration = _gameConfigurationService.BeginnerConfiguration;

	        InitializeCells();
        }

	    public ObservableCollection<CellViewModel> Cells
	    {
		    get { return _cells; }
		    set { SetProperty(ref _cells, value, nameof(Cells)); }
	    }

	    public int GameWidth => GameConfiguration.Width * _multiplier;

        public int GameHeight => GameConfiguration.Height * _multiplier;

        public GameConfiguration GameConfiguration { get; }

	    private void InitializeCells()
	    {
		    for (var i = 0; i < GameConfiguration.Width; i++)
		    {
			    for (var j = 0; j < GameConfiguration.Height; j++)
			    {
				    var cell = new CellViewModel
				    {
					    Position = new Point(i, j)
				    };

					Cells.Add(cell);
			    }
		    }
	    }
	}
}
