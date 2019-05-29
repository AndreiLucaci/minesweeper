using System.Windows;
using CommonServiceLocator;
using Minesweeper.Engine;
using Minesweeper.Ui.Models;
using Minesweeper.Ui.Processors;
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
            containerRegistry
                .RegisterWithEngineTypes()
                .RegisterWithUiTypes();
        }

        protected override Window CreateShell()
        {
            var skinProcessor = ServiceLocator.Current.GetInstance<ISkinProcessor>();

            skinProcessor.Process(SkinType.Default);

            var window = ServiceLocator.Current.GetInstance<GameWindow>();

            window.DataContext = ServiceLocator.Current.GetInstance<GameWindowViewModel>();

            return window;
        }
    }
}