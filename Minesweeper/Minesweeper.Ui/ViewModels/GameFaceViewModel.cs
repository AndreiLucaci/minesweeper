using System.Windows.Media.Imaging;
using Minesweeper.Infrastructure;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameFaceViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private BitmapImage _faceImage;

        public GameFaceViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            StartNewGameCommand = new DelegateCommand(OnStartNewGameCommand);

            SubscribeEvents();

            DrawSmile();
        }

        public DelegateCommand StartNewGameCommand { get; }
        public DelegateCommand AmazedFaceCommand { get; }

        public BitmapImage FaceImage
        {
            get => _faceImage;
            set => SetProperty(ref _faceImage, value, nameof(FaceImage));
        }

        ~GameFaceViewModel()
        {
            UnsubscribeEvents();
        }

        private void OnStartNewGameCommand()
        {
            DrawSmile();
            _eventAggregator.GetEvent<RestartGameEvent>().Publish();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<StartNewGameEvent>().Subscribe(OnStartNewGame);
            _eventAggregator.GetEvent<GameWinEvent>().Subscribe(DrawWin);
            _eventAggregator.GetEvent<GameMineExplodedEvent>().Subscribe(DrawDead);
        }

        private void UnsubscribeEvents()
        {
            _eventAggregator.GetEvent<StartNewGameEvent>().Unsubscribe(OnStartNewGame);
            _eventAggregator.GetEvent<GameWinEvent>().Unsubscribe(DrawWin);
            _eventAggregator.GetEvent<GameMineExplodedEvent>().Unsubscribe(DrawDead);
        }

        private void OnStartNewGame(GameConfiguration obj)
        {
            DrawSmile();
        }

        private void DrawDead()
        {
            FaceImage = FaceStyles.FaceDead;
        }

        private void DrawWin()
        {
            FaceImage = FaceStyles.FaceWin;
        }

        private void DrawSmile()
        {
            FaceImage = FaceStyles.FaceSmile;
        }
    }
}