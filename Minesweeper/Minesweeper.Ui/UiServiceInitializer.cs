using Minesweeper.Ui.Processors;
using Prism.Ioc;

namespace Minesweeper.Ui
{
    public static class UiServiceInitializer
    {
        public static IContainerRegistry RegisterWithUiTypes(this IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ISkinProcessor>(new CustomSkinProcessor(new DefaultSkinProcessor()));

            return containerRegistry;
        }
    }
}
