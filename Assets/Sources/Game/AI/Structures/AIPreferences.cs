using CarSumo.Teams;
using UnityEngine;

namespace AI.Structures
{
	[CreateAssetMenu(fileName = "AIPreferences", menuName = "AI/Preferences")]
	public class AIPreferences : ScriptableObject
	{
		[SerializeField] private Team _botTeam;
		[SerializeField] private Team _enemyTeam;
		[SerializeField, Min(0.0f)] private float _prepareDuration;

		public Team BotTeam => _botTeam;

		public Team EnemyTeam => _enemyTeam;

		public float PrepareDuration => _prepareDuration;
	}
}