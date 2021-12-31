using System;
using System.Collections.Generic;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.Cards
{
	[CreateAssetMenu(menuName = "Vehicles/VehicleCardsRepository", fileName = "VehicleCardsRepository")]
	public class VehicleCardsRepository : SerializedScriptableObject, IVehicleCardsRepository
	{
		[SerializeField] private IReadOnlyDictionary<Vehicle, VehicleCard> _cards = new Dictionary<Vehicle, VehicleCard>();

		public VehicleCard StatsOf(Vehicle vehicle)
		{
			if (_cards.TryGetValue(vehicle, out var card))
			{
				return card;
			}
			throw new ArgumentOutOfRangeException(nameof(vehicle), "Couldn't find card in repository. Make sure you has added this card");
		}
	}
}