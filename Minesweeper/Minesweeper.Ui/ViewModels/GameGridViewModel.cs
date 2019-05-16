using System.Collections.ObjectModel;
using System.Linq;
using Minesweeper.Engine;
using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Models;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Events;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameGridViewModel : BindableBase
    {
	    private ObservableCollection<CellViewModel> _cells = new ObservableCollection<CellViewModel>();

		private readonly IEventAggregator _eventAggregator;
	    private readonly IWorldManager _worldManager;

	    public ObservableCollection<CellViewModel> Cells
	    {
		    get { return _cells; }
		    set { SetProperty(ref _cells, value, nameof(Cells)); }
	    }

		public int GameWidth => GameConfiguration.Width * GameConstants.GameViewWidth;

	    public int GameHeight => GameConfiguration.Height * GameConstants.GameViewHeight;

	    public GameConfiguration GameConfiguration { get; }

		public GameGridViewModel(IGameConfigurationService gameConfigurationService, IEventAggregator eventAggregator)
        {
	        _eventAggregator = eventAggregator;
	        Guard.ArgumentNotNull(gameConfigurationService, nameof(gameConfigurationService));

            var configurationService = gameConfigurationService;
	        GameConfiguration = configurationService.BeginnerConfiguration;

			_worldManager = new WorldManager(configurationService.BeginnerConfiguration);

	        SubscribeToEvents();

			InitializeCells();

	        RedrawWorld();
        }

	    ~GameGridViewModel()
	    {
		    UnsubscribeToEvents();
	    }

	    private void SubscribeToEvents()
	    {
		    _eventAggregator.GetEvent<CellClickEvent>().Subscribe(OnCellClicked);
	    }

	    private void UnsubscribeToEvents()
	    {
		    _eventAggregator.GetEvent<CellClickEvent>().Unsubscribe(OnCellClicked);
	    }

		private void InitializeCells()
	    {
			_worldManager.InitializeWorld();

			Cells = new ObservableCollection<CellViewModel>(_worldManager.Cells.Select(x => new CellViewModel
			{
				Cell = x
			}));
	    }

	    private void OnCellClicked(Cell cell)
	    {
		    _worldManager.OpenCell(cell);

		    RedrawWorld();
	    }

	    private void RedrawWorld()
	    {
		    foreach (var worldManagerCell in _worldManager.Cells)
		    {
			    _eventAggregator.GetEvent<CellRedrawEvent>().Publish(worldManagerCell);
		    }
	    }
    }
}
