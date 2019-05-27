using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Events;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameTimerViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly DispatcherTimer _timer;

        private int _counter = 0;
        private BitmapImage _slot1Image;
        private BitmapImage _slot2Image;
        private BitmapImage _slot3Image;

        public BitmapImage Slot3Image
        {
            get => _slot3Image;
            set => SetProperty(ref _slot3Image, value, nameof(Slot3Image));
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

        public GameTimerViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            SubscribeEvents();

            DrawInitial();
        }

        ~GameTimerViewModel()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _eventAggregator.GetEvent<StartTimerEvent>().Subscribe(OnStartTimer);
            _eventAggregator.GetEvent<StopTimerEvent>().Subscribe(OnStopTimer);
            _eventAggregator.GetEvent<ResetGameEvent>().Subscribe(OnResetGame);

            _timer.Tick += OnTimerTick;
        }

        private void UnsubscribeEvents()
        {
            _eventAggregator.GetEvent<StartTimerEvent>().Unsubscribe(OnStartTimer);
            _eventAggregator.GetEvent<StopTimerEvent>().Unsubscribe(OnStopTimer);
            _eventAggregator.GetEvent<ResetGameEvent>().Unsubscribe(OnResetGame);
        }

        private void OnTimerTick(object sender, EventArgs eventArgs)
        {
            _counter++;

            Redraw();
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
                        RedrawSlot1(digit);
                        break;
                    case 1:
                        RedrawSlot2(digit);
                        break;
                    case 2:
                        RedrawSlot3(digit);
                        break;
                }
            }
        }

        private void RedrawSlot1(int digit)
        {
            switch (digit)
            {
                case 0:
                    Slot1Image = DigitStyles.Tile0;
                    break;
                case 1:
                    Slot1Image = DigitStyles.Tile1;
                    break;
                case 2:
                    Slot1Image = DigitStyles.Tile2;
                    break;
                case 3:
                    Slot1Image = DigitStyles.Tile3;
                    break;
                case 4:
                    Slot1Image = DigitStyles.Tile4;
                    break;
                case 5:
                    Slot1Image = DigitStyles.Tile5;
                    break;
                case 6:
                    Slot1Image = DigitStyles.Tile6;
                    break;
                case 7:
                    Slot1Image = DigitStyles.Tile7;
                    break;
                case 8:
                    Slot1Image = DigitStyles.Tile8;
                    break;
                case 9:
                    Slot1Image = DigitStyles.Tile9;
                    break;
            }
        }

        private void RedrawSlot2(int digit)
        {
            switch (digit)
            {
                case 0:
                    Slot2Image = DigitStyles.Tile0;
                    break;
                case 1:
                    Slot2Image = DigitStyles.Tile1;
                    break;
                case 2:
                    Slot2Image = DigitStyles.Tile2;
                    break;
                case 3:
                    Slot2Image = DigitStyles.Tile3;
                    break;
                case 4:
                    Slot2Image = DigitStyles.Tile4;
                    break;
                case 5:
                    Slot2Image = DigitStyles.Tile5;
                    break;
                case 6:
                    Slot2Image = DigitStyles.Tile6;
                    break;
                case 7:
                    Slot2Image = DigitStyles.Tile7;
                    break;
                case 8:
                    Slot2Image = DigitStyles.Tile8;
                    break;
                case 9:
                    Slot2Image = DigitStyles.Tile9;
                    break;
            }
        }

        private void RedrawSlot3(int digit)
        {
            switch (digit)
            {
                case 0:
                    Slot3Image = DigitStyles.Tile0;
                    break;
                case 1:
                    Slot3Image = DigitStyles.Tile1;
                    break;
                case 2:
                    Slot3Image = DigitStyles.Tile2;
                    break;
                case 3:
                    Slot3Image = DigitStyles.Tile3;
                    break;
                case 4:
                    Slot3Image = DigitStyles.Tile4;
                    break;
                case 5:
                    Slot3Image = DigitStyles.Tile5;
                    break;
                case 6:
                    Slot3Image = DigitStyles.Tile6;
                    break;
                case 7:
                    Slot3Image = DigitStyles.Tile7;
                    break;
                case 8:
                    Slot3Image = DigitStyles.Tile8;
                    break;
                case 9:
                    Slot3Image = DigitStyles.Tile9;
                    break;
            }
        }

        private void OnStopTimer()
        {
            _counter = 0;

            _timer.Stop();
        }

        private void OnStartTimer()
        {
            DrawInitial();

            _timer.Start();
        }

        private void OnResetGame()
        {
            OnStopTimer();

            DrawInitial();
        }

        private IEnumerable<int> GetDigits()
        {
            return _counter.ToString().Select(x => int.Parse(x.ToString()));
        }

        private void DrawInitial()
        {
            RedrawSlot1(0);
            RedrawSlot2(0);
            RedrawSlot3(0);
        }
    }
}
