using Minesweeper.Models;

namespace Minesweeper.Engine.Services.Contracts
{
	public interface IEngine
	{
		void GenerateWorld(GameConfiguration gameConfiguration);
	}
}