using CarSumo.Teams;
using UnityEngine;

namespace CarSumo.Vehicles.Stats
{
	public interface IVehicleStats
	{
		Team Team { get; }
	
		AnimationCurve NormalizedDrivingSpeed { get; }
		
		float DrivingTime { get; }
	}
}