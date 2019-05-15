﻿using System;
using System.Collections.Generic;
using Minesweeper.Infrastructure;

namespace Minesweeper.Models
{
	public class Cell : IEquatable<Cell>
	{
		public Point Coordinates { get; set; }

		public List<Cell> Neighbours { get; set; }

		public CellType CellType { get; set; }

		public bool IsOpened { get; set; }

		public CellState CellState { get; set; } = CellState.Untouched;

		public int NumberOfAdjacentMines { get; set; } = -1;

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
	}
}
