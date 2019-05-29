using System.Windows.Media.Imaging;
using Minesweeper.Infrastructure;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Events;
using Minesweeper.Ui.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameFaceViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private BitmapImage _faceImage;
        private FaceType _currentFace;

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
            _eventAggregator.GetEvent<SkinChangedEvent>().Subscribe(OnSkinChanged);
        }

        private void UnsubscribeEvents()
        {
            _eventAggregator.GetEvent<StartNewGameEvent>().Unsubscribe(OnStartNewGame);
            _eventAggregator.GetEvent<GameWinEvent>().Unsubscribe(DrawWin);
            _eventAggregator.GetEvent<GameMineExplodedEvent>().Unsubscribe(DrawDead);
            _eventAggregator.GetEvent<SkinChangedEvent>().Unsubscribe(OnSkinChanged);
        }

        private void OnSkinChanged()
        {
            Redraw();
        }

        private void OnStartNewGame(GameConfiguration obj)
        {
            DrawSmile();
        }

        private void DrawDead()
        {
            FaceImage = FaceStyles.FaceDead;
            _currentFace = FaceType.Dead;
        }

        private void DrawWin()
        {
            FaceImage = FaceStyles.FaceWin;
            _currentFace = FaceType.Win;
        }

        private void DrawSmile()
        {
            FaceImage = FaceStyles.FaceSmile;
            _currentFace = FaceType.Smile;
        }

        private void Redraw()
        {
            switch (_currentFace)
            {
                case FaceType.Dead:
                    DrawDead();
                    break;
                case FaceType.Smile:
                    DrawSmile();
                    break;
                case FaceType.Win:
                    DrawWin();
                    break;
            }
        }
    }
}