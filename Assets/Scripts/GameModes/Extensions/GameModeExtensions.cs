using System.Collections.Generic;
using System.Linq;
using CarSumo.Teams;

namespace GameModes.Extensions
{
	public static class GameModeExtensions
	{
		public static bool CanEnterGameModeWith(this IGameModePreferences gameModePreferences, params Team[] teamsToCheck)
		{
			return CanEnterGameModeWith(gameModePreferences, teams: teamsToCheck);
		}
		
		public static bool CanEnterGameModeWith(this IGameModePreferences gameModePreferences, IEnumerable<Team> teams)
		{
			return teams.All(team => gameModePreferences.GetAccountByTeam(team).Value != null);
		}
	}
}