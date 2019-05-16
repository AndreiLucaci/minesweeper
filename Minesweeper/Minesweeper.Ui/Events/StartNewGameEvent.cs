using Minesweeper.Infrastructure;
using Prism.Events;

namespace Minesweeper.Ui.Events
{
	public class StartNewGameEvent : PubSubEvent<GameConfiguration>
	{
	}
}
