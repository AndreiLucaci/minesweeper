using System.Windows;
using CommonServiceLocator;
using Minesweeper.Engine;
using Minesweeper.Ui.ViewModels;
using Prism.Ioc;
using Prism.Unity;

namespace Minesweeper.Ui
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterWithEngineTypes();
        }

        protected override Window CreateShell()
        {
            var window = ServiceLocator.Current.GetInstance<GameWindow>();

            window.DataContext = ServiceLocator.Current.GetInstance<GameWindowViewModel>();

            return window;
        }
    }
}