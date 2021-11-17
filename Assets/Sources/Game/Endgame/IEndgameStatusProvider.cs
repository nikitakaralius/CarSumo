using CarSumo.DataModel.Accounts;
using CarSumo.Teams;

namespace Game.Endgame
{
	public interface IEndgameStatusProvider
	{
		PersonalizedEndGameStatus Status(Team winningTeam, Account account);
	}
}