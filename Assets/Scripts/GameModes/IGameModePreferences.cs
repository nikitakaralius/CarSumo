using CarSumo.DataModel.Accounts;
using CarSumo.Teams;

namespace GameModes
{
	public interface IGameModePreferences
	{
		float TimerTimeAmount { get; }
		Account GetAccountByTeam(Team team);
	}
}