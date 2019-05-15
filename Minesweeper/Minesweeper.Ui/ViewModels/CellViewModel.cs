using System;
using System.Windows;
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
	    private Cell _cell;
	    private readonly IEventAggregator _eventAggregator;
	    private string _display;
	    private Style _style;

	    public CellViewModel()
	    {
		    ClickCommand = new DelegateCommand(OnClick);

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

	    public Style Style
	    {
		    get => _style;
		    set => SetProperty(ref _style, value, nameof(Style));
	    }

		private void SubscribeToEvents()
		{
			_eventAggregator?.GetEvent<CellRedrawEvent>()?.Subscribe(OnCellRedrawn);
		}

	    private void UnsubscribeToEvents()
	    {
		    _eventAggregator?.GetEvent<CellRedrawEvent>()?.Subscribe(OnCellRedrawn);
	    }

	    private void OnCellRedrawn(Cell cell)
	    {
		    if (cell.Equals(Cell))
		    {
			    Redrawn(cell);
		    }
	    }

	    private void OnClick()
	    {
		    _eventAggregator.GetEvent<CellClickEvent>().Publish(Cell);
	    }

	    private void Redrawn(Cell cell)
	    {
		    switch (cell.CellState)
		    {
			    case CellState.Opened:
				    SetOpen();
				    break;
			    case CellState.Neighbour:
				    SetNeighbour(cell);
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
		    Style = CellStyles.UntouchedStyle;
		    Display = string.Empty;
	    }

		private void SetMine()
	    {
		    Style = CellStyles.MineStyle;
		    Display = string.Empty;
	    }

		private void SetFlag()
	    {
		    Style = CellStyles.FlaggedStyle;
		    Display = string.Empty;
	    }

	    private void SetNeighbour(Cell cell)
	    {
			switch (cell.NumberOfAdjacentMines)
			{
				case 1:
					Style = CellStyles.OneMineStyle;
					break;
				case 2:
					Style = CellStyles.TwoMineStyle;
					break;
				case 3:
					Style = CellStyles.ThreeMineStyle;
					break;
				case 4:
					Style = CellStyles.FourMineStyle;
					break;
				case 5:
					Style = CellStyles.FiveMineStyle;
					break;
				case 6:
					Style = CellStyles.SixMineStyle;
					break;
				case 7:
					Style = CellStyles.SevenMineStyle;
					break;
			}

		    Display = cell.NumberOfAdjacentMines.ToString();
	    }

		private void SetOpen()
	    {
		    Style = CellStyles.OpenStyle;
		    Display = string.Empty;
		}
	}
}
