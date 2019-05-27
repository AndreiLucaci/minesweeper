using System.Collections.Generic;
using Minesweeper.Infrastructure;
using Minesweeper.Models;

namespace Minesweeper.Engine.Contracts
{
    public interface IWorldManager
    {
        HashSet<Cell> Cells { get; set; }

        void InitializeWorld();

        GameState OpenCell(Cell cell);

        void FlagCell(Cell cell);

        void ResetDirty();

        void ReorganizeCells(Cell cell);

        bool IsGameEndedWithSuccess();

        GameConfiguration CurrentGameConfiguration { get; }

        IEnumerable<Cell> ComputeDefusedMines();

        IEnumerable<Cell> ComputeExplodedMines();

        IEnumerable<Cell> ComputeUntouchedMines();
    }
}