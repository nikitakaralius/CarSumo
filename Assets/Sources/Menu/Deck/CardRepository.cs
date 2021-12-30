using System;
using System.Collections.Generic;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Menu.Deck
{
	[CreateAssetMenu(menuName = "Cards/Repository", fileName = "CardRepository")]
	public class CardRepository : SerializedScriptableObject, ICardRepository
	{
		[SerializeField] private IReadOnlyDictionary<VehicleId, GameObject> _cardViews = new Dictionary<VehicleId, GameObject>();

		public GameObject ViewOf(VehicleId id)
		{
			if (_cardViews.ContainsKey(id) == false)
			{
				throw new ArgumentOutOfRangeException(nameof(id));
			}
			return _cardViews[id];
		}
	}
}