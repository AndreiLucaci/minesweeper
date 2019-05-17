using System.Windows;
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
		private readonly IGameConfigurationService _gameConfigurationService;
		private readonly IEventAggregator _eventAggregator;
		private const int ExtraSpace = 100;
		private int _width;
		private int _height;

		public int Width
		{
			get { return _width; }
			set { SetProperty(ref _width, value, nameof(Width)); }
		}

		public int Height
		{
			get { return _height; }
			set { SetProperty(ref _height, value, nameof(Height)); }
		}

		public GameWindowViewModel(IGameConfigurationService gameConfigurationService, IEventAggregator eventAggregator)
		{
			Guard.ArgumentNotNull(gameConfigurationService, nameof(gameConfigurationService));
			Guard.ArgumentNotNull(eventAggregator, nameof(eventAggregator));

			_gameConfigurationService = gameConfigurationService;
			_eventAggregator = eventAggregator;

			BeginnerNewGameCommand = new DelegateCommand(() => StartNewGame(gameConfigurationService.BeginnerConfiguration));
			AdvancedNewGameCommand = new DelegateCommand(() => StartNewGame(gameConfigurationService.AdvancedConfiguration));
			ExpertNewGameCommand = new DelegateCommand(() => StartNewGame(gameConfigurationService.ExpertConfiguration));

			SetUpInitialWindowSize();

			SubscribeEvents();
		}

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
			{
				Application.Current.MainWindow.Width = width + ExtraSpace;
			}
		}

		private void OnResizeHeightEvent(int height)
		{
			if (Application.Current.MainWindow != null)
			{
				Application.Current.MainWindow.Height = height + ExtraSpace + 100;
			}
		}

		public DelegateCommand BeginnerNewGameCommand { get; }
		public DelegateCommand AdvancedNewGameCommand { get; }
		public DelegateCommand ExpertNewGameCommand { get; }

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
