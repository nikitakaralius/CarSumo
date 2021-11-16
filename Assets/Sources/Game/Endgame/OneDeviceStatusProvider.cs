using CarSumo.DataModel.Accounts;
using CarSumo.StateMachine;
using CarSumo.Teams;
using CarSumo.Units.Tracking;
using GameModes;

namespace Game.Endgame
{
	public class OneDeviceStatusProvider : IEndgameStatusProvider
	{
		public PersonalizedEndGameStatus Status(Team winningTeam, Account account) => 
			new OneDeviceEndGame(account);
	}
}