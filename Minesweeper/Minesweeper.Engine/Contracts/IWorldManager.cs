using System.Collections.Generic;
using Minesweeper.Models;

namespace Minesweeper.Engine.Contracts
{
	public interface IWorldManager
	{
		void InitializeWorld();

		string PrintWorld();

		bool IsMine(Cell cell);

		int GetNumberOfAdjacentMines(Cell cell);

		GameState OpenCell(Cell cell);

		List<Cell> Cells { get; set; }
	}
}
