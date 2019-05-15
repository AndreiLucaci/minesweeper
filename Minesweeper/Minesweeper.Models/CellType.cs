using System;

namespace Minesweeper.Models
{
	[Flags]
	public enum CellType
	{
		Wall = 0,
		Mine = 1,
		EmptyCell = 2,
	}
}
