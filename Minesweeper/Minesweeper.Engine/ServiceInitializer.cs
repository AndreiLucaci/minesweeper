using Minesweeper.Engine.Contracts;
using Minesweeper.Engine.Services;
using Prism.Ioc;

namespace Minesweeper.Engine
{
    public static class ServiceInitializer
    {
        public static IContainerRegistry RegisterWithEngineTypes(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IGameConfigurationService, GameConfigurationService>();

            return containerRegistry;
        }
    }
}