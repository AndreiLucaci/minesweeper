using Minesweeper.Models;

namespace Minesweeper.Engine.Services.Contracts
{
    public interface IGameConfigurationService
    {
        GameConfiguration BeginnerConfiguration { get; }
        GameConfiguration AdvancedConfiguration { get; }
        GameConfiguration ExpertConfiguration { get; }
    }
}
