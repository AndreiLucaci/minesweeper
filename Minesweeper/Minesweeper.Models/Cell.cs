using System;
using System.Collections.Generic;
using System.Linq;
using Minesweeper.Infrastructure;

namespace Minesweeper.Models
{
	public class Cell : IEquatable<Cell>
	{
		private HashSet<Cell> _neighbours;
		public Point Coordinates { get; set; }

		public HashSet<Cell> Neighbours
		{
			get => _neighbours;
			set
			{
				_neighbours = value;
				OriginalNumberOfMines = WorkingNumberOfMines = ComputeNumberOfMinesWithoutFlags();
			}
		}

		public CellType CellType { get; set; }

		public bool IsOpened { get; set; }

		public bool IsDirty { get; set; }

		public CellState CellState { get; set; } = CellState.Untouched;

		public override string ToString()
		{
			return $"{{x: {Coordinates.X}, y: {Coordinates.Y}}} - {CellType} - {CellState}";
		}

		public int NumberOfAdjacentMines => ComputeNumberOfMinesWithoutFlags();
		public int NumberOfAdjacentMinesWithFlags => ComputeNumberOfMinesWithFlags();
		public int NumberOfAdjacentFlags => ComputeNumberOfFlags();

		public int WorkingNumberOfMines { get; set; }
		public int OriginalNumberOfMines { get; set; }

		public override bool Equals(object obj)
		{
			return Equals(obj as Cell);
		}

		public bool Equals(Cell other)
		{
			return other != null &&
				   EqualityComparer<Point>.Default.Equals(Coordinates, other.Coordinates);
		}

		public override int GetHashCode()
		{
			return (Coordinates != null ? Coordinates.GetHashCode() : 0);
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

		private int ComputeNumberOfFlags()
		{
			var numberOfFlags = Neighbours.Count(neighbour => neighbour.CellState == CellState.FlaggedAsMine);

			return numberOfFlags;
		}
	}
}
