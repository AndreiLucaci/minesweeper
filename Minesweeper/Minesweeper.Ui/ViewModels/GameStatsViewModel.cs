using Minesweeper.Models;
using Minesweeper.Ui.Constants;
using Minesweeper.Ui.Properties;

namespace Minesweeper.Ui.ViewModels
{
    public class GameStatsViewModel : BaseDialogViewModel
    {
        private readonly GameStats _gameStats;
        private string _stats;

        public GameStatsViewModel(GameStats gameStats)
        {
            _gameStats = gameStats;

            ShowStats();
        }

        public string Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value, nameof(Stats));
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