using System.Collections.Generic;
using System.Linq;
using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Models;

namespace Minesweeper.Engine
{
    public class WorldManager : IWorldManager
    {
        public WorldManager(GameConfiguration configuration)
        {
            CurrentGameConfiguration = configuration;
        }

        public Cell[,] Cells { get; set; }

        public void InitializeWorld()
        {
            Cells = new Cell[CurrentGameConfiguration.Height, CurrentGameConfiguration.Width];

            InitializeCells();

            InitializeNeighbours();

            ReorderCells();
        }

        public void ReorganizeCells(Cell cell)
        {
            ReorganizeCellsInternal(cell);
        }

        public bool IsGameEndedWithSuccess()
        {
            return IsEndGame();
        }

        public GameConfiguration CurrentGameConfiguration { get; }

        public IEnumerable<Cell> ComputeDefusedMines()
        {
            return Cells.Cast<Cell>().Where(x => x.CellType == CellType.Mine && x.CellState == CellState.FlaggedAsMine);
        }

        public IEnumerable<Cell> ComputeExplodedMines()
        {
            return Cells.Cast<Cell>().Where(x => x.CellType == CellType.Mine && x.CellState == CellState.Mine);
        }

        public IEnumerable<Cell> ComputeUntouchedMines()
        {
            return Cells.Cast<Cell>().Where(x => x.CellType == CellType.Mine && x.CellState == CellState.Untouched);
        }

        public IEnumerable<Cell> ComputeWrongFlaggedMines()
        {
            return Cells.Cast<Cell>().Where(x => x.CellType == CellType.EmptyCell && x.CellState == CellState.FlaggedAsMine);
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
                        break;

                    case CellState.Untouched:
                        cell.CellState = CellState.FlaggedAsMine;
                        break;
                }
            }
        }

        public GameState OpenCell(Cell cell)
        {
            switch (cell.CellState)
            {
                case CellState.FlaggedAsMine:
                    return GameState.Advance;
                case CellState.Opened:
                    if (cell.ComputeNumberOfMines() > 0)
                        OpenValidNeighbours(cell);
                    if (IsMineExploded())
                        return GameState.GameOver;
                    break;
                case CellState.Untouched:
                    switch (cell.CellType)
                    {
                        case CellType.Mine:
                            cell.CellState = CellState.Mine;
                            cell.IsDirty = true;
                            return GameState.GameOver;
                        case CellType.EmptyCell:
                            OpenCellInner(cell);
                            break;
                    }
                    break;
            }

            return IsEndGame() ? GameState.EndGame : GameState.Advance;
        }

        public void ResetDirty()
        {
            foreach (var cell in Cells)
                cell.IsDirty = false;
        }

        private void OpenValidNeighbours(Cell cell)
        {
            if (cell.ComputeNumberOfMines() - cell.ComputeNumberOfFlags() == 0)
                foreach (var i in cell.Neighbours.Where(x => x.CellState == CellState.Untouched))
                    OpenCellInner(i);
        }

        private void ReorganizeCellsInternal(Cell cell)
        {
            var mines = new List<Cell>();

            if (cell.CellType == CellType.Mine)
            {
                mines.Add(cell);
            }

            mines.AddRange(cell.Neighbours.Where(x => x.CellType == CellType.Mine));

            mines.ForEach(x => DeneutralizeMine(x, cell));
        }

        private void DeneutralizeMine(Cell cell, Cell exceptCell)
        {
            var cellX = cell.Coordinates.X <= 2 ? cell.Coordinates.X + 3 : 0;
            var cellY = cell.Coordinates.Y <= 2 ? cell.Coordinates.Y + 3 : 0;

            for (var i = cellX; i < Cells.GetLength(0); i++)
            {
                for (var j = cellY; j < Cells.GetLength(1); j++)
                {
                    var cellAt = Cells[i, j];

                    if (cellAt.CellType == CellType.EmptyCell && !cellAt.Equals(exceptCell))
                    {
                        cellAt.CellType = cell.CellType;
                        cell.CellType = CellType.EmptyCell;
                        return;
                    }
                }
            }
        }

        private void OpenCellInner(Cell cell)
        {
            cell.IsDirty = true;

            if (cell.CellType == CellType.Mine)
            {
                cell.CellState = CellState.Mine;

                return;
            }

            cell.CellState = CellState.Opened;
            if (cell.ComputeNumberOfMines() == 0)
                foreach (var cellNeighbour in cell.Neighbours.Where(x => x.CellState == CellState.Untouched))
                    OpenCellInner(cellNeighbour);
        }

        #region Checks

        private bool IsEndGame()
        {
            return Cells.Cast<Cell>().Where(x => x.CellType == CellType.EmptyCell).All(x => x.CellState == CellState.Opened);
        }

        private bool CanFlag(Cell cell)
        {
            return cell.CellState == CellState.FlaggedAsMine || cell.CellState == CellState.Untouched;
        }

        private bool IsMineExploded()
        {
            return Cells.Cast<Cell>().Where(x => x.CellType == CellType.Mine).Any(x =>
                x.CellState != CellState.Untouched && x.CellState != CellState.FlaggedAsMine);
        }

        #endregion Checks

        #region Initialization

        private void InitializeNeighbours()
        {
            foreach (var cell in Cells)
            {
                var cells = new HashSet<Cell>();

                for (var i = cell.Coordinates.X - 1; i <= cell.Coordinates.X + 1; i++)
                    for (var j = cell.Coordinates.Y - 1; j <= cell.Coordinates.Y + 1; j++)
                    {
                        if (!IsIndexesValid(i, j)) continue;

                        var neighbourCell = Cells[i, j];
                        if (neighbourCell != null && !neighbourCell.Equals(cell))
                            cells.Add(neighbourCell);
                    }

                cell.Neighbours = cells;
            }
        }

        private bool IsIndexesValid(int i, int j)
        {
            return (i >= 0 && i < CurrentGameConfiguration.Width) && (j >= 0 && j < CurrentGameConfiguration.Height);
        }

        private void InitializeCells()
        {
            var coordinateRandomizer = new CoordinateRandomiser();

            var randomPoints = coordinateRandomizer.GenerateRandomCoordinates(CurrentGameConfiguration);

            for (var i = 0; i < CurrentGameConfiguration.Width; i++)
                for (var j = 0; j < CurrentGameConfiguration.Height; j++)
                {
                    var point = new Point(i, j);

                    var cell = new Cell
                    {
                        CellType = randomPoints.Contains(point) ? CellType.Mine : CellType.EmptyCell,
                        Coordinates = point
                    };

                    Cells[i, j] = cell;
                }
        }

        private void ReorderCells()
        {
            //Cells = Cells.OrderBy(x => x.Coordinates.Y).ThenBy(x => x.Coordinates.X).ToHashSet();
        }

        #endregion Initialization
    }
}