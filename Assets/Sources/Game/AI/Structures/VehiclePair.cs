using CarSumo.Vehicles;
using Sirenix.Utilities;
using UnityEngine;

namespace AI.Structures
{
	public readonly struct VehiclePair
	{
		public readonly Vehicle Controlled;
		public readonly Vehicle Target;

		public VehiclePair(Vehicle controlled, Vehicle target)
		{
			Controlled = controlled;
			Target = target;
		}

		public float SqrDistance => VectorToTarget.sqrMagnitude;

		public bool Valid => 
			Controlled.SafeIsUnityNull() == false
			&& Target.SafeIsUnityNull() == false;

		public Vector3 Direction => VectorToTarget.normalized;

		private Vector3 VectorToTarget => Target.transform.position - Controlled.transform.position;
	}
}