using System.Collections.Generic;
using CarSumo.Teams;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.Vehicles.Stats
{
	public class VehiclePreferencesSo : SerializedScriptableObject, IVehicleStatsProvider
	{
		[SerializeField] private AnimationCurve _normalizedDrivingSpeed;
		[SerializeField] private float _drivingTime;

		[SerializeField] private IReadOnlyDictionary<Team, Material> _materials = new Dictionary<Team, Material>(0);

		public Material GetTeamVehicleMaterial(Team team)
		{
			return _materials[team];
		}
		
		public VehicleStats GetStats()
		{
			return new VehicleStats(Team.Blue, _normalizedDrivingSpeed, _drivingTime);
		}
	}
}