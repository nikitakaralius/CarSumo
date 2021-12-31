using System;
using System.Collections.Generic;
using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Cards
{
	[CreateAssetMenu(fileName = "CardStatsRepository", menuName = "Cards/CardStatsRepository")]
	public class CardStatsRepository : ScriptableObject, ICardStatsRepository
	{
		[SerializeField] private IReadOnlyDictionary<Vehicle, VerboseVehicleStats> _stats;
		
		public VerboseVehicleStats StatsOf(ICard card)
		{
			if (_stats.ContainsKey(card.Vehicle) == false)
			{
				throw new ArgumentOutOfRangeException($"{nameof(card)} doesn't registered in the stats repository");
			}
			return _stats[card.Vehicle];
		}
	}
}