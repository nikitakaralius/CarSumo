using System.Collections.Generic;
using System.Linq;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Vehicles;
using UnityEngine;
using Zenject;

namespace Sources.AI
{
	public class BotPrototype : MonoBehaviour
	{
		[SerializeField] private Team _team;
		
		[SerializeField] private Vehicle[] _controlledVehicles;
		[SerializeField] private Vehicle[] _enemyVehicles;
		
		private ITeamChange _teamChange;

		[Inject]
		private void Construct(ITeamChange teamChange)
		{
			_teamChange = teamChange;
		}
		
		[ContextMenu(nameof(DriveOn))]
		private void DriveOn()
		{
			VehiclePair closestPair = Pairs().OrderBy(x => x.Distance).First();

			closestPair.Controlled.Rotation.RotateBy(-closestPair.Direction);
			closestPair.Controlled.Engine.SpeedUp(1);
			_teamChange.ChangeOnNextTeam();
		}

		private IEnumerable<VehiclePair> Pairs()
		{
			return _controlledVehicles
				.SelectMany(controlledVehicle => _enemyVehicles, (controlledVehicle, enemyVehicle) =>
						new VehiclePair(controlledVehicle, enemyVehicle));
		}

		private readonly struct VehiclePair
		{
			public readonly Vehicle Controlled;
			public readonly Vehicle Target;

			public VehiclePair(Vehicle controlled, Vehicle target)
			{
				Controlled = controlled;
				Target = target;
			}

			public float Distance => (Target.transform.position - Controlled.transform.position).magnitude;

			public Vector3 Direction => (Target.transform.position - Controlled.transform.position).normalized;
		}
	}
}