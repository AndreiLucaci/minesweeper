using Minesweeper.Models;
using Prism.Events;

namespace Minesweeper.Ui.Events
{
    public class GameStatsEvent : PubSubEvent<GameStats>
    {
    }
}