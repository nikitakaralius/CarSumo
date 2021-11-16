using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;

namespace Game.Endgame
{
	public class SingleModeEndGameTracking : EndGameTracking
	{
		private const Team BotTeam = Team.Red;
		
		public SingleModeEndGameTracking(IUnitTracking unitTracking, IGameModePreferences preferences, GameStateMachine stateMachine) 
			: base(unitTracking, preferences, stateMachine) { }
		protected override PersonalizedEndGameStatus Status(Team winningTeam, Account account)
		{
			if (winningTeam == BotTeam)
				return new SingleModeLose(account);

			return new SingleModeWin(account);
		}
	}
}