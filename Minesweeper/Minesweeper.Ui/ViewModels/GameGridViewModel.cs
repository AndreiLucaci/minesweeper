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
        private IWorldManager _worldManager;
        private bool _isFirstMove = true;

        public ObservableCollection<CellViewModel> Cells
        {
            get { return _cells; }
            set { SetProperty(ref _cells, value, nameof(Cells)); }
        }

        public int GameWidth
        {
            get { return GameConfiguration.Width * GameConstants.GameViewWidth; }
        }

        public int GameHeight
        {
            get { return GameConfiguration.Height * GameConstants.GameViewHeight; }
        }

        public GameConfiguration GameConfiguration { get; set; }

        public GameGridViewModel(IGameConfigurationService gameConfigurationService, IEventAggregator eventAggregator)
        {
            Guard.ArgumentNotNull(gameConfigurationService, nameof(gameConfigurationService));
            Guard.ArgumentNotNull(eventAggregator, nameof(eventAggregator));

            _eventAggregator = eventAggregator;

            var configurationService = gameConfigurationService;
            GameConfiguration = configurationService.DefaultConfiguration;

            StartNewGame();

            SubscribeToEvents();
        }

        private void StartNewGame()
        {
            _isFirstMove = true;
            _worldManager = new WorldManager(GameConfiguration);
            InitializeCells();
            NotifyView();

            RedrawWorld(true);
        }

        private void NotifyView()
        {
            RaisePropertyChanged(nameof(GameWidth));
            RaisePropertyChanged(nameof(GameHeight));

            _eventAggregator.GetEvent<ResizeWidthEvent>().Publish(GameWidth);
            _eventAggregator.GetEvent<ResizeHeightEvent>().Publish(GameHeight);
        }

        private void OnStartNewGame(GameConfiguration gameConfiguration)
        {
            GameConfiguration = gameConfiguration;

            StartNewGame();
        }

        ~GameGridViewModel()
        {
            UnsubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _eventAggregator.GetEvent<CellClickEvent>().Subscribe(OnCellClicked);
            _eventAggregator.GetEvent<CellFlagEvent>().Subscribe(OnCellFlagged);
            _eventAggregator.GetEvent<StartNewGameEvent>().Subscribe(OnStartNewGame);
        }

        private void UnsubscribeToEvents()
        {
            _eventAggregator.GetEvent<CellClickEvent>().Unsubscribe(OnCellClicked);
            _eventAggregator.GetEvent<CellFlagEvent>().Unsubscribe(OnCellFlagged);
            _eventAggregator.GetEvent<StartNewGameEvent>().Unsubscribe(OnStartNewGame);
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
            if (_isFirstMove)
            {
                if (cell.CellType == CellType.Mine || cell.ComputeNumberOfMines() != 0)
                {
                    _worldManager.ReorganizeCells(cell);
                }

                _isFirstMove = false;
            }

            _worldManager.OpenCell(cell);

            RedrawWorld();
        }

        private void OnCellFlagged(Cell cell)
        {
            _worldManager.FlagCell(cell);

            GetCellViewModel(cell).OnCellRedrawn();
        }

        private void RedrawWorld(bool forceRedraw = false)
        {
            var cells = forceRedraw ? _worldManager.Cells : _worldManager.Cells.Where(x => x.IsDirty);

            foreach (var worldManagerCell in cells)
            {
                GetCellViewModel(worldManagerCell).OnCellRedrawn();
            }

            _worldManager.ResetDirty();
        }

        private CellViewModel GetCellViewModel(Cell cell)
        {
            return Cells.Single(x => x.Cell.Equals(cell));
        }
    }
}
