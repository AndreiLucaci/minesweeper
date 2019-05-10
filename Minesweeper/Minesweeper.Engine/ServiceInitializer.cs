using Minesweeper.Engine.Services;
using Minesweeper.Engine.Services.Contracts;
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
