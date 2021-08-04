using System;
using System.Collections.Generic;

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
    }
}