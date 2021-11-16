using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;

namespace Game.Endgame
{
	public class OneDeviceEndGameTracking : EndGameTracking
	{
		public OneDeviceEndGameTracking(IUnitTracking unitTracking, IGameModePreferences preferences, GameStateMachine stateMachine) 
			: base(unitTracking, preferences, stateMachine) { }
		protected override PersonalizedEndGameStatus Status(Team winningTeam, Account account) => 
			new OneDeviceEndGame(account);
	}
}