namespace Minesweeper.Infrastructure
{
    public class GameConfiguration
    {
        public GameConfiguration(int height, int width, int numberOfMines)
        {
            Height = height;
            Width = width;
            NumberOfMines = numberOfMines;
        }

        public int Height { get; set; }
        public int Width { get; set; }
        public int NumberOfMines { get; set; }
    }
}