using CarSumo.DataModel.Accounts;
using CarSumo.Teams;

namespace GameModes
{
	public interface IGameModeOperations
	{
		void RegisterAccount(Team team, Account account);
		void ConfigureTimer(float timeAmount);
	}
}