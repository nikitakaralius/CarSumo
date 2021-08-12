using CarSumo.DataModel.Accounts;
using CarSumo.Teams;

namespace GameModes
{
	public interface IGameModeOperations
	{
		void RegisterAccount(Account account, Team team);
		void ConfigureTimer(float timeAmount);
	}
}