using System;
using System.Collections.Generic;

namespace Minesweeper.Infrastructure
{
    public class Point : IEquatable<Point>
    {
        public Point()
        {
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Point other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Point);
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Point point1, Point point2)
        {
            return EqualityComparer<Point>.Default.Equals(point1, point2);
        }

        public static bool operator !=(Point point1, Point point2)
        {
            return !(point1 == point2);
        }
    }
}