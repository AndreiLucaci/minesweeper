using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Events;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameMinesViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private GameConfiguration _gameConfiguration;

        private int _numberOfMines;
        private BitmapImage _slot1Image;
        private BitmapImage _slot2Image;

        public GameMinesViewModel(IGameConfigurationService gameConfigurationService, IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _gameConfiguration = gameConfigurationService.DefaultConfiguration;
            _numberOfMines = _gameConfiguration.NumberOfMines;

            Redraw();

            SubscribeEvents();
        }

        public BitmapImage Slot2Image
        {
            get => _slot2Image;
            set => SetProperty(ref _slot2Image, value, nameof(Slot2Image));
        }

        public BitmapImage Slot1Image
        {
            get => _slot1Image;
            set => SetProperty(ref _slot1Image, value, nameof(Slot1Image));
        }

        ~GameMinesViewModel()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<UpdateMinesNumberEvent>().Subscribe(OnUpdateMinesNumber);
            _eventAggregator.GetEvent<RestartGameEvent>().Subscribe(OnRestartGame);
            _eventAggregator.GetEvent<StartNewGameEvent>().Subscribe(OnStartNewGame);
            _eventAggregator.GetEvent<SkinChangedEvent>().Subscribe(OnSkinChanged);
        }

        private void UnsubscribeEvents()
        {
            _eventAggregator.GetEvent<UpdateMinesNumberEvent>().Unsubscribe(OnUpdateMinesNumber);
            _eventAggregator.GetEvent<RestartGameEvent>().Unsubscribe(OnRestartGame);
            _eventAggregator.GetEvent<StartNewGameEvent>().Unsubscribe(OnStartNewGame);
            _eventAggregator.GetEvent<SkinChangedEvent>().Unsubscribe(OnSkinChanged);
        }

        private void OnSkinChanged()
        {
            Redraw();
        }

        private void OnRestartGame()
        {
            _numberOfMines = _gameConfiguration.NumberOfMines;

            Redraw();
        }

        private void OnStartNewGame(GameConfiguration gameConfiguration)
        {
            _gameConfiguration = gameConfiguration;
            _numberOfMines = _gameConfiguration.NumberOfMines;

            Redraw();
        }

        private void OnUpdateMinesNumber(bool decrement)
        {
            if (decrement) _numberOfMines--;
            else _numberOfMines++;

            Redraw();
        }

        private IEnumerable<int> GetDigits()
        {
            return _numberOfMines >= 0 ? _numberOfMines.ToString().Select(x => int.Parse(x.ToString())) : new[] {0};
        }

        private void Redraw()
        {
            var digits = GetDigits().ToList();
            digits.Reverse();

            for (var i = 0; i < digits.Count; i++)
            {
                var digit = digits[i];
                switch (i)
                {
                    case 0:
                        RedrawSlot(1, digit);
                        break;
                    case 1:
                        RedrawSlot(2, digit);
                        break;
                }
            }
            if (digits.Count == 1)
                RedrawSlot(2, 0);
        }

        private void RedrawSlot(int slot, int digit)
        {
            switch (digit)
            {
                case 0:
                    RedrawSlot(slot, DigitStyles.Tile0);
                    break;
                case 1:
                    RedrawSlot(slot, DigitStyles.Tile1);
                    break;
                case 2:
                    RedrawSlot(slot, DigitStyles.Tile2);
                    break;
                case 3:
                    RedrawSlot(slot, DigitStyles.Tile3);
                    break;
                case 4:
                    RedrawSlot(slot, DigitStyles.Tile4);
                    break;
                case 5:
                    RedrawSlot(slot, DigitStyles.Tile5);
                    break;
                case 6:
                    RedrawSlot(slot, DigitStyles.Tile6);
                    break;
                case 7:
                    RedrawSlot(slot, DigitStyles.Tile7);
                    break;
                case 8:
                    RedrawSlot(slot, DigitStyles.Tile8);
                    break;
                case 9:
                    RedrawSlot(slot, DigitStyles.Tile9);
                    break;
            }
        }

        private void RedrawSlot(int slot, BitmapImage tile)
        {
            switch (slot)
            {
                case 1:
                    Slot1Image = tile;
                    break;
                case 2:
                    Slot2Image = tile;
                    break;
            }
        }
    }
}