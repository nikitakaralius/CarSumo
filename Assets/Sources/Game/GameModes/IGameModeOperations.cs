using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using Game.GameModes.Composites;

namespace GameModes
{
	public interface IGameModeOperations
	{
		void RegisterAccount(Team team, Account account);
		void ConfigureTimer(float timeAmount);
		void ChooseGameComposite(IGameComposite composite);
	}
}