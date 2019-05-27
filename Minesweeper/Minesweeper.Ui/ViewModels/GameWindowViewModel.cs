using System.Windows;
using System.Windows.Threading;
using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameWindowViewModel : BindableBase
    {
        private const int ExtraSpace = 20;
        private readonly IEventAggregator _eventAggregator;
        private readonly IGameConfigurationService _gameConfigurationService;
        private int _height;
        private int _width;

        public GameWindowViewModel(IGameConfigurationService gameConfigurationService, IEventAggregator eventAggregator)
        {
            Guard.ArgumentNotNull(gameConfigurationService, nameof(gameConfigurationService));
            Guard.ArgumentNotNull(eventAggregator, nameof(eventAggregator));

            _gameConfigurationService = gameConfigurationService;
            _eventAggregator = eventAggregator;

            BeginnerNewGameCommand =
                new DelegateCommand(() => StartNewGame(gameConfigurationService.BeginnerConfiguration));
            AdvancedNewGameCommand =
                new DelegateCommand(() => StartNewGame(gameConfigurationService.AdvancedConfiguration));
            ExpertNewGameCommand =
                new DelegateCommand(() => StartNewGame(gameConfigurationService.ExpertConfiguration));

            SetUpInitialWindowSize();

            SubscribeEvents();
        }

        public int Width
        {
            get => _width;
            set => SetProperty(ref _width, value, nameof(Width));
        }

        public int Height
        {
            get => _height;
            set => SetProperty(ref _height, value, nameof(Height));
        }

        public DelegateCommand BeginnerNewGameCommand { get; }
        public DelegateCommand AdvancedNewGameCommand { get; }
        public DelegateCommand ExpertNewGameCommand { get; }

        ~GameWindowViewModel()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<ResizeWidthEvent>().Subscribe(OnResizeWidthEvent);
            _eventAggregator.GetEvent<ResizeHeightEvent>().Subscribe(OnResizeHeightEvent);
        }

        private void UnsubscribeEvents()
        {
            _eventAggregator.GetEvent<ResizeWidthEvent>().Unsubscribe(OnResizeWidthEvent);
            _eventAggregator.GetEvent<ResizeHeightEvent>().Unsubscribe(OnResizeHeightEvent);
        }

        private void OnResizeWidthEvent(int width)
        {
            if (Application.Current.MainWindow != null)
                Dispatcher.CurrentDispatcher.Invoke(() => Application.Current.MainWindow.Width = width + ExtraSpace,
                    DispatcherPriority.Render);
        }

        private void OnResizeHeightEvent(int height)
        {
            if (Application.Current.MainWindow != null)
                Dispatcher.CurrentDispatcher.Invoke(
                    () => Application.Current.MainWindow.Height = height + ExtraSpace + 90, DispatcherPriority.Render);
        }

        private void StartNewGame(GameConfiguration gameConfiguration)
        {
            _eventAggregator.GetEvent<StartNewGameEvent>().Publish(gameConfiguration);
        }

        private void SetUpInitialWindowSize()
        {
            Width = _gameConfigurationService.DefaultConfiguration.Width * GameConstants.GameViewWidth;
            Height = _gameConfigurationService.DefaultConfiguration.Height * GameConstants.GameViewHeight;

            OnResizeWidthEvent(Width);
            OnResizeHeightEvent(Height);
        }
    }
}