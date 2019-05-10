using Minesweeper.Engine.Services.Contracts;
using Minesweeper.Infrastructure;
using Minesweeper.Models;

namespace Minesweeper.Ui.ViewModels
{
    public class GameGridViewModel
    {
        private readonly IGameConfigurationService _gameConfigurationService;

        private readonly int _multiplier = 20;

        public GameGridViewModel(IGameConfigurationService gameConfigurationService)
        {
            Guard.ArgumentNotNull(gameConfigurationService, nameof(gameConfigurationService));

            _gameConfigurationService = gameConfigurationService;

            GameConfiguration = _gameConfigurationService.BeginnerConfiguration;
        }

        public int GameWidth => GameConfiguration.Width * _multiplier;

        public int GameHeight => GameConfiguration.Height * _multiplier;

        public GameConfiguration GameConfiguration { get; }
    }
}
