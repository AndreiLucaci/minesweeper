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

        private bool _isEndGame = false;

        public ObservableCollection<CellViewModel> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value, nameof(Cells));
        }

        public int GameWidth => GameConfiguration.Width * GameConstants.GameViewWidth;

        public int GameHeight => GameConfiguration.Height * GameConstants.GameViewHeight;

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
            _isEndGame = false;

            _worldManager = new WorldManager(GameConfiguration);

            InitializeCells();
            NotifyView();

            RedrawWorld(true);

            _eventAggregator.GetEvent<ResetGameEvent>().Publish();
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
            _eventAggregator.GetEvent<RestartGameEvent>().Subscribe(OnRestartGame);
        }

        private void UnsubscribeToEvents()
        {
            _eventAggregator.GetEvent<CellClickEvent>().Unsubscribe(OnCellClicked);
            _eventAggregator.GetEvent<CellFlagEvent>().Unsubscribe(OnCellFlagged);
            _eventAggregator.GetEvent<StartNewGameEvent>().Unsubscribe(OnStartNewGame);
            _eventAggregator.GetEvent<RestartGameEvent>().Unsubscribe(OnRestartGame);
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
            if (_isEndGame) return;

            if (_isFirstMove)
            {
                if (cell.CellType == CellType.Mine || cell.ComputeNumberOfMines() != 0)
                {
                    _worldManager.ReorganizeCells(cell);
                }

                _isFirstMove = false;

                _eventAggregator.GetEvent<StartTimerEvent>().Publish();
            }

            var result = _worldManager.OpenCell(cell);

            RedrawWorld();

            ProcessGameState(result);
        }

        private void ProcessGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Advance:
                    break;
                case GameState.GameOver:
                    _eventAggregator.GetEvent<GameMineExplodedEvent>().Publish();
                    _eventAggregator.GetEvent<StopTimerEvent>().Publish();
                    _isEndGame = true;
                    break;
                case GameState.EndGame:
                    _eventAggregator.GetEvent<GameWinEvent>().Publish();
                    _eventAggregator.GetEvent<StopTimerEvent>().Publish();
                    _isEndGame = true;
                    break;
            }
        }

        private void OnRestartGame()
        {
            StartNewGame();
        }

        private void OnCellFlagged(Cell cell)
        {
            if (_isEndGame) return;

            _worldManager.FlagCell(cell);

            _eventAggregator.GetEvent<UpdateMinesNumberEvent>().Publish(cell.CellState == CellState.FlaggedAsMine);

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
