using System.Collections.Generic;
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

		public static void DestroyAndClear<TSource>(this List<TSource> list) where TSource : UnityObject
		{
			list.ForEach(UnityObject.Destroy);
			list.Clear();
		}
	}
}