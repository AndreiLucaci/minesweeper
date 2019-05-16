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

		public GameState OpenCell(Cell cell)
		{
			if (cell.CellState == CellState.FlaggedAsMine || cell.CellState == CellState.Opened)
			{
				return GameState.Advance;
			}

			if (IsMine(cell))
			{
				cell.CellState = CellState.Mine;
				cell.IsDirty = true;

				return GameState.GameOver;
			}

			OpenCellInternal(cell);

			return IsEndGame() ? GameState.EndGame : GameState.Advance;
		}

		public void ResetDirty()
		{
			foreach (var cell in Cells)
			{
				cell.IsDirty = false;
			}
		}

		private void OpenCellInternal(Cell cell)
		{
			cell.CellState = CellState.Opened;

			cell.IsDirty = true;

			if (cell.NumberOfAdjacentMines == 0)
			{
				var validNeighbours = cell.Neighbours.Where(CanOpen);
				foreach (var neighbour in validNeighbours)
				{
					OpenCellInternal(neighbour);
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
			return cell.CellType == CellType.Mine;
		}

		private bool IsEndGame()
		{
			return Cells.Where(x => x.CellType == CellType.EmptyCell).All(x => x.CellState == CellState.Opened);
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
