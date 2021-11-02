using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using Sources.BaseData.Operations;
using UnityEngine;
using Zenject;

namespace AI
{
	public class AIPlayer : MonoBehaviour
	{
		[SerializeField] private int _thinkMillisecondsDelay;
		[SerializeField] private float _prepareDuration;
		
		private const Team BotTeam = Team.Blue;
		private const Team EnemyTeam = Team.Red;
		
		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker tracker, ITeamPresenter teamPresenter, IAsyncTimeOperationPerformer performer)
		{
		}
	}
}