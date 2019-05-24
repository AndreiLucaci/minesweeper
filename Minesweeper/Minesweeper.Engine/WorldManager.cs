using System.Collections.Generic;
using System.Diagnostics;
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

            MethodTimer mt;

            using (mt = new MethodTimer())
            {
                InitializeCells();
            }
            Debug.WriteLine(mt.Time(nameof(InitializeCells)));

            using (mt = new MethodTimer())
            {
                InitializeNeighbours();
            }
            Debug.WriteLine(mt.Time(nameof(InitializeNeighbours)));

            using (mt = new MethodTimer())
            {
                ReorderCells();
            }
            Debug.WriteLine(mt.Time(nameof(ReorderCells)));
        }

        public void ReorganizeCells(Cell cell)
        {
            for (var i = 1; i < _configuration.Width - 1; i++)
            {
                for (var j = 1; j < _configuration.Height - 1; j++)
                {
                    var cellAt = Cells.Single(x => x.Coordinates.Equals(new Point(i, j)));
                    if (cellAt.CellType == CellType.EmptyCell && cellAt.ComputeNumberOfMines() == 0)
                    {
                        SwitchCells(cell, cellAt);

                        foreach (var (first, seocond) in cell.Neighbours.Zip(cellAt.Neighbours, (c1, c2) => (c1, c2)))
                        {
                            SwitchCells(first, seocond);
                        }

                        return;
                    }
                }
            }
        }

        public bool IsGameEndedWithSuccess()
        {
            return Cells.Where(x => x.CellType == CellType.EmptyCell).All(x => x.CellState == CellState.Opened);
        }

        private bool SwitchCells(Cell cell1, Cell cell2)
        {
            var tempState = cell1.CellState;
            cell1.CellState = cell2.CellState;
            cell2.CellState = tempState;

            var tempType = cell1.CellType;
            cell1.CellType = cell2.CellType;
            cell2.CellType = tempType;

            return true;
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
                    {
                        OpenValidNeighbours(cell);
                    }

                    return GameState.Advance;
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

        private void OpenValidNeighbours(Cell cell)
        {
            if (cell.ComputeNumberOfMines() - cell.ComputeNumberOfFlags() == 0)
            {
                foreach (var i in cell.Neighbours.Where(x => x.CellState == CellState.Untouched))
                {
                    OpenCellInner(i);
                }
            }
        }

        private void OpenCellInner(Cell cell)
        {
            cell.IsDirty = true;
            cell.CellState = CellState.Opened;

            if (cell.ComputeNumberOfMines() == 0)
            {
                foreach (var cellNeighbour in cell.Neighbours.Where(x => x.CellState == CellState.Untouched))
                {
                    OpenCellInner(cellNeighbour);
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

        #region Checks

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