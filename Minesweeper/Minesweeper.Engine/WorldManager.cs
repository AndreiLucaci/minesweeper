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
		private List<Cell> _cells;

		public WorldManager(GameConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void InitializeWorld()
		{
			_cells = new List<Cell>(_configuration.Width * _configuration.Height);

			InitializeMines();

		}

		public bool IsMine(Cell cell)
		{
			return cell.CellType == CellType.Mine;
		}

		public int GetNumberOfAdjacentMines(Cell cell)
		{
			var neighbours = GetNeighbours(cell);

			return neighbours.Count(IsMine);
		}

		private IEnumerable<Cell> GetNeighbours(Cell cell)
		{
			var cells = new List<Cell>();

			for (var i = 0; i < cell.Coordinates.X + 1; i++)
			{
				for (var j = 0; j < cell.Coordinates.Y + 1; j++)
				{
					var neighbourCell = _cells.SingleOrDefault(x => x.Coordinates.Equals(new Point(i, j)));
					if (neighbourCell != null && !neighbourCell.Equals(cell))
					{
						cells.Add(neighbourCell);
					}
				}
			}

			return cells;
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

				_cells.Add(cell);
			}
		}
	}
}
