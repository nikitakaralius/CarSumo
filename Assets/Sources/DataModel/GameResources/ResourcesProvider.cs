using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CarSumo.DataModel.GameResources
{
	[CreateAssetMenu(fileName = "ResourcesProvider", menuName = "Resources/ResourcesProvider")]
	public class ResourcesProvider : SerializedScriptableObject
	{
		[SerializeField] private IReadOnlyDictionary<ResourceId, int> _amounts = new Dictionary<ResourceId, int>();
		
		public int AmountOf(ResourceId id)
		{
			if (_amounts.TryGetValue(id, out var amount) == false)
				throw new InvalidOperationException($"{id} resource is not defined in this provider");

			return amount;
		}

		public IEnumerable<KeyValuePair<ResourceId, int>> All() => _amounts;
	}
}