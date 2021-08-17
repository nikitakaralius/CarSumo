using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Vehicles.Stats
{
	public readonly struct VehicleStats
	{
		public Team Team { get; }
		
		public AnimationCurve NormalizedDrivingSpeed { get; }
		
		public float DrivingTime { get; }

		public VehicleStats(Team team, AnimationCurve normalizedDrivingSpeed, float drivingTime)
		{
			Team = team;
			NormalizedDrivingSpeed = normalizedDrivingSpeed;
			DrivingTime = drivingTime;
		}
	}
}