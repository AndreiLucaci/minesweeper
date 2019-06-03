using System;
using System.Collections.Generic;

namespace Minesweeper.Infrastructure
{
    public class CoordinateRandomiser
    {
        private readonly Random _random = new Random(DateTime.Now.Millisecond);

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

        private Point GenerateRandomCoordinate(GameConfiguration gameConfiguration)
        {
            var point = new Point(_random.Next(0, gameConfiguration.Width), _random.Next(0, gameConfiguration.Height));

            return point;
        }
    }
}