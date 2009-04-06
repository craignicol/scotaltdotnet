namespace Horn.Domain.Utils
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensionMethods
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);

            return items;
        }
    }
}