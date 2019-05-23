using System.Collections.Generic;
using Minesweeper.Models;

namespace Minesweeper.Engine.Contracts
{
	public interface IWorldManager
	{
		void InitializeWorld();

		GameState OpenCell(Cell cell);

		void FlagCell(Cell cell);

		HashSet<Cell> Cells { get; set; }

		void ResetDirty();

        void ReorganizeCells(Cell cell);

	    bool IsGameEndedWithSuccess();
	}
}
