using System.Collections.Generic;
using Minesweeper.Infrastructure;
using Minesweeper.Models;

namespace Minesweeper.Engine.Contracts
{
    public interface IWorldManager
    {
        HashSet<Cell> Cells { get; set; }

        GameConfiguration CurrentGameConfiguration { get; }

        void InitializeWorld();

        GameState OpenCell(Cell cell);

        void FlagCell(Cell cell);

        void ResetDirty();

        void ReorganizeCells(Cell cell);

        bool IsGameEndedWithSuccess();

        IEnumerable<Cell> ComputeDefusedMines();

        IEnumerable<Cell> ComputeExplodedMines();

        IEnumerable<Cell> ComputeUntouchedMines();

        IEnumerable<Cell> ComputeWrongFlaggedMines();
    }
}