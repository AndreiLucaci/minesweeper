using Minesweeper.Infrastructure;

namespace Minesweeper.Models
{
    public class GameStats
    {
        public bool IsWin { get; set; }

        public int DefusedMines { get; set; }

        public int ExplodedMines { get; set; }

        public int UntouchedMines { get; set; }

        public int TimeElapsed { get; set; }

        public GameConfiguration Configuration { get; set; }
    }
}
