using System;

namespace Minesweeper.Models
{
    [Flags]
    public enum CellType
    {
        Mine = 0,
        EmptyCell = 1
    }
}