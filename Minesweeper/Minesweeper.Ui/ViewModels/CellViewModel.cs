using System.Windows;
using System.Windows.Controls;
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
	public class CellViewModel : BindableBase
	{
		private string _display;

		private Cell _cell;
		private Style _style;
	    private Image _cellImage;

        private readonly IEventAggregator _eventAggregator;
	    private BitmapImage _cellImageBitmap;

	    public CellViewModel()
		{
			ClickCommand = new DelegateCommand(OnClick);
			FlagCommand = new DelegateCommand(OnFlag);
			
			_eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

			SubscribeToEvents();
		}

		~CellViewModel()
		{
			UnsubscribeToEvents();
		}
		
		public int Height { get; } = GameConstants.GameViewHeight;

		public int Width { get; } = GameConstants.GameViewWidth;

		public Cell Cell
		{
			get => _cell;
			set => SetProperty(ref _cell, value, nameof(Cell));
		}

		public string Display
		{
			get => _display;
			set => SetProperty(ref _display, value, nameof(Display));
		}

		public DelegateCommand ClickCommand { get; set; }
		public DelegateCommand FlagCommand { get; set; }

	    public Image CellImage
	    {
	        get => _cellImage;
	        set => SetProperty(ref _cellImage, value, nameof(CellImage));
	    }

	    public BitmapImage CellImageBitmap
	    {
	        get => _cellImageBitmap;
	        set => SetProperty(ref _cellImageBitmap, value, nameof(CellImageBitmap));
	    }

	    private void SubscribeToEvents()
		{
			_eventAggregator?.GetEvent<CellRedrawEvent>()?.Subscribe(OnCellRedrawn);
		}

		private void UnsubscribeToEvents()
		{
			_eventAggregator?.GetEvent<CellRedrawEvent>()?.Unsubscribe(OnCellRedrawn);
		}

	    public void OnCellRedrawn(Cell cell = null)
	    {
	        cell = cell ?? Cell;

			if (cell.Equals(Cell))
			{
				Redrawn();
			}
		}

		private void OnClick()
		{
			if (Cell.CellState == CellState.Untouched || Cell.CellState == CellState.Opened)
			{
				_eventAggregator.GetEvent<CellClickEvent>().Publish(Cell);
			}
		}

		private void OnFlag()
		{
			if (Cell.CellState == CellState.Untouched || Cell.CellState == CellState.FlaggedAsMine)
			{
				_eventAggregator.GetEvent<CellFlagEvent>().Publish(Cell);
			}
		}

		private void Redrawn()
		{
			switch (Cell.CellState)
			{
				case CellState.Opened:
					SetOpen();
					break;
				case CellState.FlaggedAsMine:
					SetFlag();
					break;
				case CellState.Mine:
					SetMine();
					break;
				default:
					SetUntouched();
					break;
			}
		}

		private void SetUntouched()
		{
		    CellImageBitmap = CellStyles.UntouchedImagePath;
            Display = string.Empty;
		}

		private void SetMine()
		{
		    CellImageBitmap = CellStyles.MineExplodedImagePath;
            Display = string.Empty;
        }

		private void SetFlag()
		{
		    CellImageBitmap = CellStyles.FlagImagePath;
            Display = string.Empty;
		}

		private void SetOpen()
		{
			var mines = ComputeNumberOfMines();

			switch (mines)
			{
				case 0:
				    CellImageBitmap = CellStyles.Mine0ImagePath;
					break;
				case 1:
				    CellImageBitmap = CellStyles.Mine1ImagePath;
                    break;
				case 2:
				    CellImageBitmap = CellStyles.Mine2ImagePath;
                    break;
				case 3:
				    CellImageBitmap = CellStyles.Mine3ImagePath;
                    break;
				case 4:
				    CellImageBitmap = CellStyles.Mine4ImagePath;
                    break;
				case 5:
				    CellImageBitmap = CellStyles.Mine5ImagePath;
                    break;
				case 6:
				    CellImageBitmap = CellStyles.Mine6ImagePath;
                    break;
				case 7:
				    CellImageBitmap = CellStyles.Mine7ImagePath;
                    break;
                case 8:
                    CellImageBitmap = CellStyles.Mine8ImagePath;
                    break;
            }

			Display = mines == 0 ? string.Empty : mines.ToString();
		}

		private int ComputeNumberOfMines()
		{
		    return Cell.ComputeNumberOfMines();
		}
	}
}
