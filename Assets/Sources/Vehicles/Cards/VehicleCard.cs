using System;
using UnityEngine;

namespace Sources.Cards
{
	[Serializable]
	public struct VehicleCard
	{
		[Min(0)] public int Strength;
		[Min(0)] public int Fuel;
	}
}