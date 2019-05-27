using System;
using System.Collections.Generic;
using System.Linq;
using Minesweeper.Infrastructure;

namespace Minesweeper.Models
{
    public class Cell : IEquatable<Cell>
    {
        public Point Coordinates { get; set; }

        public HashSet<Cell> Neighbours { get; set; }

        public CellType CellType { get; set; }

        public bool IsDirty { get; set; }

        public CellState CellState { get; set; } = CellState.Untouched;

        public int NumberOfAdjacentMines => ComputeNumberOfMinesWithoutFlags();
        public int NumberOfAdjacentMinesWithFlags => ComputeNumberOfMinesWithFlags();
        public int NumberOfAdjacentFlags => ComputeNumberOfFlags();

        public bool Equals(Cell other)
        {
            return other != null &&
                   EqualityComparer<Point>.Default.Equals(Coordinates, other.Coordinates);
        }

        public override string ToString()
        {
            return $"{{x: {Coordinates.X}, y: {Coordinates.Y}}} - {CellType} - {CellState}";
        }

        public int ComputeFlagNumberOfMines()
        {
            return Neighbours.Where(x => x.CellState == CellState.Untouched).Count(x => x.CellType == CellType.Mine);
        }

        public bool IsMine()
        {
            return CellType == CellType.Mine && CellState != CellState.FlaggedAsMine;
        }

        public int ComputeNumberOfMines()
        {
            return Neighbours.Count(x => x.CellType == CellType.Mine);
        }

        public int ComputeNumberOfFlags()
        {
            return Neighbours.Count(x => x.CellState == CellState.FlaggedAsMine);
        }

        public int ComputeNumberOfUnopenedNeighbours()
        {
            return Neighbours.Count(x => x.CellState == CellState.Untouched);
        }

        public bool IsCovered()
        {
            return Neighbours.All(x => x.CellState == CellState.Opened && x.CellType == CellType.EmptyCell);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }

        public override int GetHashCode()
        {
            return Coordinates != null ? Coordinates.GetHashCode() : 0;
        }

        public static bool operator ==(Cell cell1, Cell cell2)
        {
            return EqualityComparer<Cell>.Default.Equals(cell1, cell2);
        }

        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return !(cell1 == cell2);
        }

        private int ComputeNumberOfMinesWithFlags()
        {
            var numberOfMines = Math.Abs(NumberOfAdjacentMines - NumberOfAdjacentFlags);

            return numberOfMines;
        }

        private int ComputeNumberOfMinesWithoutFlags()
        {
            var numberOfMines = Neighbours.Count(neighbour => neighbour.CellType == CellType.Mine);

            return numberOfMines;
        }
    }
}