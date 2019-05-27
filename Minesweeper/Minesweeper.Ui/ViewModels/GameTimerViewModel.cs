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

        private int _counter;
        private BitmapImage _slot1Image;
        private BitmapImage _slot2Image;
        private BitmapImage _slot3Image;

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
                        RedrawSlot(1, digit);
                        break;
                    case 1:
                        RedrawSlot(2, digit);
                        break;
                    case 2:
                        RedrawSlot(3, digit);
                        break;
                }
            }
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
                case 3:
                    Slot3Image = tile;
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
            RedrawSlot(1, 0);
            RedrawSlot(2, 0);
            RedrawSlot(3, 0);
        }
    }
}