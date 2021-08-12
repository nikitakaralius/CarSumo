using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using UniRx;

namespace GameModes
{
	public interface IGameModePreferences
	{
		float TimerTimeAmount { get; }
		IReadOnlyReactiveProperty<Account> GetAccountByTeam(Team team);
	}
}