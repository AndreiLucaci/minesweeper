using System.Windows;
using CommonServiceLocator;
using Minesweeper.Engine;
using Prism.Ioc;
using Prism.Unity;

namespace Minesweeper.Ui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterWithEngineTypes();
        }

        protected override Window CreateShell()
        {
            return ServiceLocator.Current.GetInstance<GameWindow>();
        }
    }
}
