namespace Minesweeper.Models
{
    public class GameConfiguration
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int NumberOfMines { get; set; }

        public GameConfiguration(int height, int width, int numberOfMines)
        {
            Height = height;
            Width = width;
            NumberOfMines = numberOfMines;
        }
    }
}
