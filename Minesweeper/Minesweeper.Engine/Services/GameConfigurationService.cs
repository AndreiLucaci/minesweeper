using Minesweeper.Engine.Contracts;
using Minesweeper.Infrastructure;

namespace Minesweeper.Engine.Services
{
    public class GameConfigurationService : IGameConfigurationService
    {
        public GameConfiguration BeginnerConfiguration { get; } = new GameConfiguration(8, 8, 10);
        public GameConfiguration AdvancedConfiguration { get; } = new GameConfiguration(16, 16, 40);
        public GameConfiguration ExpertConfiguration { get; } = new GameConfiguration(24, 24, 99);

	    public GameConfiguration DefaultConfiguration => BeginnerConfiguration;
    }
}
