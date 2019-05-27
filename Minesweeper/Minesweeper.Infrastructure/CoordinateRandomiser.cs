using System;
using System.Collections.Generic;

namespace Minesweeper.Infrastructure
{
    public class CoordinateRandomiser
    {
        public HashSet<Point> GenerateRandomCoordinates(GameConfiguration gameConfiguration)
        {
            Guard.ArgumentNotNull(gameConfiguration, nameof(gameConfiguration));

            var points = new HashSet<Point>(gameConfiguration.NumberOfMines);

            while (points.Count < gameConfiguration.NumberOfMines)
            {
                var point = GenerateRandomCoordinate(gameConfiguration);
                if (!points.Contains(point))
                    points.Add(point);
            }

            return points;
        }

        private static Point GenerateRandomCoordinate(GameConfiguration gameConfiguration)
        {
            var rnd = new Random(DateTime.Now.Millisecond);

            var point = new Point(rnd.Next(0, gameConfiguration.Width), rnd.Next(0, gameConfiguration.Height));

            return point;
        }
    }
}