using System.Collections.Generic;
using System.Linq;
using CarSumo.Teams;
using CarSumo.Teams.TeamChanging;
using CarSumo.Units.Tracking;
using CarSumo.Vehicles;
using UnityEngine;
using Zenject;

namespace Sources.AI
{
	public class BotPrototype : MonoBehaviour
	{
		private const Team BotTeam = Team.Blue;
		private const Team EnemyTeam = Team.Red;
		
		private IVehicleTracker _vehicleTracker;
		private ITeamChange _teamChange;

		[Inject]
		private void Construct(ITeamChange teamChange, IVehicleTracker vehicleTracker)
		{
			_teamChange = teamChange;
			_vehicleTracker = vehicleTracker;
		}
		
		[ContextMenu(nameof(DriveOn))]
		private void DriveOn()
		{
			VehiclePair closestPair = Pairs(_vehicleTracker.VehiclesBy(BotTeam),
				_vehicleTracker.VehiclesBy(EnemyTeam))
				.OrderBy(x => x.Distance)
				.First();

			closestPair.Controlled.Rotation.RotateBy(-closestPair.Direction);
			closestPair.Controlled.Engine.SpeedUp(1);
			_teamChange.ChangeOnNextTeam();
		}

		private IEnumerable<VehiclePair> Pairs(IEnumerable<Vehicle> controlled, IEnumerable<Vehicle> enemy)
		{
			return controlled
				.SelectMany(controlledVehicle => enemy, (controlledVehicle, enemyVehicle) =>
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