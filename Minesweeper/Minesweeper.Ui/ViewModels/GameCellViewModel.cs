using System.Windows.Media.Imaging;
using CommonServiceLocator;
using Minesweeper.Models;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;

namespace Minesweeper.Ui.ViewModels
{
    public class GameCellViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private Cell _cell;
        private BitmapImage _cellImageBitmap;

        private IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                {
                    _eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
                }

                return _eventAggregator;
            }
        }

        public GameCellViewModel(Cell cell)
        {
            ClickCommand = new DelegateCommand(OnClick);
            FlagCommand = new DelegateCommand(OnFlag);
            Cell = cell;
        }

        public int Height { get; } = GameConstants.GameViewHeight;

        public int Width { get; } = GameConstants.GameViewWidth;

        public Cell Cell
        {
            get => _cell;
            set => SetProperty(ref _cell, value, nameof(Cell));
        }

        public DelegateCommand ClickCommand { get; set; }
        public DelegateCommand FlagCommand { get; set; }

        public BitmapImage CellImageBitmap
        {
            get => _cellImageBitmap;
            set => SetProperty(ref _cellImageBitmap, value, nameof(CellImageBitmap));
        }

        public void OnCellRedrawn(Cell cell = null)
        {
            cell = cell ?? Cell;

            if (cell.Equals(Cell))
                Redrawn();
        }

        private void OnClick()
        {
            if (Cell.CellState == CellState.Untouched || Cell.CellState == CellState.Opened)
                EventAggregator.GetEvent<CellClickEvent>().Publish(Cell);
        }

        private void OnFlag()
        {
            if (Cell.CellState == CellState.Untouched || Cell.CellState == CellState.FlaggedAsMine)
                EventAggregator.GetEvent<CellFlagEvent>().Publish(Cell);
        }

        private void Redrawn()
        {
            switch (Cell.CellState)
            {
                case CellState.Mine:
                    SetMine();
                    break;
                case CellState.Opened:
                    SetOpen();
                    break;
                case CellState.FlaggedAsMine:
                    SetFlag();
                    break;
                case CellState.MissFlag:
                    SetMissFlag();
                    break;
                case CellState.UntouchedMine:
                    SetUntouchedMine();
                    break;
                default:
                    SetUntouched();
                    break;
            }
        }

        private void SetUntouchedMine()
        {
            CellImageBitmap = CellStyles.MineImagePath;
        }

        private void SetMissFlag()
        {
            CellImageBitmap = CellStyles.MineWrongFlagPath;
        }

        private void SetUntouched()
        {
            CellImageBitmap = CellStyles.UntouchedImagePath;
        }

        private void SetMine()
        {
            CellImageBitmap = CellStyles.MineExplodedImagePath;
        }

        private void SetFlag()
        {
            CellImageBitmap = CellStyles.FlagImagePath;
        }

        private void SetOpen()
        {
            var mines = ComputeNumberOfMines();

            switch (mines)
            {
                case 0:
                    CellImageBitmap = CellStyles.Cell0;
                    break;
                case 1:
                    CellImageBitmap = CellStyles.Cell1;
                    break;
                case 2:
                    CellImageBitmap = CellStyles.Cell2;
                    break;
                case 3:
                    CellImageBitmap = CellStyles.Cell3;
                    break;
                case 4:
                    CellImageBitmap = CellStyles.Cell4;
                    break;
                case 5:
                    CellImageBitmap = CellStyles.Cell5;
                    break;
                case 6:
                    CellImageBitmap = CellStyles.Cell6;
                    break;
                case 7:
                    CellImageBitmap = CellStyles.Cell7;
                    break;
                case 8:
                    CellImageBitmap = CellStyles.Cell8;
                    break;
            }
        }

        private int ComputeNumberOfMines()
        {
            return Cell.ComputeNumberOfMines();
        }
    }
}