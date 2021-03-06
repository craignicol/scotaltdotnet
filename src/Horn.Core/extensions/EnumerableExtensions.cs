using System;
using System.Collections.Generic;

namespace Horn.Core.extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);

            return items;
        }

        public static bool HasElements<T>(this ICollection<T> collection)
        {
            return collection != null && collection.Count != 0;
        }
    }
}