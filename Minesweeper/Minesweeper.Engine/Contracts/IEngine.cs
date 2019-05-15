using Minesweeper.Infrastructure;

namespace Minesweeper.Engine.Contracts
{
	public interface IEngine
	{
		void GenerateWorld(GameConfiguration gameConfiguration);
	}
}