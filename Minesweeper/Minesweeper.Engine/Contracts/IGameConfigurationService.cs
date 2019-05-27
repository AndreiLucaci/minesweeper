using Minesweeper.Infrastructure;

namespace Minesweeper.Engine.Contracts
{
    public interface IGameConfigurationService
    {
        GameConfiguration BeginnerConfiguration { get; }
        GameConfiguration AdvancedConfiguration { get; }
        GameConfiguration ExpertConfiguration { get; }
        GameConfiguration DefaultConfiguration { get; }
    }
}