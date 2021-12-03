using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Vehicles.Stats
{
	public readonly struct VehicleStats
	{
		public readonly Team Team;
		
		public readonly AnimationCurve NormalizedDrivingSpeed;
		public readonly AnimationCurve NormalizedDrivingTime;
		
		public readonly float RotationalSpeed;

		public VehicleStats(Team team, AnimationCurve normalizedDrivingSpeed, AnimationCurve normalizedDrivingTime, float rotationalSpeed)
		{
			Team = team;
			NormalizedDrivingSpeed = normalizedDrivingSpeed;
			NormalizedDrivingTime = normalizedDrivingTime;
			RotationalSpeed = rotationalSpeed;
		}
	}
}