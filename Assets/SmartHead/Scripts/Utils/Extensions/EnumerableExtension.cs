using System;
using System.Collections.Generic;

namespace SmartHead.Utils.Extensions
{
    public static class EnumerableExtension
    {
        public static void Foreach<T>(this IEnumerable<T> source, Action<T> method)
        {
            foreach (var item in source)
            {
                method?.Invoke(item);
            }
        }
    }
}