using System;
using System.Collections.Generic;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Menu.Cards
{
	[CreateAssetMenu(fileName = "CarViewRepository", menuName = "Cards/CardViewRepository")]
	public class CardViewRepository : SerializedScriptableObject, ICardViewRepository
	{
		[SerializeField] private IReadOnlyDictionary<Vehicle, GameObject> _views = new Dictionary<Vehicle, GameObject>();
		
		public GameObject ViewOf(Vehicle vehicle)
		{
			if (_views.ContainsKey(vehicle) == false)
			{
				throw new ArgumentOutOfRangeException($"{nameof(vehicle)} doesn't registered in the stats repository");
			}
			return _views[vehicle];
		}
	}
}