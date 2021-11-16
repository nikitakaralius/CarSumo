using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;

namespace Game.Endgame
{
	public class SingleModeStatusProvider : IEndgameStatusProvider
	{
		private const Team BotTeam = Team.Red;
		
		public PersonalizedEndGameStatus Status(Team winningTeam, Account account)
		{
			if (winningTeam == BotTeam)
				return new SingleModeLose(account);

			return new SingleModeWin(account);
		}
	}
}