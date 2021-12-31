using System;
using System.Collections.Generic;
using System.Linq;
using DataModel.Vehicles;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Menu.Extensions
{
	public static class LinqExtensions
	{
		public static IEnumerable<TSource> RemoveFirstOccurrences<TSource>(this IEnumerable<TSource> collection, IEnumerable<TSource> occurrences)
		{
			var collectionWithoutFirstOccurrences = new List<TSource>(collection);

			foreach (TSource occurrence in occurrences) 
				collectionWithoutFirstOccurrences.Remove(occurrence);

			return collectionWithoutFirstOccurrences;
		}

		public static void DestroyAndClear<TComponent>(this List<TComponent> list) where TComponent : Component
		{
			list.ForEach(x => UnityObject.Destroy(x.gameObject));
			list.Clear();
		}
		public static void DestroyAndClear(this List<GameObject> list)
		{
			list.ForEach(UnityObject.Destroy);
			list.Clear();
		}

		public static IEnumerable<Vehicle> Without(this IVehicleStorage storage, IVehicleDeck deck)
		{
			return storage.BoughtVehicles.Without(deck.ActiveVehicles, (a, b) => a == b);
		}
		
		public static IEnumerable<TSource> Without<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> values, Func<TSource, TSource, bool> equalityComparer)
		{
			List<TSource> cachedValues = new List<TSource>(values);

			foreach (TSource element in source)
			{
				if (cachedValues.Any(cachedElement => equalityComparer.Invoke(cachedElement, element)))
				{
					cachedValues.Remove(element);
					continue;
				}

				yield return element;
			}
		}
	}
}