using System.Collections.Generic;
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
	}
}