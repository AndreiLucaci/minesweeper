using System.Windows;
using System.Windows.Controls;
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

		public Style Style
		{
			get => _style;
			set => SetProperty(ref _style, value, nameof(Style));
		}

	    public Image CellImage
	    {
	        get => _cellImage;
	        set => SetProperty(ref _cellImage, value, nameof(CellImage));
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
			Style = null;
		    CellImage = CellStyles.UntouchedImage;
            Display = string.Empty;
		}

		private void SetMine()
		{
			Style = CellStyles.MineStyle;
		    CellImage = CellStyles.MineExplodedImage;
			Display = string.Empty;
        }

		private void SetFlag()
		{
			Style = CellStyles.FlaggedStyle;
		    CellImage = CellStyles.FlagImage;
			Display = string.Empty;
		}

		private void SetOpen()
		{
			var mines = ComputeNumberOfMines();

			switch (mines)
			{
				case 0:
					Style = CellStyles.OpenStyle;
				    CellImage = CellStyles.Mine0Image;
					break;
				case 1:
					Style = CellStyles.OneMineStyle;
				    CellImage = CellStyles.Mine1Image;
                    break;
				case 2:
					Style = CellStyles.TwoMineStyle;
				    CellImage = CellStyles.Mine2Image;
                    break;
				case 3:
					Style = CellStyles.ThreeMineStyle;
				    CellImage = CellStyles.Mine3Image;
                    break;
				case 4:
					Style = CellStyles.FourMineStyle;
				    CellImage = CellStyles.Mine4Image;
                    break;
				case 5:
					Style = CellStyles.FiveMineStyle;
				    CellImage = CellStyles.Mine5Image;
                    break;
				case 6:
					Style = CellStyles.SixMineStyle;
				    CellImage = CellStyles.Mine6Image;
                    break;
				case 7:
					Style = CellStyles.SevenMineStyle;
				    CellImage = CellStyles.Mine7Image;
                    break;
                case 8:
                    CellImage = CellStyles.Mine8Image;
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
