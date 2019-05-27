using Minesweeper.Models;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Properties;

namespace Minesweeper.Ui.ViewModels
{
    public class GameStatsViewModel : BaseDialogViewModel
    {
        private string _stats;

        private readonly Minesweeper.Models.GameStats _gameStats;

        public string Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value, nameof(Stats));
        }

        public GameStatsViewModel(GameStats gameStats)
        {
            _gameStats = gameStats;

            ShowStats();
        }

        private void ShowStats()
        {
            var template = Resources.GameStats;

            Stats = string.Format(template,
                _gameStats.IsWin ? "Win" : "Loose",
                _gameStats.TimeElapsed,
                _gameStats.DefusedMines,
                _gameStats.ExplodedMines,
                _gameStats.UntouchedMines,
                _gameStats.Configuration.Name
            );

            Logo = _gameStats.IsWin ? BaseStyles.WinFace : BaseStyles.LooseFace;
        }
    }
}
