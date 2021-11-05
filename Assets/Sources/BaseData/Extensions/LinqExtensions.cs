using System;
using System.Collections.Generic;
using System.Linq;
using UnityRandom = UnityEngine.Random;

namespace CarSumo.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Every<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
                action.Invoke(element);

            return source;
        }

        public static TSource Random<TSource>(this IEnumerable<TSource> source) =>
            Random(source.ToArray());

        public static TSource Random<TSource>(this IReadOnlyList<TSource> source) => 
            source[UnityRandom.Range(0, source.Count)];
    }
}