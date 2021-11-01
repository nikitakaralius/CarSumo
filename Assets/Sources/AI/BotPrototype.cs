using AI.StateMachine.Common;
using AI.StateMachine.States;
using AI.Structures;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using UnityEngine;
using Zenject;

namespace Sources.AI
{
	public class BotPrototype : MonoBehaviour
	{
		private const Team BotTeam = Team.Blue;
		private const Team EnemyTeam = Team.Red;

		private AIStateMachine _stateMachine;
		
		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker tracker)
		{
			var transfer = new PairTransfer();

			_stateMachine = new AIStateMachine(new IAsyncState[]
			{
				new AISelectTargetState(tracker, transfer, BotTeam, EnemyTeam),
				new AIDriveOnTargetState(transfer),
				new AICompleteMoveState(teamChange, transfer)
			});
		}
		
		[ContextMenu(nameof(DriveOn))]
		private void DriveOn()
		{
			_stateMachine.RunAsync();
		}
	}
}