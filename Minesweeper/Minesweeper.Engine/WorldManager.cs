using System.Collections.Generic;
using System.Linq;
using System.Text;
using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Models;

namespace Minesweeper.Engine
{
	public class WorldManager : IWorldManager
	{
		private readonly GameConfiguration _configuration;

		public WorldManager(GameConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void InitializeWorld()
		{
			Cells = new List<Cell>(_configuration.Width * _configuration.Height);

			InitializeMines();

			InitializeCells();

			Cells = Cells.OrderBy(x => x.Coordinates.X).ThenBy(x => x.Coordinates.Y).ToList();
		}

		public string PrintWorld()
		{
			var sb = new StringBuilder();

			sb.AppendLine($"+{new string('-', _configuration.Width * 2)}+");
			for (var i = 0; i < _configuration.Height; i++)
			{
				var sbInner = new StringBuilder("|");
				for (var j = 0; j < _configuration.Width; j++)
				{
					sbInner.AppendFormat("{0}|",
						Cells.Single(x => x.Coordinates.Equals(new Point(j, i))).CellType == CellType.Mine ? "x" : " ");
				}
				sb.AppendLine(sbInner.ToString());
			}
			sb.AppendLine($"+{new string('-', _configuration.Width * 2)}+");

			return sb.ToString();
		}

		public bool IsMine(Cell cell)
		{
			return cell.CellType == CellType.Mine;
		}

		public int GetNumberOfAdjacentMines(Cell cell)
		{
			var neighbours = GetNeighbours(cell);

			return neighbours.Where(x => x.CellState == CellState.Untouched).Count(IsMine);
		}

		public GameState OpenCell(Cell cell)
		{
			if (cell.CellState == CellState.FlaggedAsMine)
			{
				return GameState.Advance;
			}

			if (IsMine(cell))
			{
				cell.CellState = CellState.Mine;
				return GameState.GameOver;
			}

			cell.CellState = CellState.Opened;
			var neighbours = GetNeighbours(cell);

			ProcessNeighbours(neighbours);

			return IsEndGame() ? GameState.EndGame : GameState.Advance;
		}

		public List<Cell> Cells { get; set; }

		private IEnumerable<Cell> GetNeighbours(Cell cell)
		{
			var cells = new List<Cell>();

			for (var i = 0; i < cell.Coordinates.X + 1; i++)
			{
				for (var j = 0; j < cell.Coordinates.Y + 1; j++)
				{
					var neighbourCell = Cells.SingleOrDefault(x => x.Coordinates.Equals(new Point(i, j)));
					if (neighbourCell != null && !neighbourCell.Equals(cell))
					{
						cells.Add(neighbourCell);
					}
				}
			}

			return cells;
		}

		private bool IsEndGame()
		{
			return Cells.Where(x => x.CellType != CellType.Mine).All(x => x.IsOpened);
		}

		private void InitializeMines()
		{
			var coordinateRandomizer = new CoordinateRandomiser();

			var randomPoints = coordinateRandomizer.GenerateRandomCoordinates(_configuration);

			foreach (var randomPoint in randomPoints)
			{
				var cell = new Cell
				{
					CellType = CellType.Mine,
					Coordinates = randomPoint,
					Neighbours = new List<Cell>()
				};

				Cells.Add(cell);
			}
		}

		private void InitializeCells()
		{
			for (var i = 0; i < _configuration.Width; i++)
			{
				for (var j = 0; j < _configuration.Height; j++)
				{
					var cell = new Cell
					{
						CellType = CellType.EmptyCell,
						Coordinates = new Point(i, j),
					};

					if (!Cells.Contains(cell))
					{
						Cells.Add(cell);
					}
				}
			}
		}

		private void ProcessNeighbours(IEnumerable<Cell> neighbours)
		{
			foreach (var neighbour in neighbours)
			{
				var numberOfMines = GetNumberOfAdjacentMines(neighbour);
				neighbour.NumberOfAdjacentMines = numberOfMines;

				if (neighbour.CellState == CellState.Untouched)
				{
					neighbour.CellState = CellState.Neighbour;
				}

				if (numberOfMines == 0 && neighbour.CellState != CellState.FlaggedAsMine)
				{
					OpenCell(neighbour);
				}
			}
		}
	}
}
