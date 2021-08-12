using System.Collections.Generic;
using System.Linq;
using CarSumo.Teams;

namespace GameModes.Extensions
{
	public static class GameModeExtensions
	{
		public static bool CanEnterGameModeWith(this IGameModePreferences gameModePreferences, params Team[] teamsToCheck)
		{
			return teamsToCheck.All(team => gameModePreferences.GetAccountByTeam(team).Value != null);
		}
		
		public static bool CanEnterGameModeWith(this IGameModePreferences gameModePreferences, IEnumerable<Team> teamsToCheck)
		{
			return teamsToCheck.All(team => gameModePreferences.GetAccountByTeam(team).Value != null);
		}
	}
}