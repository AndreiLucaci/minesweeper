using System.Linq;
using System.Windows;
using Minesweeper.Ui.Views;

namespace Minesweeper.Ui
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();

            Initialize(100);
        }

        private void Initialize(int nr)
        {

        }
    }
}
