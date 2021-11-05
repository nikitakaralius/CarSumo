using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using Game.GameModes.Composites;
using UniRx;

namespace GameModes
{
	public interface IGameModePreferences
	{
		float TimerTimeAmount { get; }
		IGameComposite Composite { get; }
		IReadOnlyReactiveProperty<Account> GetAccountByTeam(Team team);
	}
}