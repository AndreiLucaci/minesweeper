using System.Collections.Generic;
using System.Linq;
using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Models;

namespace Minesweeper.Engine
{
	public class WorldManager : IWorldManager
	{
		private readonly GameConfiguration _configuration;

		public HashSet<Cell> Cells { get; set; }

		public WorldManager(GameConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void InitializeWorld()
		{
			Cells = new HashSet<Cell>(_configuration.Width * _configuration.Height);

			InitializeCells();
			ReorderCells();
			InitializeNeighbours();
		}

		public void FlagCell(Cell cell)
		{
			if (CanFlag(cell))
			{
				cell.IsDirty = true;

				switch (cell.CellState)
				{
					case CellState.FlaggedAsMine:
						cell.CellState = CellState.Untouched;
						AddMineFromNeighbours(cell);
						break;
					case CellState.Untouched:
						cell.CellState = CellState.FlaggedAsMine;
						RemoveMineFromNeighbours(cell);
						break;
				}
			}
		}

		private void RemoveMineFromNeighbours(Cell cell)
		{
			foreach (var cellNeighbour in cell.Neighbours)
			{
				cellNeighbour.WorkingNumberOfMines--;
			}
		}

		private void AddMineFromNeighbours(Cell cell)
		{
			foreach (var cellNeighbour in cell.Neighbours)
			{
				cellNeighbour.WorkingNumberOfMines--;
			}
		}

		private void Open1(Cell cell)
		{
			if (cell.WorkingNumberOfMines == 0)
			{
				var queue = new Queue<Cell>(cell.Neighbours);
				var processed = new HashSet<Cell>();

				while (queue.Any())
				{
					var n = queue.Dequeue();

					if (n.WorkingNumberOfMines == 0)
					{
						foreach (var nn in n.Neighbours)
						{
							if (!nn.Equals(cell) && !queue.Contains(nn) && !processed.Contains(nn))
							{
								queue.Enqueue(nn);
							}
						}
					}
					n.CellState = CellState.Opened;
					n.IsDirty = true;
					processed.Add(n);
				}
			}
			cell.CellState = CellState.Opened;
			cell.IsDirty = true;
		}

		public GameState OpenCell(Cell cell)
		{
			if (cell.CellState == CellState.FlaggedAsMine)
			{
				return GameState.Advance;
			}

			if (cell.CellState == CellState.Opened)
			{
				OpenNeighbourCells(cell);

				return GameState.Advance;
			}

			if (IsMine(cell))
			{
				cell.CellState = CellState.Mine;
				cell.IsDirty = true;

				return GameState.GameOver;
			}

			Open1(cell);

			return IsEndGame() ? GameState.EndGame : GameState.Advance;
		}

		public void ResetDirty()
		{
			foreach (var cell in Cells)
			{
				cell.IsDirty = false;
			}
		}

		private void OpenCellInternal(Cell cell, bool openNeighbours = true) 
		{
			cell.CellState = CellState.Opened;

			cell.IsDirty = true;

			if (openNeighbours && cell.WorkingNumberOfMines == 0)
			{
				var validNeighbours = cell.Neighbours.Where(x => x.CellState == CellState.Untouched);
				foreach (var neighbour in validNeighbours)
				{
					OpenCellInternal(neighbour);
				}
			}
		}

		private void OpenNeighbourCells(Cell cell)
		{
			cell.IsDirty = true;

			var validNeighbours = cell.Neighbours.Where(x => x.CellState == CellState.Untouched).ToList();

			foreach (var cellNeighbour in validNeighbours)
			{
				if (cellNeighbour.WorkingNumberOfMines == 0)
				{
					OpenCellInternal(cellNeighbour);
				}
				else
				{
					OpenCellInternal(cellNeighbour, false);
				}
			}
		}

		#region Checks

		private bool CanOpen(Cell cell)
		{
			return cell.CellState == CellState.Untouched && cell.CellType == CellType.EmptyCell;
		}

		private bool IsMine(Cell cell)
		{
			return cell.CellType == CellType.Mine && cell.CellState != CellState.FlaggedAsMine;
		}

		private bool IsEndGame()
		{
			return Cells.Where(x => x.CellType == CellType.EmptyCell).All(x => x.CellState == CellState.Opened);
		}

		private bool CanFlag(Cell cell)
		{
			return cell.CellState == CellState.FlaggedAsMine || cell.CellState == CellState.Untouched;
		}

		#endregion

		#region Initialization

		private void InitializeNeighbours()
		{
			foreach (var cell in Cells)
			{
				var cells = new HashSet<Cell>();

				for (var i = cell.Coordinates.X - 1; i <= cell.Coordinates.X + 1; i++)
				{
					for (var j = cell.Coordinates.Y - 1; j <= cell.Coordinates.Y + 1; j++)
					{
						var neighbourCell = Cells.SingleOrDefault(x => x.Coordinates.Equals(new Point(i, j)));
						if (neighbourCell != null && !neighbourCell.Equals(cell))
						{
							cells.Add(neighbourCell);
						}
					}
				}

				cell.Neighbours = cells;
			}
		}

		private void InitializeCells()
		{
			var coordinateRandomizer = new CoordinateRandomiser();

			var randomPoints = coordinateRandomizer.GenerateRandomCoordinates(_configuration);

			for (var i = 0; i < _configuration.Width; i++)
			{
				for (var j = 0; j < _configuration.Height; j++)
				{
					var point = new Point(i, j);
					var cell = new Cell
					{
						CellType = randomPoints.Contains(point) ? CellType.Mine : CellType.EmptyCell,
						Coordinates = point
					};

					if (!Cells.Contains(cell))
					{
						Cells.Add(cell);
					}
				}
			}
		}

		private void ReorderCells()
		{
			Cells = new HashSet<Cell>(Cells.OrderBy(x => x.Coordinates.X).ThenBy(x => x.Coordinates.Y));
		}

		#endregion
	}
}
