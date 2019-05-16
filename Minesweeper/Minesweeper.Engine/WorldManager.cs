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
            InitializeNeighbours();

            ReorderCells();
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

        private void OpenSingleCell(Cell cell)
        {
            if (cell.CellState == CellState.Untouched)
            {
                cell.IsDirty = true;

                if (cell.CellType == CellType.EmptyCell)
                {
                    cell.CellState = CellState.Opened;

                    if (cell.ComputeFlagNumberOfMines() == 0)
                    {
                        var validNeighbours = cell.Neighbours.Where(x => x.CellState == CellState.Untouched);
                        foreach (var i in validNeighbours)
                        {
                            OpenSingleCell(i);
                        }
                    }

                    var neighbours = cell.Neighbours.Where(x => x.CellState == CellState.Untouched && x.CellType == CellType.EmptyCell && x.ComputeFlagNumberOfMines() == 0);
                    foreach (var i in neighbours)
                    {
                        OpenSingleCell(i);
                    }
                }

                if (cell.CellType == CellType.Mine)
                {
                    cell.CellState = CellState.Mine;
                }
            }
        }

        public GameState OpenCell(Cell cell)
        {
            if (cell.CellState == CellState.FlaggedAsMine)
            {
                return GameState.Advance;
            }

            if (cell.CellState == CellState.Opened && cell.WorkingNumberOfMines > 0)
            {
                OpenValidNeighbours(cell);

                return GameState.Advance;
            }

            if (IsMine(cell))
            {
                cell.CellState = CellState.Mine;
                cell.IsDirty = true;

                return GameState.GameOver;
            }

            OpenSingleCell(cell);

            return IsEndGame() ? GameState.EndGame : GameState.Advance;
        }

        private void OpenValidNeighbours(Cell cell)
        {
            var neighbours = cell.Neighbours.ToList();

            var mineNeighbours = neighbours.Where(x => x.CellType == CellType.Mine).ToList();
            var flaggedNeighbours = neighbours.Where(x => x.CellState == CellState.FlaggedAsMine).ToList();

            if (mineNeighbours.Count - flaggedNeighbours.Count == 0)
            {
                var untouchedNeighbours = neighbours.Where(x => x.CellState == CellState.Untouched).ToList();

                foreach (var i in untouchedNeighbours)
                {
                    OpenSingleCell(i);
                }
            }
        }

        public void ResetDirty()
        {
            foreach (var cell in Cells)
            {
                cell.IsDirty = false;
            }
        }

        private void OpenCellInternal(Cell cell, bool openNeighbours = true)
        {
            cell.CellState = CellState.Opened;

            cell.IsDirty = true;

            if (openNeighbours && cell.WorkingNumberOfMines == 0)
            {
                var validNeighbours = cell.Neighbours.Where(x => x.CellState == CellState.Untouched);
                foreach (var neighbour in validNeighbours)
                {
                    OpenCellInternal(neighbour);
                }
            }
        }

        #region Checks

        private bool IsMine(Cell cell)
        {
            return cell.CellType == CellType.Mine && cell.CellState != CellState.FlaggedAsMine;
        }

        private bool IsEndGame()
        {
            return Cells.Where(x => x.CellType == CellType.EmptyCell).All(x => x.CellState == CellState.Opened);
        }

        private bool CanFlag(Cell cell)
        {
            return cell.CellState == CellState.FlaggedAsMine || cell.CellState == CellState.Untouched;
        }

        #endregion Checks

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
            Cells = Cells.OrderBy(x => x.Coordinates.Y).ThenBy(x => x.Coordinates.X).ToHashSet();
        }

        #endregion Initialization
    }
}